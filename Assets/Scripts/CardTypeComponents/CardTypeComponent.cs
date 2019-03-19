using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CardTypeComponent : MonoBehaviour {

    /// <summary>
    /// Init when the card is drawn
    /// </summary>
    public virtual void Init(CardData _cardData) { }

    /// <summary>
    /// Init when the card is dropped
    /// </summary>
    public virtual void InitBoardCard(CardTypeComponent _cardTypeComp) { }

    /// <summary>
    /// Call at the begining of a turn
    /// </summary>
    public virtual void StartTurn() { }

    /// <summary>
    /// Call at the end of a turn
    /// </summary>
    public virtual void EndTurn() { }

    /// <summary>
    /// Call to remove a card from all cards
    /// </summary>
    public virtual void RemoveCard()
    {
        CardManager.instance.RemoveCard(this.gameObject);
    }
}

/// <summary>
/// All the professions a human can be
/// </summary>
public enum HumanMetier { Harvester, ToolManufacturer, Woodcutter, Miner, Farmer, Hunter, WeaponManufacturer, Soldier}