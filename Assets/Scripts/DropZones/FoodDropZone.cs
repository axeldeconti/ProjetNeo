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
            }
        }
    }
}
