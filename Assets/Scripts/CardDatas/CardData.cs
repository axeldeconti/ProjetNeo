using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardData : ScriptableObject {

    public CardType cardType;
    public Sprite frame, artwork;

    public virtual void ApplyCardEffect() { }
}

public enum CardType { Human, Ressource, Tool, Building, Event }
