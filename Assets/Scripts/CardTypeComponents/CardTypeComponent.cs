using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CardTypeComponent : MonoBehaviour {

    public virtual void Init(CardData _cardData) { }
}

public class Humanshco : CardTypeComponent
{

    public override void Init(CardData _cardData)
    {
        base.Init(_cardData);

        HumanCardData cardData;// = (HumanCardData)_cardData;

        if(cardData = (_cardData as HumanCardData))
        {

        }
    }
}