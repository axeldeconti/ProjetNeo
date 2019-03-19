using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Human : CardTypeComponent {

    private HumanCardData cardData;

    [HideInInspector] public int maxAge, maxLife;
    public HumanMetier metier;
    public int currentAge, currentLife, Atk;
    public Text age, life, attack;

    public override void Init(CardData _cardData)
    {
        HumanCardData tmp;

        if (tmp = (_cardData as HumanCardData))
        {
            cardData = tmp;
        }

        maxAge = Random.Range(cardData.minAge, cardData.maxAge + 1);
        maxLife = Random.Range(cardData.minLife, cardData.maxLife + 1);
        Atk = Random.Range(cardData.minAtk, cardData.maxAtk + 1);
        currentAge = 0;
        currentLife = maxLife;

        age.text = currentAge.ToString();
        life.text = currentLife.ToString();
        attack.text = Atk.ToString();
    }

    public override void InitBoardCard(CardTypeComponent _cardTypeComp)
    {
        Human humanComp, tmp;

        if (tmp = (_cardTypeComp as Human))
        {
            humanComp = tmp;
        } else
        {
            Debug.Log("Not HumanComponent in InitBoardCard");
            humanComp = null;
        }

        maxAge = humanComp.maxAge;
        maxLife = humanComp.maxLife;
        Atk = humanComp.Atk;
        currentAge = humanComp.currentAge;
        currentLife = humanComp.currentLife;

        age.text = currentAge.ToString();
        life.text = currentLife.ToString();
        attack.text = Atk.ToString();

        metier = HumanMetier.Harvester;
    }

    public override void EndTurn()
    {
        GrowOlder();
    }

    /// <summary>
    /// Call at the end of a turn to age this Human
    /// </summary>
    public void GrowOlder()
    {
        if (++currentAge >= maxAge)
        {
            Die();
        }

        age.text = currentAge.ToString();
    }

    /// <summary>
    /// Call to damage this Human
    /// </summary>
    public void GetDamaged(int damages)
    {
        currentLife -= damages;

        if (currentLife <= 0)
        {
            Die();
        }
    }

    /// <summary>
    /// Call to kill this Human
    /// </summary>
    public void Die()
    {
        RemoveCard();
        Destroy(this.gameObject);
    }
}
