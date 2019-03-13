using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    [HideInInspector] public Image myImage;
    public bool isEmpty = true;

    private void Start()
    {
        myImage = GetComponent<Image>();
    }

    //Mouse over this dropzone
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

    //Exit mouse over this dropzone
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

    public void OnDrop(PointerEventData eventData)
    {
        Draggable d = eventData.pointerDrag.GetComponent<Draggable>();

        if (d != null)
        {
            //Drop the card if the pannel is empty
            if(isEmpty)
            {
                //Set the card parent to this
                d.parentToReturnTo = this.transform;
                Destroy(d.placeholder);

                Card c = eventData.pointerDrag.GetComponent<Card>();
                c.DropCard(this);
                isEmpty = false;
            }
        }
    }
}
