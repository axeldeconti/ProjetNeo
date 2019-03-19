using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour {

    public CardData cardData;
    public Image artwork, icon;
    public Text cardName;

    /// <summary>
    /// Call to initialize a boardCard
    /// </summary>
    public void Init(CardData _cardData)
    {
        cardData = _cardData;
        artwork.sprite = cardData.artwork;
        cardName.text = cardData.name;

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
    public void DropCard(DropZone dz)
    {
        GameObject boardCardPrefab;

        switch (cardData.cardType)
        {
            case CardType.Human:
                boardCardPrefab = CardManager.instance.humanBoardCardPrefab;
                break;
            case CardType.Ressource:
                boardCardPrefab = CardManager.instance.toolBoardCardPrefab;
                break;
            case CardType.Tool:
                boardCardPrefab = CardManager.instance.toolBoardCardPrefab;
                break;
            case CardType.Building:
                boardCardPrefab = CardManager.instance.buildingBoardCardPrefab;
                break;
            case CardType.Event:
                boardCardPrefab = CardManager.instance.eventBoardCardPrefab;
                break;
            default:
                boardCardPrefab = null;
                Debug.Log("No cardType found");
                break;
        }

        GameObject droppedCard;
        droppedCard = Instantiate(boardCardPrefab, dz.transform);
        droppedCard.GetComponent<BoardCard>().Init(cardData);
        droppedCard.GetComponent<CardTypeComponent>().InitBoardCard(this.GetComponent<CardTypeComponent>());

        Destroy(gameObject);
    } 

    #region Inits
    /*
    //Initialisation for Human
    public void Init(HumanCardData _cardData)
    {
        cardData = _cardData;
        artwork.sprite = cardData.artwork;
        cardName.text = cardData.name;
        GetComponent<Human>().Init(_cardData);
    }

    //Initialisation for Ressource
    public void Init(RessourceCardData _cardData)
    {
        cardData = _cardData;
        artwork.sprite = cardData.artwork;
        cardName.text = cardData.name;
        GetComponent<Ressource>().Init(_cardData);
    }

    //Initialisation for Tool
    public void Init(ToolCardData _cardData)
    {
        cardData = _cardData;
        artwork.sprite = cardData.artwork;
        cardName.text = cardData.name;
        GetComponent<Tool>().Init(_cardData);
    }

    //Initialisation for Building
    public void Init(BuildingCardData _cardData)
    {
        cardData = _cardData;
        artwork.sprite = cardData.artwork;
        cardName.text = cardData.name;
        GetComponent<Building>().Init(_cardData);
    }

    //Initialisation for Event
    public void Init(EventCardData _cardData)
    {
        cardData = _cardData;
        artwork.sprite = cardData.artwork;
        cardName.text = cardData.name;
        GetComponent<Event>().Init(_cardData);
    }
    */
    #endregion
}
