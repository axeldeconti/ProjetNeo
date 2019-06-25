using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[CreateAssetMenu(fileName = "New card", menuName = "CardData/RessourceCardData")]
public class RessourceCardData : CardData {

    public bool canBeDrawn;
    public bool isFood;

    public override string GetTooltipInfoText()
    {
        StringBuilder builder = new StringBuilder();

        builder.Append("<size=35>").Append(ColouredName).Append("</size>").AppendLine();
        builder.Append("Ressource");
        //builder.Append("Description : ").Append(tooltipDescription).AppendLine();

        return builder.ToString();
    }
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
