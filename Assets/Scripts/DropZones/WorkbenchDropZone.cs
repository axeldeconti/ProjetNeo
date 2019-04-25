using UnityEngine;

public class WorkbenchDropZone : DropZone_Base
{
    protected override void DropCard(Draggable d, Card c)
    {
        RessourceCardData ressourceData;

        if (ressourceData = (c.cardData as RessourceCardData))
        {
            BoardCard bc = CreateBoardcard(d, c);
            if (cardParent.GetComponent<Workbench>() != null)
                cardParent.GetComponent<Workbench>().AddRessource(bc.gameObject);
            else cardParent.GetComponent<Building>().AddRessource(bc.gameObject);
        }
    }
}
