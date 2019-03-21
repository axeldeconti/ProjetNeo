using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WbOutcome : MonoBehaviour, IPointerDownHandler
{

    public Workbench wb;
    public Image image;
    public bool hasOutcome = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && hasOutcome)
        {
            DeckManager.instance.AddCard(RecipeManager.instance.allRecipe[wb.currentRecipeID].cardName);
            wb.RemoveAllRessources(false);
        }
    }
}
