using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New card", menuName = "CardData/ToolCardData")]
public class ToolCardData : CardData {

    public HumanMetier metier;
    public int bonusAtk, bonusLife;
}
