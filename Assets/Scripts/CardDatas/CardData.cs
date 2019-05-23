using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardData : ScriptableObject {

    public CardType cardType;
    public Sprite frame, artwork;
    public string cardName;
    public AudioClip soundEffect;

    /// <summary>
    /// Call to apply the effect of the card
    /// </summary>
    public virtual void ApplyCardEffect() { }
}

/// <summary>
/// All type a card can be
/// </summary>
public enum CardType { Human, Ressource, Tool, Building, Event }
