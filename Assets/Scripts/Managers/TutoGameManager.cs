using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoGameManager : MonoBehaviour {

    #region Singleton

    public static TutoGameManager instance { get; private set; }

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

    public int turn;

    private void Start()
    {
        turn = 1;

        StartTurn();
    }

    public void StartTurn()
    {
        switch (turn)
        {
            case 1:
                DeckManager.instance.AddCard("Human");
                DeckManager.instance.AddCard("Human");
                DeckManager.instance.AddCard("Human");
                DeckManager.instance.AddCard("Berry");
                DeckManager.instance.AddCard("Wood");
                DeckManager.instance.AddCard("Stone");
                break;
            case 2:
                DeckManager.instance.AddCard("Human");
                DeckManager.instance.AddCard("Human");
                DeckManager.instance.AddCard("Meat");
                DeckManager.instance.AddCard("Meat");
                DeckManager.instance.AddCard("Cloth");
                DeckManager.instance.AddCard("Bone");
                break;
            case 3:
                break;
            default:
                break;
        }

        turn++;
    }
}
