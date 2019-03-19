using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardCard : MonoBehaviour {

    public CardData cardData;
    public Image icon;

    /// <summary>
    /// Call to initialize a boardCard
    /// </summary>
    public void Init(CardData _cardData)
    {
        cardData = _cardData;

        if (CardManager.instance.AddCard(this.gameObject))
        {
            icon = GetComponent<Image>();
            icon.sprite = cardData.artwork;

            cardData.ApplyCardEffect();
        }
    }
}
