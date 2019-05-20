using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New card", menuName = "CardData/Event/EncounterCardData")]
public class EncounterEventCardData : EventCardData {

    public int atk;
    public RessourceCardData[] ressourcesToDrop;

    public override void ApplyCardEffect()
    {
        FightManager.instance.ResolveFight(this);
    }
}
