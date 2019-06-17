using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New card", menuName = "CardData/SeedCardData")]
public class Seed : RessourceCardData {

    public SeedStage[] stages;
    public SeedStage actualStage;
    public RessourceCardData droppedRessource;
    [HideInInspector] public int currentIndex = 0;
    [HideInInspector] public bool isDone = false;

    public void Grow()
    {
        actualStage.duration--;
        if (actualStage.duration <= 0 && !isDone)
        {
            if (currentIndex < (stages.Length - 1))
            {
                currentIndex++;
                actualStage = stages[currentIndex];
            }

            if (currentIndex == (stages.Length - 1))
                isDone = true;
        }

        Debug.Log(cardName + " at stage " + currentIndex + " and is done = " + isDone);
    }
}

[Serializable]
public struct SeedStage
{
    public int duration;
    public Sprite stageSprite;
}
