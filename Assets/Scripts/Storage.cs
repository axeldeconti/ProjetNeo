using System.Collections.Generic;
using UnityEngine;

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

    public int maxStorage = 20;
    public GameObject NbUnitePrefab, storageImage;
    public StorageDropZone[] dropzones;
    public Dictionary<string, int> storage = new Dictionary<string, int>();

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
                dz.UpdateNbDisplay();
            }
        }
    }
}
