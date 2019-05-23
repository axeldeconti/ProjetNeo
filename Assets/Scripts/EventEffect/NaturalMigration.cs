using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New event effect", menuName = "Event effect/Natural Migration")]
public class NaturalMigration : EventEffect {

    public EncounterEventCardData animal;

    public override void ApplyEffect()
    {
        base.ApplyEffect();

        foreach (RessourceCardData card in animal.ressourcesToDrop)
        {
            DeckManager.instance.AddCard(card.cardName);
        }
    }
}
