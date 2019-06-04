using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BoardCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public CardData cardData;
    public Image icon;

    /// <summary>
    /// Call to initialize a boardCard
    /// </summary>
    public void Init(CardData _cardData)
    {
        cardData = _cardData;

        if (CardManager.instance.AddCard(this.gameObject))
        {
            icon = GetComponent<Image>();
            icon.sprite = cardData.artwork;

            cardData.ApplyCardEffect();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerEnter.GetComponent<HumanDropZone>() != null)
            return;
               
        Human h = GetComponent<Human>();
        Building b = GetComponent<Building>();

        if (h != null)
            TooltipPopup.instance.DisplayInfo(cardData, h.TooltipText());
        else if(b != null)
            TooltipPopup.instance.DisplayInfo(cardData, b.TooltipText());
        else TooltipPopup.instance.DisplayInfo(cardData);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipPopup.instance.HideInfo();
    }

    /// <summary>
    /// Highlight the card
    /// </summary>
    public void SetHighlightBoardCard(bool isHighlighted)
    {
        Debug.Log("Highlight card : " + cardData.cardName);
    }
}
