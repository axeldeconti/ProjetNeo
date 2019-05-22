using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FoodDropZone : DropZone_Base
{
    public Sprite normal, highlighted;

    protected override void DropCard(Draggable d, Card c)
    {
        RessourceCardData ressourceData;

        if (!FeedingManager.instance.GetCanFeedHuman())
            return;

        if(ressourceData = (c.cardData as RessourceCardData))
        {
            if (ressourceData.isFood)
            {
                FeedingManager.instance.OpenFeedingScreen();

                //Set the card parent to this
                d.parentToReturnTo = this.transform;
                Destroy(d.placeholder);

                Destroy(c.gameObject);

                DeckManager.instance.UpdateCardInHandCount();
            }
        }
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);

        if (eventData.pointerDrag == null)
            return;

        Card c = eventData.pointerDrag.GetComponent<Card>();

        if(c != null)
        {
            RessourceCardData data;

            if(data = (c.cardData as RessourceCardData))
            {
                if (data.isFood && FeedingManager.instance.GetCanFeedHuman())
                {
                    GetComponent<Image>().sprite = highlighted;
                }
            }
        }
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);

        GetComponent<Image>().sprite = normal;
    }
}
