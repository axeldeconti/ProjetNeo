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
    public GameObject pauseMenu, gameOverScreen, winScreen;
    public GameObject helpPanel, burnAllUI;
    public Sprite invisibleSprite;
    public Transform AgriSquareScreen;
    public bool hasTuto;

    /// <summary>
    /// Call to start a new turn
    /// </summary>
    public void StartTurn()
    {
        ClearConsole();
        EventManager.instance.canGoToNextLevel = false;
        EventManager.instance.ButtonsCloseTree.gameObject.SetActive(true);

        if (hasTuto)
        {
            TutoGameManager.instance.StartTurn();
            return;
        }

        DeckManager.instance.StartTurn();

        foreach (GameObject card in CardManager.instance.GetAllCards())
        {
            card.GetComponent<CardTypeComponent>().StartTurn();
        }

        foreach (GameObject h in CardManager.instance.allHumanCards.Values)
        {
            if(h.GetComponent<Human>().metier == HumanMetier.Farmer)
            {
                foreach (GameObject building in CardManager.instance.allBuildingCards.Values)
                {
                    AgriculturalSquare agri = building.GetComponent<AgriculturalSquare>();
                    if (agri != null)
                        agri.StartTurn();
                }

                break;
            }
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

        DeckManager.instance.endTurnButton.SetActive(false);

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
        gameOverScreen.SetActive(true);
    }

    public void WinGame()
    {
        Debug.Log("You won !");
        winScreen.SetActive(true);
    }

    public void BurnAll()
    {
        DeckManager.instance.ClearHand();
        burnAllUI.SetActive(false);
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

    public void OnClickHelp()
    {
        helpPanel.SetActive(true);
    }

    public void OnQuitHelp()
    {
        helpPanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
            TogglePauseMenu();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            foreach (GameObject human in CardManager.instance.GetAllCardsOfType(CardType.Human))
            {
                Debug.Log(human.gameObject.GetInstanceID());
            }
        }
    }
}
