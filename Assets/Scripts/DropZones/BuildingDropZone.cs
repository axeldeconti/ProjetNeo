using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingDropZone : DropZone_Base {

    protected override void DropCard(Draggable d, Card c)
    {
        RessourceCardData ressourceData;

        if (ressourceData = (c.cardData as RessourceCardData))
        {
            if (GetComponent<Building>().AddRessource(ressourceData))
            {
                Destroy(c.gameObject);
                DeckManager.instance.UpdateCardInHandCount();

                if (c.cardData.soundEffect != null)
                    AudioManager.instance.PlaySoundEffects(c.cardData.soundEffect);
            }
        }
    }
}
