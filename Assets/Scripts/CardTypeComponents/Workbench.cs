using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Workbench : Building {

    public int currentRecipeID = 000000;

    public override void AddRessource(RessourceCardData ressourceToAdd)
    {
        base.AddRessource(ressourceToAdd);

        switch (ressourceToAdd.cardName)
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
                Debug.Log("Can't add this card to the workbench : " + ressourceToAdd.cardName);
                break;
        }

        UpdateCurrentRecipe();
    }

    public override void RemoveRessource(GameObject ressourceToRemove)
    {
        base.RemoveRessource(ressourceToRemove);

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

        DeckManager.instance.AddCard(data.cardName);
        Destroy(ressourceToRemove);
    }

    public void UpdateCurrentRecipe()
    {
        if (RecipeManager.instance.allRecipe.ContainsKey(currentRecipeID))
        {
            Debug.Log(RecipeManager.instance.allRecipe[currentRecipeID].cardName);
        }
        else
        {
            Debug.Log("No matching recipe");
        }
        
    }
}
