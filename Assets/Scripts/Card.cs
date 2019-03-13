using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour {

    public CardData cardData;
    public Image artwork, icon;
    public Text cardName;

    //Initialisation base
    public void Init(CardData _cardData)
    {
        cardData = _cardData;
        artwork.sprite = cardData.artwork;
    }

    //Initialisation for Human
    public void Init(HumanCardData _cardData)
    {
        cardData = _cardData;
        artwork.sprite = cardData.artwork;
        GetComponent<Human>().Init(_cardData);
    }

    //Initialisation for Ressource
    public void Init(RessourceCardData _cardData)
    {
        cardData = _cardData;
        artwork.sprite = cardData.artwork;
        GetComponent<Ressource>().Init(_cardData);
    }

    //Initialisation for Tool
    public void Init(ToolCardData _cardData)
    {
        cardData = _cardData;
        artwork.sprite = cardData.artwork;
        GetComponent<Tool>().Init(_cardData);
    }

    //Initialisation for Building
    public void Init(BuildingCardData _cardData)
    {
        cardData = _cardData;
        artwork.sprite = cardData.artwork;
        GetComponent<Building>().Init(_cardData);
    }

    //Initialisation for Event
    public void Init(EventCardData _cardData)
    {
        cardData = _cardData;
        artwork.sprite = cardData.artwork;
        GetComponent<Event>().Init(_cardData);
    }

    //Call when starts being dragged
    public void ChangeAspectToIcon()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        icon.gameObject.SetActive(true);
    }

    //Call when stops being dragged
    public void ChangeAspectToCard()
    {
        icon.gameObject.SetActive(false);
        transform.GetChild(0).gameObject.SetActive(true);
    }

    //Drop this card on the specified dropZone
    public void DropCard(DropZone dz)
    {
        Instantiate(CardManager.instance.boardCardPrefab, dz.transform).GetComponent<BoardCard>().Init(cardData);
        Destroy(gameObject);
    }
}
