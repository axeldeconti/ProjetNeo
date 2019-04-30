using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HumanToFeed : MonoBehaviour, IPointerDownHandler
{

    public int myHumanID;
    public bool isSelected = false;
    public Text age, life, attack;

    private FeedingManager feedingManager = FeedingManager.instance;

    public void Init(Human myHuman)
    {
        myHumanID = myHuman.GetInstanceID();

        age.text = myHuman.age.ToString();
        life.text = myHuman.life.ToString();
        attack.text = myHuman.attack.ToString();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (!isSelected)
            {
                isSelected = true;
                feedingManager.AddHumanSelected(myHumanID);
            }
            else
            {
                isSelected = false;
                feedingManager.RemoveHumanSelected(myHumanID);
            }
        }
    }
}
