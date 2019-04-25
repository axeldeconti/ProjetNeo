using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanDrawManager : MonoBehaviour {

    public Section[] sections;

    /// <summary>
    /// Compute how many humans have to be drawn
    /// </summary>
    public int ComputeHumanToDraw()
    {
        int nb = CardManager.instance.GetAllCardsOfType(CardType.Human).Count;
        float proba = 0f;

        foreach (Section section in sections)
        {
            if(nb >= section.min && nb <= section.max)
            {
                proba = section.spawnProba;
                break;
            }
        }

        int couples = nb / 2;
        int nbHumansToDraw = 0;

        for (int i = 0; i < couples; i++)
        {
            if(UnityEngine.Random.Range(0f, 100f) <= proba)
            {
                nbHumansToDraw++;
            }
        }

        return nbHumansToDraw;
    }
}

/// <summary>
/// Struc for the different section for human spawn
/// </summary>
[Serializable]
public struct Section
{
    public string section;
    public int min, max;
    public float spawnProba;
}
