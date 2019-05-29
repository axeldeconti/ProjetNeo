using UnityEngine;
using UnityEngine.EventSystems;

public class WorkbenchDropZone : DropZone_Base
{
    public Workbench wb;

    protected override void DropCard(Draggable d, Card c)
    {
        RessourceCardData ressourceData;

        if (ressourceData = (c.cardData as RessourceCardData))
        {
            BoardCard bc = CreateBoardcard(d, c);
            wb.AddRessource(bc.gameObject);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) && !isEmpty)
        {
            wb.RemoveRessource(transform.GetChild(0).gameObject, true);
            TooltipPopup.instance.HideInfo();
        }
    }
}
