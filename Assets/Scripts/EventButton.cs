using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EventButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public EventType type;
    public EventButton[] nextEvents;
    public EventCardData eventdata;
    public int lvl = 0;
    public bool isEndButton;

    /// <summary>
    /// Init the EventButton and call the init of its next ones
    /// </summary>
    public void Init(int fatherLvl)
    {
        if (isEndButton)
            return;

        lvl = fatherLvl + 1;
        eventdata = EventManager.instance.ChooseRandomEventType(type);
        EventManager.instance.AddEventButton(this);

        GetComponent<Button>().interactable = false;

        if (nextEvents.Length == 0)
            return;

        foreach (EventButton nextEvent in nextEvents)
        {
            if (nextEvent.lvl == 0)
                nextEvent.Init(lvl);
        }
    }

    public void CallEvent()
    {
        if (EventManager.instance.canGoToNextLevel)
            eventdata.ApplyCardEffect();

        EventSystem.current.SetSelectedGameObject(null);
    }

    public void OnPointerEnter(PointerEventData _eventData)
    {
        if(GetComponent<Button>().interactable)
            TooltipPopup.instance.DisplayInfo(eventdata);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipPopup.instance.HideInfo();
    }
}
