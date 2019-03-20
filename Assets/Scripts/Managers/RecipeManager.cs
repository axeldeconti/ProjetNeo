using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeManager : MonoBehaviour
{
    #region Singleton

    public static RecipeManager instance { get; private set; }

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

    public AllRecipeStruct[] recipes;
    public Dictionary<int, CardData> allRecipe;

    private void Start()
    {
        //Init the allRecipe dictionary
        allRecipe = new Dictionary<int, CardData>();

        foreach (AllRecipeStruct recipe in recipes)
        {
            allRecipe.Add(recipe.recipeID, recipe.outcome);
        }
    }
}

/// <summary>
/// Struct to serve as input for allRecipe in Unity
/// </summary>
[Serializable]
public struct AllRecipeStruct
{
    public string name;
    public int recipeID; //The order for the ID is : Fur | Cloth | Bone | Clay | Stone | Wood
    public CardData outcome;
}
