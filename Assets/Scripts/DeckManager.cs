using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour {

    #region Singleton

    public static DeckManager instance;

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

    public int nbCard;

    private void Start()
    {
        for (int i = 0; i < nbCard; i++)
        {
            CardManager.instance.CreateCard();
        }
    }
}
