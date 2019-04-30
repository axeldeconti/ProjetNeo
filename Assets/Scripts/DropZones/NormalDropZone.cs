public class NormalDropZone : DropZone_Base
{
    public CardType[] acceptedTypes;

    protected override void DropCard(Draggable d, Card c)
    {
        bool canBeDropped = false;

        foreach (CardType type in acceptedTypes)
        {
            if(c.cardData.cardType == type)
            {
                canBeDropped = true;
                break;
            }
        }

        if(canBeDropped)
            CreateBoardcard(d, c);
    }
}
