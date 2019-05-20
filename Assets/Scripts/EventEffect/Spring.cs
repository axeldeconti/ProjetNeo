using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New event effect", menuName = "Event effect/Spring")]
public class Spring : EventEffect {

    public override void ApplyEffect()
    {
        base.ApplyEffect();

        FeedingManager.instance.nbOfHumanFedByOneFood = 4;
    }

    public override void UnapplyEffect()
    {
        FeedingManager.instance.nbOfHumanFedByOneFood = 3;
    }
}
