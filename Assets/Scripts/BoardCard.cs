using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardCard : MonoBehaviour {

    public CardData cardData;
    public Image icon;

    //Call to initialize a boardCard
    public void Init(CardData _cardData)
    {
        cardData = _cardData;

        icon = GetComponent<Image>();
        icon.sprite = cardData.artwork;

        cardData.ApplyCardEffect();

        CardManager.instance.AddCard(this);
    }
}
