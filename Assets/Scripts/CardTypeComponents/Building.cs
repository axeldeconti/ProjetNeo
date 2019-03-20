using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : CardTypeComponent {

    private List<RessourceCardData> ressourceList = new List<RessourceCardData>();

    public GameObject dropZone;
    public BuildingCardData cardData;
    public BuildingType type;

    public override void Init(CardData _cardData)
    {
        BuildingCardData tmp;

        if (tmp = (_cardData as BuildingCardData))
        {
            cardData = tmp;
        }

        type = cardData.buildingType;
    }

    public override void InitBoardCard(CardTypeComponent _cardTypeComp)
    {
        Building buildingComp, tmp;

        if (tmp = (_cardTypeComp as Building))
        {
            buildingComp = tmp;
        }
        else
        {
            Debug.Log("Not BuildingComponent in InitBoardCard");
            buildingComp = null;
        }

        cardData = buildingComp.cardData;
        type = buildingComp.type;

        switch (type)
        {
            case BuildingType.Workbench:
                gameObject.AddComponent<Workbench>();
                break;
            case BuildingType.Agricultural_Square:
                break;
            case BuildingType.Stone_circle:
                break;
            case BuildingType.Hut:
                break;
            case BuildingType.Cabin:
                break;
            case BuildingType.Bone_cabin:
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Add a ressource to this building
    /// </summary>
    public virtual void AddRessource(RessourceCardData ressourceToAdd)
    {
        ressourceList.Add(ressourceToAdd);

        GameManager.instance.ClearConsole();
        foreach (RessourceCardData data in ressourceList)
        {
            Debug.Log(data.cardName);
        }
    }

    /// <summary>
    /// Remove a ressource from this building
    /// </summary>
    public virtual void RemoveRessource(GameObject ressourceToRemove)
    {
        ressourceList.Remove(ressourceToRemove.GetComponent<Ressource>().cardData);

        GameManager.instance.ClearConsole();
        foreach (RessourceCardData data in ressourceList)
        {
            Debug.Log(data.cardName);
        }
    }
}
