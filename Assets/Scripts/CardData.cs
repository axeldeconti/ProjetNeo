using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New card", menuName = "CardData")]
public class CardData : ScriptableObject {

    public enum CardType { Human, Ressource, Tool}

    public CardType cardType;
    public new string name;
    public Sprite artwork;
}
