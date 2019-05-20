using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New event effect", menuName = "Event effect/Love Period")]
public class LovePeriod : EventEffect {

    public int min, max;

    public override void ApplyEffect()
    {
        base.ApplyEffect();

        int rnd = Random.Range(min, max + 1);

        for (int i = 0; i <= rnd; i++)
        {
            DeckManager.instance.AddCard("Human");
        }
    }
}
