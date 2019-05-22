using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public int turn = 1;
    public EventEffect eventEffect = null;
    public GameObject pauseMenu;

    /// <summary>
    /// Call to start a new turn
    /// </summary>
    public void StartTurn()
    {
        DeckManager.instance.StartTurn();
        EventManager.instance.ButtonsCloseTree.gameObject.SetActive(true);

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
        turn++;
        EventManager.instance.canGoToNextLevel = true;
        EventManager.instance.ButtonsCloseTree.gameObject.SetActive(false);

        DeckManager.instance.endButton.interactable = false;

        foreach (GameObject card in CardManager.instance.GetAllCards())
        {
            //Debug.Log(card.GetComponent<BoardCard>().cardData.cardName + " "  + card.GetComponent<BoardCard>().GetInstanceID().ToString() + " end turn");
            card.GetComponent<CardTypeComponent>().EndTurn();
        }

        CheckForGameOver();

        EventManager.instance.OpenEventTreeScreen();

        if (eventEffect)
        {
            eventEffect.UnapplyEffect();
            eventEffect = null;
        }
    }

    public void GoToScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void TogglePauseMenu()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
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
        if (Input.GetButtonDown("Cancel"))
            TogglePauseMenu();
    }
}
