using UnityEngine;
using UnityEngine.UI;

public class Human : CardTypeComponent
{

    private HumanCardData cardData;

    [HideInInspector] public int maxAge, maxLife;
    [HideInInspector] public Tool tool = null;
    public HumanMetier metier;
    public int currentAge, currentLife, atk;
    public Text age, life, attack;
    public bool isFed;
    public GameObject hungerMarker;

    public override void Init(CardData _cardData)
    {
        HumanCardData tmp;

        if (tmp = (_cardData as HumanCardData))
        {
            cardData = tmp;
        }

        maxAge = Random.Range(cardData.minAge, cardData.maxAge + 1);
        maxLife = Random.Range(cardData.minLife, cardData.maxLife + 1);
        atk = Random.Range(cardData.minAtk, cardData.maxAtk + 1);
        currentAge = maxAge;
        currentLife = maxLife;

        age.text = currentAge.ToString();
        life.text = currentLife.ToString();
        attack.text = atk.ToString();
    }

    public override void InitBoardCard(CardTypeComponent _cardTypeComp)
    {
        Human humanComp, tmp;

        if (tmp = (_cardTypeComp as Human))
        {
            humanComp = tmp;
        }
        else
        {
            Debug.Log("Not HumanComponent in InitBoardCard");
            humanComp = null;
        }

        cardData = humanComp.cardData;

        maxAge = humanComp.maxAge;
        maxLife = humanComp.maxLife;
        atk = humanComp.atk;
        currentAge = humanComp.currentAge;
        currentLife = humanComp.currentLife;

        age.text = currentAge.ToString();
        life.text = currentLife.ToString();
        attack.text = atk.ToString();

        metier = HumanMetier.Harvester;
    }

    public override void StartTurn()
    {
        base.StartTurn();

        switch (metier)
        {
            case HumanMetier.Woodcutter:
                DeckManager.instance.AddCard("Wood");
                break;
            case HumanMetier.Miner:
                DeckManager.instance.AddCard("Stone");
                break;
            case HumanMetier.Farmer:
                DeckManager.instance.AddCard("Food");
                break;
            case HumanMetier.Hunter:
                DeckManager.instance.AddCard("Food");
                break;
            default:
                break;
        }

        isFed = false;
        hungerMarker.SetActive(true);
    }

    public override void EndTurn()
    {
        GrowOlder();

        if (!isFed)
            GetDamaged(1);
    }

    /// <summary>
    /// Call at the end of a turn to age this Human
    /// </summary>
    public void GrowOlder()
    {
        if (--currentAge <= 0)
        {
            Die();
        }

        age.text = currentAge.ToString();
    }

    /// <summary>
    /// Call to damage this Human
    /// </summary>
    public void GetDamaged(int damages)
    {
        currentLife -= damages;

        if (currentLife <= 0)
        {
            Die();
        }

        life.text = currentLife.ToString();
    }

    /// <summary>
    /// Call to kill this Human
    /// </summary>
    public void Die()
    {
        if (tool != null)
            tool.RemoveCard();

        RemoveCard();

        GameManager.instance.CheckForGameOver();

        Destroy(this.gameObject);
    }

    public void FeedMe()
    {
        isFed = true;
        hungerMarker.SetActive(false);
    }

    /// <summary>
    /// Add life point(s)
    /// </summary>
    public void AddLife(int lifeToAdd)
    {
        for (int i = 0; i < lifeToAdd; i++)
        {
            //Missing : Add life tokens
            maxLife++;
            currentLife++;
            life.text = currentLife.ToString();
        }
    }

    /// <summary>
    /// Add atk point(s)
    /// </summary>
    public void AddAtk(int atkToAdd)
    {
        for (int i = 0; i < atkToAdd; i++)
        {
            atk++;
            attack.text = atk.ToString();
        }
    }
}
