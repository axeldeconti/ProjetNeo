using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Bin : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    /// <summary>
    /// Mouse over the bin
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
    /// Exit mouse over the bin
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
    /// Drop a card on the bin
    /// </summary>
    public void OnDrop(PointerEventData eventData)
    {
        Draggable d = eventData.pointerDrag.GetComponent<Draggable>();

        if (d != null)
        {
            DeckManager.instance.UpdateCardInHandCount();
            Destroy(d.gameObject);
            Destroy(transform.GetChild(0).gameObject);
        }
    }
}
