public class FoodDropZone : DropZone_Base
{
    protected override void DropCard(Draggable d, Card c)
    {
        RessourceCardData ressourceData;

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
}
