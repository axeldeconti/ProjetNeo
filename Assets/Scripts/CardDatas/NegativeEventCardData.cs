﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New card", menuName = "CardData/Event/NegativeCardData")]
public class NegativeEventCardData : EventCardData {

    public EventEffect effect;

    public override void ApplyCardEffect()
    {
        effect.ApplyEffect();
    }
}
