using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropZoneStorage : DropZone, IPointerEnterHandler
{
    public bool hasRessource = false;

    public void OnDropStorage(PointerEventData eventData)
    {
        Draggable d = eventData.pointerDrag.GetComponent<Draggable>();

        if (d != null)
        {
            //Drop the card if the pannel is empty
            if (isEmpty)
            {
                Card c = eventData.pointerDrag.GetComponent<Card>();
                RessourceCardData ressourceData;

                // Add Ressource
                if (isStockage && (ressourceData = (c.cardData as RessourceCardData)) && hasRessource == false)
                {
                    hasRessource = true;
                    BoardCard bc = DropCard(d, c);
                    Storage.instance.AddItemToStorage(bc.cardData.cardName);
                }

                // Remove Ressource
                else if (isStockage && (ressourceData = (c.cardData as RessourceCardData)) && hasRessource)
                {
                    BoardCard bc = DropCard(d, c);
                    Storage.instance.RemoveItemFromStorage(bc.cardData.cardName);

                    if (Storage.instance.isEmpty)
                    {
                        hasRessource = false;
                    }
                }
            }
        }
    }

    private BoardCard DropCard(Draggable d, Card c)
    {
        //Set the card parent to this
        d.parentToReturnTo = this.transform;
        Destroy(d.placeholder);

        BoardCard bc = c.DropCard(this);
        Destroy(c.gameObject);
        isEmpty = false;
        return bc;
    }
}
