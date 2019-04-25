public class NormalDropZone : DropZone_Base
{

    protected override void DropCard(Draggable d, Card c)
    {
        CreateBoardcard(d, c);
    }
}
