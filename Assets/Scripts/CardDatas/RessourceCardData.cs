using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New card", menuName = "RessourceCardData")]
public class RessourceCardData : CardData {

    public bool canBeDrawn;
    public bool isFood;
}

/// <summary>
/// Struct to serve as recipe in Unity
/// </summary>
[SerializeField]
public struct RessourceForRecipe
{
    public string name;
    public int qte;
}
