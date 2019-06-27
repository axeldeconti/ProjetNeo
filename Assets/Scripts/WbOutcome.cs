using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WbOutcome : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{

    public Workbench wb;
    public Image image;
    public bool hasOutcome = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && hasOutcome)
        {
            Card c = DeckManager.instance.AddCard(RecipeManager.instance.allRecipe[wb.currentRecipeID].cardName);
            c.transform.SetAsFirstSibling();
            wb.RemoveAllRessources(false);
            AudioManager.instance.PlaySoundEffects(wb.buildSFX);
            TooltipPopup.instance.HideInfo();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (hasOutcome)
            TooltipPopup.instance.DisplayInfo(RecipeManager.instance.allRecipe[wb.currentRecipeID]);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (hasOutcome)
            TooltipPopup.instance.HideInfo();
    }
}
