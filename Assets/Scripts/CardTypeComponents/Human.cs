﻿using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class Human : CardTypeComponent
{

    private HumanCardData cardData;

    [HideInInspector] public int maxAge, maxLife;
    [HideInInspector] public Tool tool = null;
    public HumanMetier metier;
    public int currentAge, currentLife, atk;
    public Text age, life, attack;
    public bool isFed;
    public GameObject hungerMarker;
    public ParticleSystem humanFedPS;
    public int damageIfNotFed;

    public override void Init(CardData _cardData)
    {
        HumanCardData tmp;

        if (tmp = (_cardData as HumanCardData))
        {
            cardData = tmp;
        }

        maxAge = Random.Range(cardData.minAge, cardData.maxAge + 1);
        maxLife = Random.Range(cardData.minLife, cardData.maxLife + 1);
        atk = Random.Range(cardData.minAtk, cardData.maxAtk + 1);
        currentAge = maxAge;
        currentLife = maxLife;

        age.text = currentAge.ToString();
        life.text = currentLife.ToString();
        attack.text = atk.ToString();

        cardData.human = this;
    }

    public override void InitBoardCard(CardTypeComponent _cardTypeComp)
    {
        Human humanComp, tmp;

        if (tmp = (_cardTypeComp as Human))
        {
            humanComp = tmp;
        }
        else
        {
            Debug.Log("Not HumanComponent in InitBoardCard");
            humanComp = null;
        }

        cardData = humanComp.cardData;

        maxAge = humanComp.maxAge;
        maxLife = humanComp.maxLife;
        atk = humanComp.atk;
        currentAge = humanComp.currentAge;
        currentLife = humanComp.currentLife;
        damageIfNotFed = humanComp.damageIfNotFed;

        age.text = currentAge.ToString();
        life.text = currentLife.ToString();
        attack.text = atk.ToString();

        metier = HumanMetier.Harvester;

        cardData.human = this;
    }

    public override void StartTurn()
    {
        base.StartTurn();

        switch (metier)
        {
            case HumanMetier.Woodcutter:
                DeckManager.instance.AddCard("Wood");
                break;
            case HumanMetier.Miner:
                DeckManager.instance.AddCard("Stone");
                break;
            case HumanMetier.Farmer:
                break;
            case HumanMetier.Hunter:
                DeckManager.instance.AddCard("Meat");
                break;
            default:
                break;
        }

        isFed = false;
        hungerMarker.SetActive(true);
    }

    public override void EndTurn()
    {
        GrowOlder();

        if (!isFed)
            GetDamaged(damageIfNotFed);
    }

    /// <summary>
    /// Call at the end of a turn to age this Human
    /// </summary>
    public void GrowOlder()
    {
        if (--currentAge <= 0)
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
        Debug.Log("get damage " + damages);
        currentLife -= damages;

        if (currentLife <= 0)
        {
            Die();
        }

        life.text = currentLife.ToString();
    }

    /// <summary>
    /// Call to kill this Human
    /// </summary>
    public void Die()
    {
        if (tool != null)
            tool.RemoveCard();

        RemoveCard();

        GameManager.instance.CheckForGameOver();

        Destroy(this.gameObject);
    }

    public void FeedMe()
    {
        isFed = true;
        hungerMarker.SetActive(false);
        humanFedPS.Play();
    }

    /// <summary>
    /// Add life point(s)
    /// </summary>
    public void AddLife(int lifeToAdd)
    {
        for (int i = 0; i < lifeToAdd; i++)
        {
            //Missing : Add life tokens
            maxLife++;
            currentLife++;
            life.text = currentLife.ToString();
        }
    }

    /// <summary>
    /// Add atk point(s)
    /// </summary>
    public void AddAtk(int atkToAdd)
    {
        for (int i = 0; i < atkToAdd; i++)
        {
            atk++;
            attack.text = atk.ToString();
        }
    }

    public string TooltipText()
    {
        StringBuilder builder = new StringBuilder();

        HumanCardData data = cardData as HumanCardData;

        string isFedText = isFed ? "Yes" : "No";
        string lifeColour = ColorUtility.ToHtmlStringRGB(data.lifeColor);
        string ageColour = ColorUtility.ToHtmlStringRGB(data.ageColor);
        string attackColour = ColorUtility.ToHtmlStringRGB(data.attackColor);

        builder.Append($"<color=#{lifeColour}>{"Life : "}").Append(currentLife).Append("</color>").AppendLine();
        builder.Append($"<color=#{ageColour}>{"Age : "}").Append(currentAge).Append("</color>").AppendLine();
        builder.Append($"<color=#{attackColour}>{"Attack : "}").Append(atk).Append("</color>");

        return builder.ToString();
    }
}
