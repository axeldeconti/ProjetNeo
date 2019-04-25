using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Building : CardTypeComponent, IPointerDownHandler {

    protected List<GameObject> ressourceList = new List<GameObject>();

    public GameObject dropZones, wbo;
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

        wbo.SetActive(false);
        dropZones.SetActive(false);

        cardData = buildingComp.cardData;
        type = buildingComp.type;

        switch (type)
        {
            case BuildingType.Workbench:
                gameObject.AddComponent<Workbench>().Init(this);
                Destroy(this);
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
    public virtual void AddRessource(GameObject ressourceToAdd)
    {
        ressourceList.Add(ressourceToAdd);

        GameManager.instance.ClearConsole();
        foreach (GameObject item in ressourceList)
        {
            Debug.Log(item.GetComponent<Ressource>().cardData.cardName);
        }
    }

    /// <summary>
    /// Remove a ressource from this building
    /// </summary>
    public virtual void RemoveRessource(GameObject ressourceToRemove, bool giveBackRessource)
    {
        ressourceToRemove.transform.parent.GetComponent<DropZone_Base>().isEmpty = true;

        CardManager.instance.RemoveCard(ressourceToRemove);

        ressourceList.Remove(ressourceToRemove);

        GameManager.instance.ClearConsole();
        foreach (GameObject item in ressourceList)
        {
            Debug.Log(item.GetComponent<Ressource>().cardData.cardName);
        }
    }

    /// <summary>
    /// Remove all ressources from this building
    /// </summary>
    public virtual void RemoveAllRessources(bool giveBackRessources)
    {
        while (ressourceList.Count > 0)
        {
            if (ressourceList[0])
            {
                ressourceList[0].GetComponent<Ressource>().RemoveCard();
                RemoveRessource(ressourceList[0], giveBackRessources);
            }
            else
            {
                Debug.LogError("Infinity loop");
                break;
            }
        }
    }

    /// <summary>
    /// Allow to clic on the image
    /// </summary>
    public virtual void OnPointerDown(PointerEventData eventData)
    {
        
    }
}
