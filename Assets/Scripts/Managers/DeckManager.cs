using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DeckManager : MonoBehaviour {

    #region Singleton

    public static DeckManager instance;

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

    public CardData[] allCardData;//Change this for a dictionary later
    public int nbCard;
    public GameObject humanCardPrefab, ressourceCardPrefab, toolCardPrefab, buildingCardPrefab, eventCardPrefab;
    public Transform HandPannel;

    private void Start()
    {
        for (int i = 0; i < nbCard; i++)
        {
            DrawCard();
        }
    }

    //Draw one card
    public void DrawCard()
    {
        CardType type = GetRandomCardType();

        GameObject cardPrefab;

        //Change the way data is assigned
        switch (type)
        {
            case CardType.Human:
                cardPrefab = humanCardPrefab;
                Instantiate(cardPrefab, HandPannel).GetComponent<Card>().Init(allCardData[0]);
                break;
            case CardType.Ressource:
                cardPrefab = ressourceCardPrefab;
                Instantiate(cardPrefab, HandPannel).GetComponent<Card>().Init(allCardData[1]);
                break;
            case CardType.Tool:
                cardPrefab = toolCardPrefab;
                Instantiate(cardPrefab, HandPannel).GetComponent<Card>().Init(allCardData[2]);
                break;
            case CardType.Building:
                cardPrefab = buildingCardPrefab;
                Instantiate(cardPrefab, HandPannel).GetComponent<Card>().Init(allCardData[3]);
                break;
            case CardType.Event:
                cardPrefab = eventCardPrefab;
                Instantiate(cardPrefab, HandPannel).GetComponent<Card>().Init(allCardData[4]);
                break;
            default:
                Debug.Log("No cardType for " + this.name);
                break;
        }
    }

    //Return a random cardType
    public CardType GetRandomCardType()
    {
        System.Array A = System.Enum.GetValues(typeof(CardType));
        CardType type =  (CardType)UnityEngine.Random.Range(0, A.Length);
        return type;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            DrawCard();
    }
}
