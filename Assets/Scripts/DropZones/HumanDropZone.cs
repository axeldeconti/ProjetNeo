using UnityEngine;

public class HumanDropZone : DropZone_Base
{

    protected override void DropCard(Draggable d, Card c)
    {
        ToolCardData toolData;

        if (toolData = c.cardData as ToolCardData)
        {
            CreateBoardcard(d, c).GetComponent<Tool>().DropOnHuman(toolData, cardParent.GetComponent<Human>());

            if (c.cardData.soundEffect != null)
                AudioManager.instance.PlaySoundEffects(c.cardData.soundEffect);
        }
    }
}
