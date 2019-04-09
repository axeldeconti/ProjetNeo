using System.Collections;
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
    public Button endButton;
    public Transform HandPannel;

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
        StartTurn();
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

        cardInHand = HandPannel.childCount;
        endButton.interactable = false;
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
    }

    /// <summary>
    /// Draw one ressource from available ones
    /// </summary>
    public void DrawRessourceCard()
    {
        List<GameObject> allRessources =  CardManager.instance.GetAllCardsOfType(CardType.Ressource);

        bool cardFound = false;
        RessourceCardData data = null;

        while (!cardFound)
        {
            data = (allCardData[GetRandomCardNameFromList(ressourceCardsData)] as RessourceCardData);

            if (data.canBeDrawn)
                cardFound = true;
        }

        Instantiate(ressourceCardPrefab, HandPannel).GetComponent<Card>().Init(data);
    }

    /// <summary>
    /// Add a specified card
    /// </summary>
    public void AddCard(string cardNameToAdd)
    {
        CardData data = allCardData[cardNameToAdd];

        switch (data.cardType)
        {
            case CardType.Human:
                Instantiate(humanCardPrefab, HandPannel).GetComponent<Card>().Init(data);
                break;
            case CardType.Ressource:
                Instantiate(ressourceCardPrefab, HandPannel).GetComponent<Card>().Init(data);
                break;
            case CardType.Tool:
                Instantiate(toolCardPrefab, HandPannel).GetComponent<Card>().Init(data);
                break;
            case CardType.Building:
                Instantiate(buildingCardPrefab, HandPannel).GetComponent<Card>().Init(data);
                break;
            case CardType.Event:
                Instantiate(eventCardPrefab, HandPannel).GetComponent<Card>().Init(data);
                break;
            default:
                Debug.Log("Can't add this card : " + cardNameToAdd);
                break;
        }
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

        if (cardInHand == 0)
            endButton.interactable = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
            DrawCard();
        if (Input.GetKeyDown(KeyCode.W))
            AddCard("Wood");
        if (Input.GetKeyDown(KeyCode.S))
            AddCard("Stone");
        if (Input.GetKeyDown(KeyCode.A))
            AddCard("Axe");
        if (Input.GetKeyDown(KeyCode.L))
            AddCard("Leather garment");
        if (Input.GetKeyDown(KeyCode.H))
            AddCard("Human");
        if (Input.GetKeyDown(KeyCode.P))
            AddCard("Workbench");
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
