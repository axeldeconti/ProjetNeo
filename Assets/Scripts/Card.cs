using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Card : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{

    public CardData cardData;
    public Image frame, artwork, icon;
    public Text cardName;

    /// <summary>
    /// Call to initialize a boardCard
    /// </summary>
    public void Init(CardData _cardData)
    {
        cardData = _cardData;
        artwork.sprite = cardData.artwork;
        //cardName.text = cardData.cardName;
        cardName.text = "";
        icon.sprite = cardData.artwork;

        if (cardData.frame)
            frame.sprite = cardData.frame;

        GetComponent<CardTypeComponent>().Init(cardData);
    }

    /// <summary>
    /// Call when starts being dragged
    /// </summary>
    public void ChangeAspectToIcon()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        icon.gameObject.SetActive(true);
    }

    /// <summary>
    /// Call when stops being dragged
    /// </summary>
    public void ChangeAspectToCard()
    {
        icon.gameObject.SetActive(false);
        transform.GetChild(0).gameObject.SetActive(true);
    }

    /// <summary>
    /// Drop this card on the specified dropZone
    /// </summary>
    public BoardCard DropCard(DropZone_Base dz)
    {
        DeckManager.instance.UpdateCardInHandCount();

        GameObject boardCardPrefab;

        switch (cardData.cardType)
        {
            case CardType.Human:
                boardCardPrefab = CardManager.instance.humanBoardCardPrefab;
                break;
            case CardType.Ressource:
                boardCardPrefab = CardManager.instance.ressourceBoardCardPrefab;
                break;
            case CardType.Tool:
                boardCardPrefab = CardManager.instance.toolBoardCardPrefab;
                break;
            case CardType.Building:
                if(cardData.cardName.Equals("Agricultural square"))
                    boardCardPrefab = CardManager.instance.agriculturalSquarePrefab;
                else boardCardPrefab = CardManager.instance.buildingBoardCardPrefab;
                break;
            case CardType.Event:
                boardCardPrefab = CardManager.instance.eventBoardCardPrefab;
                break;
            default:
                boardCardPrefab = null;
                Debug.Log("No cardType found");
                break;
        }

        GameObject droppedCard = Instantiate(boardCardPrefab, dz.transform);
        droppedCard.GetComponent<BoardCard>().Init(cardData);
        droppedCard.GetComponent<CardTypeComponent>().InitBoardCard(this.GetComponent<CardTypeComponent>());

        if (cardData.cardName.Equals("Agricultural square"))
            droppedCard.GetComponent<AgriculturalSquare>().Init();

        return droppedCard.GetComponent<BoardCard>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Human h = GetComponent<Human>();
        Building b = GetComponent<Building>();

        if (h != null)
            TooltipPopup.instance.DisplayInfo(cardData, h.TooltipText());
        else if (b != null)
            TooltipPopup.instance.DisplayInfo(cardData, b.TooltipText());
        else TooltipPopup.instance.DisplayInfo(cardData);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipPopup.instance.HideInfo();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //Store the card with right click
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (!cardData.cardType.Equals(CardType.Human) && !cardData.cardType.Equals(CardType.Building))
            {
                if (Storage.instance.CheckStorage())
                {
                    Storage.instance.AddItemToStorage(GetComponent<Draggable>(), this);

                    if (DeckManager.instance.HandPannel.childCount <= 1)
                    {
                        DeckManager.instance.endTurnButton.SetActive(true);
                        DeckManager.instance.burnAllButton.SetActive(false);
                    }

                    TooltipPopup.instance.HideInfo();
                }
            }
        }
    }
}
