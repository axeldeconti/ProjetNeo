using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Building : CardTypeComponent, IPointerDownHandler {

    protected List<GameObject> ressourceList = new List<GameObject>();
    protected List<RessourceCardData> ressourceDataList = new List<RessourceCardData>();

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
        GetComponent<Image>().sprite = cardData.notBuild;
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

    public bool AddRessource(RessourceCardData ressourceData)
    {
        string cardName = ressourceData.cardName;
        bool isRessource1 = false;

        if (!(cardName == cardData.ressource1 || cardName == cardData.ressource2))
            return false;
        else if (cardName == cardData.ressource1)
            isRessource1 = true;

        int nbOfRessource = 0;

        foreach (RessourceCardData data in ressourceDataList)
        {
            if (data.cardName == cardName)
                nbOfRessource++;
        }

        if (isRessource1 && nbOfRessource >= cardData.nbRessource1)
            return false;
        else if (!isRessource1 && nbOfRessource >= cardData.nbRessource2)
            return false;

        ressourceDataList.Add(ressourceData);

        CheckIfBuilt();

        return true;
    }

    public  void CheckIfBuilt()
    {
        int nb = cardData.nbRessource1 + cardData.nbRessource2;

        if (nb <= ressourceDataList.Count)
            BuildBuilding();
    }

    public void BuildBuilding()
    {
        Debug.Log(cardData.cardName + " is built");

        Storage.instance.maxStorage += cardData.storageIncrease;
        Storage.instance.UpdateNbOfItem();

        GetComponent<Image>().sprite = cardData.artwork;
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
