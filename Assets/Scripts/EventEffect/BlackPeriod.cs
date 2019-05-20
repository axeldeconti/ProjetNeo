using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New event effect", menuName = "Event effect/Black Period")]
public class BlackPeriod : EventEffect {

    public override void ApplyEffect()
    {
        base.ApplyEffect();

        DeckManager.instance.nbCard = 3;
    }

    public override void UnapplyEffect()
    {
        DeckManager.instance.nbCard = 6;
    }
}
