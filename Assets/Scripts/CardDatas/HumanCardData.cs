using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New card", menuName = "CardData/HumanCardData")]
public class HumanCardData : CardData {

    public int minAge, maxAge, minLife, maxLife, minAtk, maxAtk; 
}
