using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Workbench : MonoBehaviour, IPointerDownHandler {

    private Image outcomeImage;
    public WbOutcome wbOutcome;
    public GameObject dropZones;
    public List<GameObject> ressourceList;
    public Sprite outcomeNull;
    public int currentRecipeID = 0;
    public AudioClip buildSFX;
    public Button burnAllButton, endButton;
    
    /// <summary>
    /// Init the workbench
    /// </summary>
    private void Start()
    {
        ressourceList = new List<GameObject>();
        wbOutcome.wb = this;
    }

    public void AddRessource(GameObject ressourceToAdd)
    {
        ressourceList.Add(ressourceToAdd);

        GameManager.instance.ClearConsole();
        foreach (GameObject item in ressourceList)
        {
            Debug.Log(item.GetComponent<Ressource>().cardData.cardName);
        }

        RessourceCardData data = ressourceToAdd.GetComponent<Ressource>().cardData;

        switch (data.cardName)
        {
            case "Wood":
                currentRecipeID += 1;
                break;
            case "Stone":
                currentRecipeID += 10;
                break;
            case "Clay":
                currentRecipeID += 100;
                break;
            case "Bone":
                currentRecipeID += 1000;
                break;
            case "Cloth":
                currentRecipeID += 10000;
                break;
            case "Fur":
                currentRecipeID += 100000;
                break;
            default:
                if (data.isFood)
                {
                    currentRecipeID += 1000000;
                } else
                {
                    Debug.Log("Can't add this card to the workbench : " + data.cardName);
                    RemoveRessource(ressourceToAdd, true);
                }
                break;
        }

        UpdateCurrentRecipe();
        DeckManager.instance.UpdateCardInHandCount();
    }

    public void RemoveRessource(GameObject ressourceToRemove, bool giveBackRessource)
    {
        ressourceToRemove.transform.parent.GetComponent<DropZone_Base>().isEmpty = true;

        CardManager.instance.RemoveCard(ressourceToRemove);

        ressourceList.Remove(ressourceToRemove);

        GameManager.instance.ClearConsole();
        foreach (GameObject item in ressourceList)
        {
            Debug.Log(item.GetComponent<Ressource>().cardData.cardName);
        }

        RessourceCardData data = ressourceToRemove.GetComponent<Ressource>().cardData;

        switch (data.cardName)
        {
            case "Wood":
                currentRecipeID -= 1;
                break;
            case "Stone":
                currentRecipeID -= 10;
                break;
            case "Clay":
                currentRecipeID -= 100;
                break;
            case "Bone":
                currentRecipeID -= 1000;
                break;
            case "Cloth":
                currentRecipeID -= 10000;
                break;
            case "Fur":
                currentRecipeID -= 100000;
                break;
            default:
                if (data.isFood)
                {
                    currentRecipeID -= 1000000;
                }
                else
                {
                    Debug.Log("Can't add this card to the workbench : " + data.cardName);
                }
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

            DeckManager.instance.canEndTurn = false;
        }
        else
        {
            Debug.Log("No matching recipe");
            wbOutcome.hasOutcome = false;
            wbOutcome.image.sprite = outcomeNull;

            if (currentRecipeID == 0)
                DeckManager.instance.canEndTurn = true;
            else DeckManager.instance.canEndTurn = false;
        }
        
    }

    /// <summary>
    /// Call when the workbench is clicked on
    /// </summary>
    public void OnPointerDown(PointerEventData eventData)
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

    /// <summary>
    /// Remove all ressources from this building
    /// </summary>
    public void RemoveAllRessources(bool giveBackRessources)
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

        currentRecipeID = 0;
        UpdateCurrentRecipe();
        DeckManager.instance.UpdateCardInHandCount();
    }

    public void ToggleWorkbench()
    {
        if (gameObject.activeSelf)
            RemoveAllRessources(true);

        gameObject.SetActive(!gameObject.activeSelf);
        burnAllButton.interactable = !burnAllButton.interactable;
        endButton.interactable = !endButton.interactable;

        EventSystem.current.SetSelectedGameObject(null);
    }
}
