using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardCard : MonoBehaviour {

    [HideInInspector] public CardData cardData;
    public Image icon;

    public void Init(CardData _cardData)
    {
        cardData = _cardData;

        icon = GetComponent<Image>();

        icon.sprite = cardData.artwork;
    }
}
