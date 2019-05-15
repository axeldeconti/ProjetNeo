using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventCardData : CardData {

    public EventType type;

    public void CallEvent() { }
}

public enum EventType { Building, Random, Negative, Encounter }
