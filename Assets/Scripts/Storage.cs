using System.Collections;
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
    public int sumRSC = 0;

    public bool isFull;
    public bool isEmpty;

    private Dictionary<string, int> storage = new Dictionary<string, int>();

    // Use this for initialization
    void Start ()
    {

    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public bool CheckStorage()
    {
        if (sumRSC < maxStorage)
            isFull = false;

        else
            isFull = true;

        return isFull;
    }

    public int SumAllRSC ()
    {
        foreach (string item in storage.Keys)
        {
            sumRSC += storage[item];
        }
        return sumRSC;
    }

    public void AddItemToStorage(string item)
    {
        if (CheckStorage())
        {
            if (storage.ContainsKey(item))
            {
                storage.Add(item, storage[item]++);
            }

            else
            {
                storage.Add(item, 1);
            }
        }
    }

    public void RemoveItemFromStorage(string item)
    {
        isEmpty = false;
        if (storage.ContainsKey(item))
        {
            storage.Add(item, storage[item]--);
            if (storage.ContainsValue(0))
            {
                storage.Remove(item);
                isEmpty = true;
            }
        }
    }
}
