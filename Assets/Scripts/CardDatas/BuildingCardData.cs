using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New card", menuName = "BuildingCardData")]
public class BuildingCardData : CardData {

    public BuildingType buildingType;
}

/// <summary>
/// All the building that can be build
/// </summary>
public enum BuildingType { Workbench, Agricultural_Square, Stone_circle, Hut, Cabin, Bone_cabin }
