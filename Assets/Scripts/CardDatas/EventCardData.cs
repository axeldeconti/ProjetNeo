using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventCardData : CardData {

    public EventType type;
    public string description;
}

public enum EventType { Building, Random, Positive, Encounter }
