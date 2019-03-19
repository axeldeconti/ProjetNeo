using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour {

    #region Singleton

    public static CardManager instance { get; private set; }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    #endregion

    public Dictionary<int, GameObject> allHumanCards = new Dictionary<int, GameObject>();
    public Dictionary<int, GameObject> allRessourceCards = new Dictionary<int, GameObject>();
    public Dictionary<int, GameObject> allToolCards = new Dictionary<int, GameObject>();
    public Dictionary<int, GameObject> allBuildingCards = new Dictionary<int, GameObject>();
    public GameObject humanBoardCardPrefab, ressourceBoardCardPrefab, toolBoardCardPrefab, buildingBoardCardPrefab, eventBoardCardPrefab;

    /// <summary>
    /// Add a card to the right dictionary
    /// </summary>
    public bool AddCard(GameObject cardToAdd)
    {
        CardType type = cardToAdd.GetComponent<BoardCard>().cardData.cardType;
        //Debug.Log("Card to add : " + cardToAdd.GetComponent<BoardCard>().cardData.cardName + " " + cardToAdd.GetInstanceID().ToString());

        switch (type)
        {
            case CardType.Human:
                allHumanCards.Add(cardToAdd.GetInstanceID(), cardToAdd);
                break;
            case CardType.Ressource:
                allRessourceCards.Add(cardToAdd.GetInstanceID(), cardToAdd);
                break;
            case CardType.Tool:
                allToolCards.Add(cardToAdd.GetInstanceID(), cardToAdd);
                break;
            case CardType.Building:
                allBuildingCards.Add(cardToAdd.GetInstanceID(), cardToAdd);
                break;
            default:
                Debug.Log("Can't add card : " + cardToAdd.GetInstanceID());
                return false;
        }

        return true;
    }

    /// <summary>
    /// Remove a card from the right dictionary
    /// </summary>
    public void RemoveCard(GameObject cardToRemove)
    {
        CardType type = cardToRemove.GetComponent<BoardCard>().cardData.cardType;

        switch (type)
        {
            case CardType.Human:
                if (allHumanCards.ContainsKey(cardToRemove.GetInstanceID()))
                    allHumanCards.Remove(cardToRemove.GetInstanceID());
                break;
            case CardType.Ressource:
                if (allRessourceCards.ContainsKey(cardToRemove.GetInstanceID()))
                    allRessourceCards.Remove(cardToRemove.GetInstanceID());
                break;
            case CardType.Tool:
                if (allToolCards.ContainsKey(cardToRemove.GetInstanceID()))
                    allToolCards.Remove(cardToRemove.GetInstanceID());
                break;
            case CardType.Building:
                if (allBuildingCards.ContainsKey(cardToRemove.GetInstanceID()))
                    allBuildingCards.Remove(cardToRemove.GetInstanceID());
                break;
            default:
                Debug.Log("Can't remove card : " + cardToRemove.GetInstanceID());
                break;
        }
    }

    /// <summary>
    /// Return a list of all cards of the specified cardType
    /// </summary>
    public List<GameObject> GetAllCardsOfType(CardType cardType)
    {
        List<GameObject> allCards = new List<GameObject>();

        Dictionary<int, GameObject> tmp;

        switch (cardType)
        {
            case CardType.Human:
                tmp = allHumanCards;
                break;
            case CardType.Ressource:
                tmp = allRessourceCards;
                break;
            case CardType.Tool:
                tmp = allToolCards;
                break;
            case CardType.Building:
                tmp = allBuildingCards;
                break;
            default:
                tmp = null;
                Debug.Log("Can't return this cardType : " + cardType);
                return null;
        }

        foreach (GameObject card in tmp.Values)
        {
            allCards.Add(card);
        }

        return allCards;
    }

    /// <summary>
    /// Return all cards
    /// </summary>
    public List<GameObject> GetAllCards()
    {
        List<GameObject> allCards = new List<GameObject>();

        foreach (GameObject card in allHumanCards.Values)
            allCards.Add(card);

        foreach (GameObject card in allRessourceCards.Values)
            allCards.Add(card);

        foreach (GameObject card in allToolCards.Values)
            allCards.Add(card);

        foreach (GameObject card in allBuildingCards.Values)
            allCards.Add(card);

        return allCards;
    }
}
