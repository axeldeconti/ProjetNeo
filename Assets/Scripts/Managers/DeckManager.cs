﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class DeckManager : MonoBehaviour {

    #region Singleton

    public static DeckManager instance { get; private set; }

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

        Init();
    }

    #endregion

    private Dictionary<string, CardData> allCardData;
    private List<string> humanCardsData, ressourceCardsData, toolCardsData, buildingCardsData, eventCardsData;
    private int cardInHand = 0;

    public AllCardDataStruct[] allData;
    public int nbCard;
    public GameObject humanCardPrefab, ressourceCardPrefab, toolCardPrefab, buildingCardPrefab, eventCardPrefab;
    public Transform HandPannel;
    public bool canEndTurn;
    public AudioClip cardFlipSFX;
    public GameObject burnAllButton, endTurnButton;

    /// <summary>
    /// Create the dictionary from the allData array
    /// </summary>
    private void Init()
    {
        humanCardsData = new List<string>();
        ressourceCardsData = new List<string>();
        toolCardsData = new List<string>();
        buildingCardsData = new List<string>();
        eventCardsData = new List<string>();

        allCardData = new Dictionary<string, CardData>();

        foreach (AllCardDataStruct obj in allData)
        {
            allCardData.Add(obj.name, obj.data);

            switch (obj.data.cardType)
            {
                case CardType.Human:
                    humanCardsData.Add(obj.name);
                    break;
                case CardType.Ressource:
                    ressourceCardsData.Add(obj.name);
                    break;
                case CardType.Tool:
                    toolCardsData.Add(obj.name);
                    break;
                case CardType.Building:
                    buildingCardsData.Add(obj.name);
                    break;
                case CardType.Event:
                    eventCardsData.Add(obj.name);
                    break;
                default:
                    Debug.Log("Can't add card name");
                    break;
            }
        }
    }

    private void Start()
    {
        AddCard("Human");
        AddCard("Human");
        AddCard("Human");
        AddCard("Berry");
        DrawRessourceCard();
        DrawRessourceCard();

        canEndTurn = true;
        endTurnButton.SetActive(false);
        burnAllButton.SetActive(true);
    }

    /// <summary>
    /// Call at the begining of a turn
    /// </summary>
    public void StartTurn()
    {
        for (int i = 0; i < nbCard; i++)
        {
            DrawRessourceCard();
        }

        for (int i = 0; i < GetComponent<HumanDrawManager>().ComputeHumanToDraw(); i++)
        {
            AddCard("Human");
        }

        cardInHand = HandPannel.childCount;
        endTurnButton.SetActive(false);
        burnAllButton.SetActive(true);
    }

    /// <summary>
    /// Draw one card
    /// </summary>
    public void DrawCard()
    {
        CardType type = GetRandomCardType();

        //Change the way data is assigned
        switch (type)
        {
            case CardType.Human:
                Instantiate(humanCardPrefab, HandPannel).GetComponent<Card>().Init(allCardData[GetRandomCardNameFromList(humanCardsData)]);
                break;
            case CardType.Ressource:
                Instantiate(ressourceCardPrefab, HandPannel).GetComponent<Card>().Init(allCardData[GetRandomCardNameFromList(ressourceCardsData)]);
                break;
            case CardType.Tool:
                Instantiate(toolCardPrefab, HandPannel).GetComponent<Card>().Init(allCardData[GetRandomCardNameFromList(toolCardsData)]);
                break;
            case CardType.Building:
                Instantiate(buildingCardPrefab, HandPannel).GetComponent<Card>().Init(allCardData[GetRandomCardNameFromList(buildingCardsData)]);
                break;
            case CardType.Event:
                Instantiate(eventCardPrefab, HandPannel).GetComponent<Card>().Init(allCardData[GetRandomCardNameFromList(eventCardsData)]);
                break;
            default:
                Debug.Log("No cardType for " + type);
                break;
        }

        if (!AudioManager.instance.effectSource.isPlaying)
            AudioManager.instance.PlaySoundEffects(cardFlipSFX);
    }

    /// <summary>
    /// Draw one ressource from available ones
    /// </summary>
    public void DrawRessourceCard()
    {
        //List<GameObject> allRessources =  CardManager.instance.GetAllCardsOfType(CardType.Ressource);

        bool cardFound = false;
        RessourceCardData data = null;

        while (!cardFound)
        {
            data = (allCardData[GetRandomCardNameFromList(ressourceCardsData)] as RessourceCardData);

            if (data.canBeDrawn)
                cardFound = true;
        }

        Instantiate(ressourceCardPrefab, HandPannel).GetComponent<Card>().Init(data);

        if (!AudioManager.instance.effectSource.isPlaying)
            AudioManager.instance.PlaySoundEffects(cardFlipSFX);
    }

    /// <summary>
    /// Add a specified card
    /// </summary>
    public Card AddCard(string cardNameToAdd)
    {
        CardData data = allCardData[cardNameToAdd];
        Card cardAdded = null;

        //Debug.Log("AddCard : " + cardNameToAdd + " of type " + data.cardType);

        switch (data.cardType)
        {
            case CardType.Human:
                cardAdded = Instantiate(humanCardPrefab, HandPannel).GetComponent<Card>();
                break;
            case CardType.Ressource:
                cardAdded = Instantiate(ressourceCardPrefab, HandPannel).GetComponent<Card>();
                break;
            case CardType.Tool:
                cardAdded = Instantiate(toolCardPrefab, HandPannel).GetComponent<Card>();
                break;
            case CardType.Building:
                cardAdded = Instantiate(buildingCardPrefab, HandPannel).GetComponent<Card>();
                break;
            case CardType.Event:
                cardAdded = Instantiate(eventCardPrefab, HandPannel).GetComponent<Card>();
                break;
            default:
                Debug.Log("Can't add this card : " + cardNameToAdd);
                break;
        }

        cardAdded.Init(data);

        UpdateCardInHandCount();

        if (!AudioManager.instance.effectSource.isPlaying)
            AudioManager.instance.PlaySoundEffects(cardFlipSFX);

        return cardAdded;
    }

    /// <summary>
    /// Return a random cardType
    /// </summary>
    public CardType GetRandomCardType()
    {
        System.Array A = System.Enum.GetValues(typeof(CardType));
        CardType type =  (CardType)UnityEngine.Random.Range(0, A.Length);

        return type;
    }

    /// <summary>
    /// Return a random name from the list in parameter
    /// </summary>
    public string GetRandomCardNameFromList(List<string> list)
    {
        return list[UnityEngine.Random.Range(0, list.Count)];
    }

    /// <summary>
    /// Update the card in had count to see if there are cards left
    /// </summary>
    public void UpdateCardInHandCount()
    {
        cardInHand = HandPannel.childCount;

        if (cardInHand == 0 && canEndTurn)
        {
            endTurnButton.SetActive(true);
            burnAllButton.SetActive(false);
        }
        else
        {
            endTurnButton.SetActive(false);
            burnAllButton.SetActive(true);
        }
    }

    public void ClearHand()
    {
        for (int i = HandPannel.childCount - 1; i >= 0; i--)
        {
            Destroy(HandPannel.GetChild(i).gameObject);
        }

        if (canEndTurn)
        {
            endTurnButton.SetActive(true);
            burnAllButton.SetActive(false);
        }
    }

    private void Update()
    {
#if UNITY_EDITOR

        if (Input.GetKeyDown(KeyCode.Tab))
            DrawCard();
        if (Input.GetKeyDown(KeyCode.W))
            AddCard("Wood");
        if (Input.GetKeyDown(KeyCode.S))
            AddCard("Stone");
        if (Input.GetKeyDown(KeyCode.A))
            AddCard("Axe");
        if (Input.GetKeyDown(KeyCode.L))
            AddCard("Spear");
        if (Input.GetKeyDown(KeyCode.H))
            AddCard("Human");
        if (Input.GetKeyDown(KeyCode.P))
            AddCard("Agricultural square");
        if (Input.GetKeyDown(KeyCode.M))
            AddCard("Meat");
        if (Input.GetKeyDown(KeyCode.C))
            AddCard("Clay");
        if (Input.GetKeyDown(KeyCode.K))
            AddCard("Cloth");
        if (Input.GetKeyDown(KeyCode.B))
            AddCard("Bone");
        if (Input.GetKeyDown(KeyCode.O))
            AddCard("Sorgho Seed");
        if (Input.GetKeyDown(KeyCode.I))
            AddCard("Shovel");

#endif
    }
}

/// <summary>
/// Struct to serve as input for allCardData in Unity
/// </summary>
[Serializable]
public struct AllCardDataStruct
{
    public string name;
    public CardData data;
}
