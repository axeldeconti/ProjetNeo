using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    [HideInInspector] public Image myImage;
    public bool isEmpty = true;
    public bool isHumans = false;
    public bool isBuildings = false;
    public bool isStockage = false;
    public GameObject cardParent;

    private void Start()
    {
        myImage = GetComponent<Image>();
    }

    /// <summary>
    /// Mouse over this dropzone
    /// </summary>
    public void OnPointerEnter(PointerEventData eventData)
    {
        //Return if nothing is being dragged
        if (eventData.pointerDrag == null)
            return;

        Draggable d = eventData.pointerDrag.GetComponent<Draggable>();

        //Set the placeholder parent to this
        if (d != null)
            d.placeholderParent = transform;
    }

    /// <summary>
    /// Exit mouse over this dropzone
    /// </summary>
    public void OnPointerExit(PointerEventData eventData)
    {
        //Return if nothing is being dragged
        if (eventData.pointerDrag == null)
            return;

        Draggable d = eventData.pointerDrag.GetComponent<Draggable>();

        //Set the placeholder parent to the card one
        if (d != null && d.placeholderParent == transform)
            d.placeholderParent = d.parentToReturnTo;
    }

    /// <summary>
    /// Drop a card on this dropzone
    /// </summary>
    public void OnDrop(PointerEventData eventData)
    {
        Draggable d = eventData.pointerDrag.GetComponent<Draggable>();

        if (d != null)
        {
            //Drop the card if the pannel is empty
            if(isEmpty)
            {
                Card c = eventData.pointerDrag.GetComponent<Card>();
                ToolCardData toolData;
                RessourceCardData ressourceData;

                if (isHumans && (toolData = (c.cardData as ToolCardData)))
                {
                    DropCard(d, c).GetComponent<Tool>().DropOnHuman(toolData, cardParent.GetComponent<Human>());
                }
                else if (isBuildings && (ressourceData = (c.cardData as RessourceCardData)))
                {
                    BoardCard bc = DropCard(d, c);
                    if (cardParent.GetComponent<Workbench>() != null)
                        cardParent.GetComponent<Workbench>().AddRessource(bc.gameObject);
                    else cardParent.GetComponent<Building>().AddRessource(bc.gameObject);
                }
                else if (!isHumans && !isBuildings)
                {
                    DropCard(d, c);
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
