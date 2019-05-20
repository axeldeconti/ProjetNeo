using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New event effect", menuName = "Event effect/Overall Weakness")]
public class OverallWeakness : EventEffect {

    public override void ApplyEffect()
    {
        base.ApplyEffect();

        DeckManager.instance.nbCard = 4;
    }

    public override void UnapplyEffect()
    {
        DeckManager.instance.nbCard = 6;
    }
}
