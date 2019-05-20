using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New event effect", menuName = "Event effect/Natural Migration")]
public class NaturalMigration : EventEffect {

    public EncounterEventCardData bison;

    public override void ApplyEffect()
    {
        base.ApplyEffect();

        foreach (RessourceCardData card in bison.ressourcesToDrop)
        {
            DeckManager.instance.AddCard(card.cardName);
        }
    }
}
