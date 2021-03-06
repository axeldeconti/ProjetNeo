﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FeedingManager : MonoBehaviour
{

    #region Singleton

    public static FeedingManager instance { get; private set; }

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

    public GameObject humanSelectionScreen, humanToSelectPrefab;
    public int nbOfHumanFedByOneFood = 3;
    public Sprite background;

    private bool canFeedHuman;
    private List<int> humansSelected;

    private void Start()
    {
        humansSelected = new List<int>();

        canFeedHuman = false;
    }

    /// <summary>
    /// Feed all Humans selected
    /// </summary>
    private void FeedHumans()
    {
        foreach (int humanToFeed in humansSelected)
        {
            CardManager.instance.allHumanCards[humanToFeed].GetComponent<Human>().FeedMe();
        }

        humansSelected.Clear();

        CloseFeedingScreen();
    }

    /// <summary>
    /// Feed all Humans from the list in parameter
    /// </summary>
    private void FeedHumans(List<int> humanList)
    {
        foreach (int humanToFeed in humanList)
        {
            CardManager.instance.allHumanCards[humanToFeed].GetComponent<Human>().FeedMe();
        }

        CloseFeedingScreen();
    }

    /// <summary>
    /// Add a Human to the selection
    /// </summary>
    public void AddHumanSelected(int humanID)
    {
        if (!humansSelected.Contains(humanID))
        {
            humansSelected.Add(humanID);
        }

        if(humansSelected.Count >= nbOfHumanFedByOneFood)
        {
            FeedHumans();
        }
    }

    /// <summary>
    /// Remove a Human from selection
    /// </summary>
    public void RemoveHumanSelected(int humanID)
    {
        if (humansSelected.Contains(humanID))
        {
            humansSelected.Remove(humanID);
        }
    }

    /// <summary>
    /// Open the feeding screen and initialize it
    /// </summary>
    public void OpenFeedingScreen()
    {
        humanSelectionScreen.SetActive(true);
        humanSelectionScreen.transform.GetChild(0).GetComponent<Image>().sprite = background;

        List<int> humansAdded = new List<int>();

        foreach (GameObject human in CardManager.instance.GetAllCardsOfType(CardType.Human))
        {
            if (!human.GetComponent<Human>().isFed)
            {
                GameObject HumanToFeed = Instantiate(humanToSelectPrefab, humanSelectionScreen.transform.GetChild(1));
                HumanToFeed.GetComponent<HumanToFeed>().Init(human.GetComponent<Human>(), true);
                humansAdded.Add(human.GetInstanceID());
            }
        }

        if (humansAdded.Count <= nbOfHumanFedByOneFood)
            FeedHumans(humansAdded);
    }

    /// <summary>
    /// Close the feeding screen and clear it
    /// </summary>
    public void CloseFeedingScreen()
    {
        humanSelectionScreen.SetActive(false);

        foreach (Transform child in humanSelectionScreen.transform.GetChild(1))
        {
            Destroy(child.gameObject);
        }

        humansSelected.Clear();
    }

    public bool GetCanFeedHuman()
    {
        canFeedHuman = false;

        if (CardManager.instance.allHumanCards.Count != 0)
        {
            foreach(GameObject h in CardManager.instance.allHumanCards.Values)
            {
                if (!h.GetComponent<Human>().isFed)
                {
                    canFeedHuman = true;
                    break;
                }
            }
        }

        return canFeedHuman;
    }
}
