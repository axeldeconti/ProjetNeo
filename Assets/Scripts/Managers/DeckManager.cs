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
                HumanCardData hData = new HumanCardData();
                cardPrefab = humanCardPrefab;
                Instantiate(cardPrefab, HandPannel).GetComponent<Card>().Init(hData);
                break;
            case CardType.Ressource:
                RessourceCardData rData = new RessourceCardData();
                cardPrefab = ressourceCardPrefab;
                Instantiate(cardPrefab, HandPannel).GetComponent<Card>().Init(rData);
                break;
            case CardType.Tool:
                ToolCardData tData = new ToolCardData();
                cardPrefab = toolCardPrefab;
                Instantiate(cardPrefab, HandPannel).GetComponent<Card>().Init(tData);
                break;
            case CardType.Building:
                BuildingCardData bData = new BuildingCardData();
                cardPrefab = buildingCardPrefab;
                Instantiate(cardPrefab, HandPannel).GetComponent<Card>().Init(bData);
                break;
            case CardType.Event:
                EventCardData eData = new EventCardData();
                cardPrefab = eventCardPrefab;
                Instantiate(cardPrefab, HandPannel).GetComponent<Card>().Init(eData);
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
