using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HumanToFeed : MonoBehaviour, IPointerDownHandler
{

    public int myHumanID;
    public bool isSelected = false;
    public Text age, life, attack;
    public GameObject hungerMarker;

    private bool toFeed;
    private FeedingManager feedingManager = FeedingManager.instance;
    private FightManager fightManager = FightManager.instance;

    public void Init(Human myHuman, bool _toFeed)
    {
        myHumanID = myHuman.gameObject.GetInstanceID();

        age.text = myHuman.currentAge.ToString();
        life.text = myHuman.currentLife.ToString();
        attack.text = myHuman.atk.ToString();

        toFeed = _toFeed;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (!isSelected && toFeed)
            {
                isSelected = true;
                feedingManager.AddHumanSelected(myHumanID);
                hungerMarker.SetActive(false);
            }
            else if (toFeed)
            {
                isSelected = false;
                feedingManager.RemoveHumanSelected(myHumanID);
                hungerMarker.SetActive(true);
            }
            else
            {
                fightManager.DamageHuman(myHumanID);
            }
        }
    }
}
