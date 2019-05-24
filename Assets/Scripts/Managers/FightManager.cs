using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FightManager : MonoBehaviour
{

    #region Singleton

    public static FightManager instance { get; private set; }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    #endregion

    public GameObject humanSelectionScreen, humanToSelectPrefab;
    public int damageToGive = 0;
    public Text damageLeftToGive;

    private List<int> humansAdded = new List<int>();
    private List<HumanToFeed> humansToSelect = new List<HumanToFeed>();

    /// <summary>
    /// Resolve the fight against the encounter
    /// </summary>
    public void ResolveFight(EncounterEventCardData encounter)
    {
        damageToGive = encounter.atk - ComputeHumanTotalAttack();

        if (damageToGive > 0)
        {
            //The fight is lost
            Debug.Log("The fight is lost");
            OpenHumanSelectionScreen();
        }
        else
        {
            //The fight is won
            Debug.Log("The fight is won");

            foreach (RessourceCardData card in encounter.ressourcesToDrop)
            {
                DeckManager.instance.AddCard(card.cardName);
            }
        }
    }

    /// <summary>
    /// Compute the sum of all humans attack on board
    /// </summary>
    private int ComputeHumanTotalAttack()
    {
        int totalAtk = 0;

        foreach (GameObject human in CardManager.instance.GetAllCardsOfType(CardType.Human))
        {
            totalAtk += human.GetComponent<Human>().atk;
        }

        return totalAtk;
    }

    /// <summary>
    /// Damage the human selected
    /// </summary>
    public void DamageHuman(int humanID)
    {
        Human h = CardManager.instance.allHumanCards[humanID].GetComponent<Human>();

        if (h.currentLife > 1)
        {
            h.GetDamaged(1);
            damageToGive--;
            damageLeftToGive.text = damageToGive.ToString();

            foreach (HumanToFeed hts in humansToSelect)
            {
                if (hts.myHumanID == humanID)
                {
                    hts.life.text = h.currentLife.ToString();
                    break;
                }
            }
        }
        else
        {
            humansAdded.Remove(h.GetInstanceID());
            h.Die();
            damageToGive--;
            damageLeftToGive.text = damageToGive.ToString();

            foreach (HumanToFeed hts in humansToSelect)
            {
                if (hts.myHumanID == humanID)
                {
                    humansToSelect.Remove(hts);

                    foreach (Transform child in humanSelectionScreen.transform.GetChild(1))
                    {
                        if (child.gameObject.GetInstanceID() == hts.gameObject.GetInstanceID())
                        {
                            Destroy(child.gameObject);
                            break;
                        }
                    }

                    break;
                }
            }
        }

        if (damageToGive <= 0)
            CloseHumanSelectionScreen();
    }

    /// <summary>
    /// Open the human selection screen and initialize it
    /// </summary>
    public void OpenHumanSelectionScreen()
    {
        humanSelectionScreen.SetActive(true);
        damageLeftToGive.transform.parent.gameObject.SetActive(true);
        damageLeftToGive.text = damageToGive.ToString();

        foreach (GameObject human in CardManager.instance.GetAllCardsOfType(CardType.Human))
        {
            GameObject HumanToFeed = Instantiate(humanToSelectPrefab, humanSelectionScreen.transform.GetChild(1));
            HumanToFeed.GetComponent<HumanToFeed>().Init(human.GetComponent<Human>(), false);
            HumanToFeed.GetComponent<HumanToFeed>().hungerMarker.SetActive(false);
            humansAdded.Add(human.GetInstanceID());
            humansToSelect.Add(HumanToFeed.GetComponent<HumanToFeed>());
        }
    }

    /// <summary>
    /// Close the human selection screen and clear it
    /// </summary>
    public void CloseHumanSelectionScreen()
    {
        humanSelectionScreen.SetActive(false);
        damageLeftToGive.transform.parent.gameObject.SetActive(false);

        foreach (Transform child in humanSelectionScreen.transform.GetChild(1))
        {
            Destroy(child.gameObject);
        }

        humansAdded.Clear();
        humansToSelect.Clear();
        damageToGive = 0;
    }
}
