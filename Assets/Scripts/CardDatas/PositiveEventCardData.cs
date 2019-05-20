using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New card", menuName = "CardData/Event/PositiveCardData")]
public class PositiveEventCardData : EventCardData {

    public EventEffect effect;

    public override void ApplyCardEffect()
    {
        effect.ApplyEffect();
    }
}
