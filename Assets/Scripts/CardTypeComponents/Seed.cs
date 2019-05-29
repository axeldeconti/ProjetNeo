using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New card", menuName = "CardData/SeedCardData")]
public class Seed : RessourceCardData {

    public SeedStage[] stages;
    public SeedStage actualStage;
    public RessourceCardData droppedRessource;
    [HideInInspector] public AgriculturalSquare mySquare;

    public void Grow()
    {
        for (int i = 0; i < stages.Length; i++)
        {
            if(stages[i].duration > 0)
            {
                if(stages[i].duration-- == 0)
                {

                }
                else
                {

                }

                return;
            }
        }
    }

    public void Harvest()
    {

    }
}

[Serializable]
public struct SeedStage
{
    public int duration;
    public Sprite stageSprite;
}
