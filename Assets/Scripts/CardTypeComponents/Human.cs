using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Human : CardTypeComponent {

    private HumanCardData cardData;

    public int currentAge, maxAge, currentLife, maxLife, Atk;
    public Text age, life, attack;

    public override void Init(CardData _cardData)
    {
        HumanCardData tmp;

        if (tmp = (_cardData as HumanCardData))
        {
            cardData = tmp;
        }

        currentAge = 0;
        maxAge = Random.Range(cardData.minAge, cardData.maxAge + 1);
        maxLife = Random.Range(cardData.minLife, cardData.maxLife + 1);
    }
}
