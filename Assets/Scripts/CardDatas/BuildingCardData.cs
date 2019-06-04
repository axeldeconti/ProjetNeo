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
        return "";
    }
}

/// <summary>
/// All the building that can be build
/// </summary>
public enum BuildingType { Workbench, Agricultural_Square, Stone_circle, Hut, Cabin, Bone_cabin }
