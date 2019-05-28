using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StorageDropZone : DropZone_Base, IPointerDownHandler
{
    private Storage storage;
    private CardData data;

    public bool hasRessource = false;
    public string ressourceName = "";
    public int nbOfRessouces;
    public Text nbOfRessourcesText;

    private void Start()
    {
        storage = Storage.instance;
        nbOfRessouces = 0;
        nbOfRessourcesText.text = "";
    }

    protected override void DropCard(Draggable d, Card c)
    {
        ToolCardData toolData;
        RessourceCardData ressourceData;

        if ((ressourceData = (c.cardData as RessourceCardData)) || (toolData = (c.cardData as ToolCardData)))
        {
            if (storage.CheckStorage())
            {
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
            nbOfRessouces++;
            Destroy(c.gameObject);
        }
        else
        {
            CreateStorageBoardCard(d, c);
            data = c.cardData;
            ressourceName = data.cardName;
            nbOfRessouces = 1;
            hasRessource = true;
        }

        UpdateNbDisplay();
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
        Destroy(transform.GetChild(1).gameObject);
        hasRessource = false;
        nbOfRessouces = 0;
        nbOfRessourcesText.text = "";
        data = null;
        TooltipPopup.instance.HideInfo();
    }

    /// <summary>
    /// Update the number of ressouces on this DropZone
    /// </summary>
    public void UpdateNbDisplay()
    {
        nbOfRessourcesText.text = nbOfRessouces.ToString();

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

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);

        if (hasRessource)
            TooltipPopup.instance.DisplayInfo(data);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);

        if (hasRessource)
            TooltipPopup.instance.HideInfo();
    }
}
