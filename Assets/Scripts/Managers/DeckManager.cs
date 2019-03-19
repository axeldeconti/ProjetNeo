using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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

        CreateAllCardDataDictionary();
    }

    #endregion

    private Dictionary<string, CardData> allCardData;

    public AllCardDataStruct[] allData;
    public int nbCard;
    public GameObject humanCardPrefab, ressourceCardPrefab, toolCardPrefab, buildingCardPrefab, eventCardPrefab;
    public Transform HandPannel;

    /// <summary>
    /// Create the dictionary from the allData array
    /// </summary>
    private void CreateAllCardDataDictionary()
    {
        allCardData = new Dictionary<string, CardData>();

        foreach (AllCardDataStruct obj in allData)
            allCardData.Add(obj.name, obj.data);
    }

    private void Start()
    {
        for (int i = 0; i < nbCard; i++)
        {
            DrawCard();
        }
    }

    /// <summary>
    /// Draw one card
    /// </summary>
    public void DrawCard()
    {
        CardType type = GetRandomCardType();

        GameObject cardPrefab;

        //Change the way data is assigned
        switch (type)
        {
            case CardType.Human:
                cardPrefab = humanCardPrefab;
                Instantiate(cardPrefab, HandPannel).GetComponent<Card>().Init(allCardData["Human"]);
                break;
            case CardType.Ressource:
                cardPrefab = ressourceCardPrefab;
                Instantiate(cardPrefab, HandPannel).GetComponent<Card>().Init(allCardData["Ressource"]);
                break;
            case CardType.Tool:
                cardPrefab = toolCardPrefab;
                Instantiate(cardPrefab, HandPannel).GetComponent<Card>().Init(allCardData["Tool"]);
                break;
            case CardType.Building:
                cardPrefab = buildingCardPrefab;
                Instantiate(cardPrefab, HandPannel).GetComponent<Card>().Init(allCardData["Building"]);
                break;
            case CardType.Event:
                cardPrefab = eventCardPrefab;
                Instantiate(cardPrefab, HandPannel).GetComponent<Card>().Init(allCardData["Event"]);
                break;
            default:
                Debug.Log("No cardType for " + this.name);
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            DrawCard();
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
