using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class DropZone_Base : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    [HideInInspector] public Image myImage;
    public bool isEmpty = true;
    public GameObject cardParent;

    private void Start()
    {
        myImage = GetComponent<Image>();
    }

    /// <summary>
    /// Mouse over this dropzone
    /// </summary>
    public virtual void OnPointerEnter(PointerEventData eventData)
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
    public virtual void OnPointerExit(PointerEventData eventData)
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
    /// Call when something is dropped
    /// </summary>
    public void OnDrop(PointerEventData eventData)
    {
        Draggable d = eventData.pointerDrag.GetComponent<Draggable>();

        if (d != null)
        {
            //Drop the card if the pannel is empty
            if (isEmpty)
            {
                Card c = eventData.pointerDrag.GetComponent<Card>();

                DropCard(d, c);
            }
        }
    }

    /// <summary>
    /// Drop a card on this dropzone
    /// </summary>
    protected virtual void DropCard(Draggable d, Card c)
    {

    }

    /// <summary>
    /// Use when a card is dropped to create the board card
    /// </summary>
    protected BoardCard CreateBoardcard(Draggable d, Card c)
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
