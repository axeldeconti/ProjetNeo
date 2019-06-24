using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Storage : MonoBehaviour
{
    #region Singleton

    public static Storage instance { get; private set; }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    #endregion

    public int maxStorage = 6;
    public GameObject storageImage;
    public StorageDropZone[] dropzones;
    public Dictionary<string, int> storage = new Dictionary<string, int>();
    public Text nbOfItem;

    private void Start()
    {
        UpdateNbOfItem();
    }

    /// <summary>
    /// Checks if one ressource can be added
    /// </summary>
    public bool CheckStorage()
    {
        bool canAddCard;

        if (SumAllRSC() < maxStorage)
            canAddCard = true;
        else
            canAddCard = false;

        return canAddCard;
    }

    /// <summary>
    /// Compute and return all ressources in storage
    /// </summary>
    private int SumAllRSC()
    {
        int sumRSC = 0;
        foreach (string item in storage.Keys)
        {
            sumRSC += storage[item];
        }
        return sumRSC;
    }

    /// <summary>
    /// Add an item to the storage
    /// </summary>
    public void AddItemToStorage(Draggable d, Card c)
    {
        DeckManager.instance.UpdateCardInHandCount();

        string item = c.cardData.cardName;

        if (storage.ContainsKey(item))
        {
            storage[item]++;

            for (int i = 0; i < dropzones.Length; i++)
            {
                if (dropzones[i].hasRessource && dropzones[i].ressourceName == item)
                {
                    dropzones[i].nbOfRessouces++;
                    dropzones[i].UpdateNbDisplay();
                    d.parentToReturnTo = this.transform;
                    Destroy(d.placeholder);
                    Destroy(c.gameObject);
                    break;
                }

            }
        }
        else
        {
            storage.Add(item, 1);

            for (int i = 0; i < dropzones.Length; i++)
            {
                if (!dropzones[i].hasRessource)
                {
                    dropzones[i].AddCard(d, c);
                    break;
                }

            }
        }

        UpdateNbOfItem();

        if (DeckManager.instance.HandPannel.childCount <= 1)
        {
            DeckManager.instance.endTurnButton.SetActive(true);
            DeckManager.instance.burnAllButton.SetActive(false);
        }
    }

    /// <summary>
    /// Remove an item from the storage
    /// </summary>
    public void RemoveItemFromStorage(string item, StorageDropZone dz)
    {
        if (storage.ContainsKey(item))
        {
            if (storage[item] == 1)
            {
                dz.ResetDZ();
                storage.Remove(item);

                foreach (string _item in storage.Keys)
                {
                    Debug.Log(_item + storage[_item]);
                }
            }
            else
            {
                storage[item]--;
                dz.nbOfRessouces--;
                dz.UpdateNbDisplay();
            }
        }

        UpdateNbOfItem();
    }

    public void UpdateNbOfItem()
    {
        int nb = 0;

        for (int i = 0; i < dropzones.Length; i++)
        {
            if (dropzones[i].hasRessource)
            {
                nb += dropzones[i].nbOfRessouces;
            }
        }

        nbOfItem.text = nb.ToString() + "/" + maxStorage.ToString();
    }
}
