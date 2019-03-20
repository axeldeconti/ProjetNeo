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
    /// Call at the end of a turn
    /// </summary>
    public void StartTurn()
    {
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
        foreach (GameObject card in CardManager.instance.GetAllCards())
        {
            //Debug.Log(card.GetComponent<BoardCard>().cardData.cardName + " "  + card.GetComponent<BoardCard>().GetInstanceID().ToString() + " end turn");
            card.GetComponent<CardTypeComponent>().EndTurn();
        }
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
}
