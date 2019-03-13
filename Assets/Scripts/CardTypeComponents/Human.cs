using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Human : CardTypeComponent {

    private HumanCardData cardData;

    public int currentAge, maxAge, currentLife, maxLife, Atk;
    public Text age, life, attack;

    public void Init(HumanCardData _cardData)
    {
        cardData = _cardData;

        currentAge = 0;
        maxAge = Random.Range(cardData.minAge, cardData.maxAge + 1);
        maxLife = Random.Range(cardData.minLife, cardData.maxLife + 1);
    }
}
