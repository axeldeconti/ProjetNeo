using UnityEngine;

[CreateAssetMenu(fileName = "New event effect", menuName = "Event effect/Glacial Winter")]
public class GlacialWinter : EventEffect {

    public override void ApplyEffect()
    {
        base.ApplyEffect();

        FeedingManager.instance.nbOfHumanFedByOneFood = 2;
    }

    public override void UnapplyEffect()
    {
        FeedingManager.instance.nbOfHumanFedByOneFood = 3;
    }
}
