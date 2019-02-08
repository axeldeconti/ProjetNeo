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

    public CardData[] allCardData;
    public GameObject cardPrefab, boardCardPrefab;
    public Transform HandPannel;

    public void CreateCard()
    {
        Instantiate(cardPrefab, HandPannel).GetComponent<Card>().InitializeCard(GetRandomCardData());
    }

    //Returns a random cardData from allCardData
    public CardData GetRandomCardData()
    {
        return allCardData[Random.Range(0, allCardData.Length)];
    }
}
