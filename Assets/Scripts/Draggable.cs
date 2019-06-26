using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

    [HideInInspector] public Transform parentToReturnTo = null, placeholderParent = null, emptyParent = null;
    [HideInInspector] public GameObject placeholder = null;

    public void OnBeginDrag(PointerEventData eventData)
    {
        //Create a placeholder for the layout group
        placeholder = new GameObject();
        placeholder.name = "Placeholder";
        placeholder.transform.SetParent(transform.parent);

        //Setup the layout element of the placeholder
        LayoutElement le = placeholder.AddComponent<LayoutElement>();
        le.preferredWidth = GetComponent<LayoutElement>().preferredWidth;
        le.preferredHeight = GetComponent<LayoutElement>().preferredHeight;
        le.flexibleWidth = 0;
        le.flexibleHeight = 0;

        //Set the placeholder at the same place as this
        placeholder.transform.SetSiblingIndex(transform.GetSiblingIndex());

        //Set the right parent of the placeholder
        parentToReturnTo = transform.parent;
        placeholderParent = parentToReturnTo;
        transform.SetParent(transform.parent.parent);
        emptyParent = transform.parent;

        //Block raycast to "see" through the card
        GetComponent<CanvasGroup>().blocksRaycasts = false;

        //Change the aspect of the card to the icon
        GetComponent<Card>().ChangeAspectToIcon();
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;

        //Move the placeholder
        if (placeholder.transform.parent != placeholderParent)
            placeholder.transform.SetParent(placeholderParent);

        int newSiblingIndex = placeholderParent.childCount;

        //Set the placeholder at the right position in the group layout
        for (int i = 0; i < placeholderParent.childCount; i++)
        {
            if (transform.position.x < placeholderParent.GetChild(i).position.x)
            {
                newSiblingIndex = i;

                if (placeholder.transform.GetSiblingIndex() < newSiblingIndex)
                    newSiblingIndex--;

                break;
            }
        }
        placeholder.transform.SetSiblingIndex(newSiblingIndex);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Set this card to the placeholder place
        transform.SetParent(parentToReturnTo);
        transform.SetSiblingIndex(placeholder.transform.GetSiblingIndex());
        GetComponent<CanvasGroup>().blocksRaycasts = true;

        Destroy(placeholder);

        //Change the aspect from icon to card
        GetComponent<Card>().ChangeAspectToCard();
    }
}
