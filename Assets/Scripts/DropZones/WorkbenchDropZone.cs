using UnityEngine;
using UnityEngine.EventSystems;

public class WorkbenchDropZone : DropZone_Base, IPointerDownHandler
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

    public void OnPointerDown(PointerEventData eventData)
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) && !isEmpty)
        {
            Workbench wb = cardParent.GetComponent<Workbench>();

            if (wb != null)
            {
                wb.RemoveRessource(transform.GetChild(0).gameObject, true);
            }
            else
                Debug.Log("Not a ressource on workbench");
        }
    }
}
