using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour {

    #region Singleton

    public static CardManager instance;

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

    private Dictionary<CardType, List<GameObject>> allCards = new Dictionary<CardType, List<GameObject>>();

    public GameObject boardCardPrefab;

    private void Start()
    {
        foreach(CardType type in Enum.GetValues(typeof(CardType)))
        {
            allCards[type] = new List<GameObject>();
        }
    }

    //Add a card to allCards
    public void AddCard(BoardCard cardToAdd)
    {
        CardType type = cardToAdd.cardData.cardType;
        allCards[type].Add(cardToAdd.gameObject);
    }

    //Remove a card from allCards
    public void RemoveCard(Card cardToRemove)
    {
        CardType type = cardToRemove.cardData.cardType;
        allCards[type].Remove(cardToRemove.gameObject);
    }

    //Return of all cards of the specified cardType
    public List<GameObject> GetAllCardsOfType(CardType cardType)
    {
        return allCards[cardType];
    }
    
}
