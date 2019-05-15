using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour {

    #region Singleton

    public static GameManager instance { get; private set; }

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

    /// <summary>
    /// Call to start a new turn
    /// </summary>
    public void StartTurn()
    {
        DeckManager.instance.StartTurn();

        foreach (GameObject card in CardManager.instance.GetAllCards())
        {
            card.GetComponent<CardTypeComponent>().StartTurn();
        }
    }

    /// <summary>
    /// Call to end the turn
    /// </summary>
    public void EndTurn()
    {
        DeckManager.instance.endButton.interactable = false;

        foreach (GameObject card in CardManager.instance.GetAllCards())
        {
            //Debug.Log(card.GetComponent<BoardCard>().cardData.cardName + " "  + card.GetComponent<BoardCard>().GetInstanceID().ToString() + " end turn");
            card.GetComponent<CardTypeComponent>().EndTurn();
        }

        //Remove when the event tree is done
        StartTurn();
    }

    /// <summary>
    /// Check if the game is over
    /// </summary>
    public void CheckForGameOver()
    {
        if(CardManager.instance.allHumanCards.Count <= 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        Debug.Log("Game Over !");
    }

    /// <summary>
    /// Clear the Unity console
    /// </summary>
    public void ClearConsole()
    {
        
        var assembly = Assembly.GetAssembly(typeof(SceneView));
        var type = assembly.GetType("UnityEditor.LogEntries");
        var method = type.GetMethod("Clear");
        method.Invoke(new object(), null);
        
    }

    public void ExitGame()
    {
        Debug.Log("Exit Game !");
        Application.Quit();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            ExitGame();
    }
}
