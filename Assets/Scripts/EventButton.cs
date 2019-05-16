using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventButton : MonoBehaviour {

    public EventType type;
    public EventButton[] nextEvents;
    public EventCardData eventdata;
    public int lvl = 0;

    /// <summary>
    /// Init the EventButton and call the init of its next ones
    /// </summary>
    public void Init(int fatherLvl)
    {
        lvl = fatherLvl + 1;
        eventdata = EventManager.instance.ChooseRandomEventType(type);

        if (nextEvents.Length == 0)
            return;

        foreach (EventButton nextEvent in nextEvents)
        {
            if(nextEvent.lvl != 0)
                nextEvent.Init(lvl);
        }
    }

    public void CallEvent()
    {

    }
}
