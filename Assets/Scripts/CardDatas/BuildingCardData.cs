using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[CreateAssetMenu(fileName = "New card", menuName = "CardData/BuildingCardData")]
public class BuildingCardData : CardData {

    public BuildingType buildingType;
    public Sprite notBuild;
    public int storageIncrease;
    public string ressource1, ressource2;
    public int nbRessource1 = 0, nbRessource2 = 0;

    public override string GetTooltipInfoText()
    {
        StringBuilder builder = new StringBuilder();

        builder.Append("Category : Building").AppendLine();
        builder.Append("Description : ").Append(tooltipDescription).AppendLine();
        builder.Append("Bonus storage : ").Append(storageIncrease).AppendLine();
        builder.Append("Craft : ").Append(nbRessource1).Append(" ").Append(ressource1);

        if (nbRessource2 != 0)
            builder.Append(" / ").Append(nbRessource2).Append(" ").Append(ressource2);

        return builder.ToString();
    }
}

/// <summary>
/// All the building that can be build
/// </summary>
public enum BuildingType { Workbench, Agricultural_Square, Stone_circle, Hut, Cabin, Bone_cabin }
