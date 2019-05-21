using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StorageDropZone : DropZone_Base, IPointerDownHandler
{
    private Storage storage;

    public bool hasRessource = false;
    public string ressourceName = "";

    private void Start()
    {
        storage = Storage.instance;
    }

    protected override void DropCard(Draggable d, Card c)
    {
        ToolCardData toolData;
        RessourceCardData ressourceData;

        if ((ressourceData = (c.cardData as RessourceCardData)) || (toolData = (c.cardData as ToolCardData)))
        {
            if (storage.CheckStorage())
            {
                Debug.Log("storage.AddItemToStorage(d, c)");
                storage.AddItemToStorage(d, c);
            }
        }
    }

    /// <summary>
    /// Add a card to the DropZone
    /// </summary>
    public void AddCard(Draggable d, Card c)
    {
        if (hasRessource)
        {
            UpdateNbDisplay();
            Destroy(c.gameObject);
        }
        else
        {
            CreateStorageBoardCard(d, c);
            ressourceName = c.cardData.cardName;
            UpdateNbDisplay();
            hasRessource = true;
        }
    }

    private void CreateStorageBoardCard(Draggable d, Card c)
    {
        //Set the card parent to this
        d.parentToReturnTo = this.transform;
        Destroy(d.placeholder);

        GameObject image = Instantiate(Storage.instance.storageImage, transform);
        image.GetComponent<Image>().sprite = c.cardData.artwork;
        Destroy(c.gameObject);
        //isEmpty = false;
    }

    /// <summary>
    /// Call when the last ressource is removed
    /// </summary>
    public void ResetDZ()
    {
        Destroy(transform.GetChild(0).gameObject);
        hasRessource = false;
    }

    /// <summary>
    /// Update the number of ressouces on this DropZone
    /// </summary>
    public void UpdateNbDisplay()
    {
        GameManager.instance.ClearConsole();
        Debug.Log("There is " + storage.storage[ressourceName] + " of " + ressourceName);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) && hasRessource)
        {
            storage.RemoveItemFromStorage(ressourceName, this);
            DeckManager.instance.AddCard(ressourceName);
            DeckManager.instance.UpdateCardInHandCount();
        }
    }
}
