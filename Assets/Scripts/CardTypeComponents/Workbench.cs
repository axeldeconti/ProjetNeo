using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Workbench : Building {

    private Image outcomeImage;
    private WbOutcome wbOutcome;

    public int currentRecipeID = 000000;
    
    /// <summary>
    /// Init the workbench
    /// </summary>
    public void Init(Building b)
    {
        type = b.type;
        ressourceList = new List<GameObject>();
        dropZones = b.dropZones;
        wbo = b.wbo;
        cardData = b.cardData;

        wbOutcome = wbo.GetComponent<WbOutcome>();
        wbOutcome.wb = this;
    }

    public override void AddRessource(GameObject ressourceToAdd)
    {
        base.AddRessource(ressourceToAdd);

        RessourceCardData data = ressourceToAdd.GetComponent<Ressource>().cardData;

        switch (data.cardName)
        {
            case "Wood":
                currentRecipeID += 1;
                break;
            case "Stone":
                currentRecipeID += 10;
                break;
            case "Cloth":
                currentRecipeID += 100;
                break;
            case "Fur":
                currentRecipeID += 1000;
                break;
            case "Bone":
                currentRecipeID += 10000;
                break;
            case "Clay":
                currentRecipeID += 100000;
                break;
            default:
                Debug.Log("Can't add this card to the workbench : " + data.cardName);
                RemoveRessource(ressourceToAdd, true);
                break;
        }

        UpdateCurrentRecipe();
    }

    public override void RemoveRessource(GameObject ressourceToRemove, bool giveBackRessource)
    {
        base.RemoveRessource(ressourceToRemove, giveBackRessource);

        RessourceCardData data = ressourceToRemove.GetComponent<Ressource>().cardData;

        switch (data.cardName)
        {
            case "Wood":
                currentRecipeID -= 1;
                break;
            case "Stone":
                currentRecipeID -= 10;
                break;
            case "Cloth":
                currentRecipeID -= 100;
                break;
            case "Fur":
                currentRecipeID -= 1000;
                break;
            case "Bone":
                currentRecipeID -= 10000;
                break;
            case "Clay":
                currentRecipeID -= 100000;
                break;
            default:
                Debug.Log("Can't add this card to the workbench : " + data.cardName);
                break;
        }

        UpdateCurrentRecipe();

        if (giveBackRessource)
            DeckManager.instance.AddCard(data.cardName);

        Destroy(ressourceToRemove);
    }

    /// <summary>
    /// Update the outcome of workbench for the current recipe
    /// </summary>
    public void UpdateCurrentRecipe()
    {
        if (RecipeManager.instance.allRecipe.ContainsKey(currentRecipeID))
        {
            Debug.Log(RecipeManager.instance.allRecipe[currentRecipeID].cardName);
            wbOutcome.hasOutcome = true;
            wbOutcome.image.sprite = RecipeManager.instance.allRecipe[currentRecipeID].artwork;
        }
        else
        {
            Debug.Log("No matching recipe");
            wbOutcome.hasOutcome = false;
            wbOutcome.image.sprite = null;
        }
        
    }

    /// <summary>
    /// Call when the workbench is clicked on
    /// </summary>
    public override void OnPointerDown(PointerEventData eventData)
    {
        if (GetComponent<BoardCard>() != null && Input.GetKeyDown(KeyCode.Mouse0))
        {
            bool isActive = dropZones.activeSelf;

            if (isActive)
            {
                RemoveAllRessources(true);
            }

            dropZones.SetActive(!isActive);
            wbOutcome.gameObject.SetActive(!isActive);
        }
    }
}
