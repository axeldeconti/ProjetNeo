﻿using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class EventCardData : CardData {

    public EventType type;
    public string description;

    public override string GetTooltipInfoText()
    {
        return "";
    }
}

public enum EventType { Building, Random, Positive, Encounter }
