using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AgriculturalSquare : MonoBehaviour {

    public Seed[] seeds;
    public SeedDropZone[] seedDZ;
    public Image[] boardImages;

	public void Init()
    {
        for (int i = 0; i < seedDZ.Length; i++)
        {
            seedDZ[i].nb = i;
        }
    }

    public void StartTurn()
    {
        foreach (GameObject h in CardManager.instance.GetAllCardsOfType(CardType.Human))
        {
            if (h.GetComponent<Human>().metier == HumanMetier.Farmer)
                break;
            else return;
        }

        for (int i = 0; i < seeds.Length; i++)
        {
            if(seeds[i] != null)
            {
                seeds[i].Grow();
                boardImages[i].sprite = seeds[i].actualStage.stageSprite;
            }              
        }
    }

    public void AddSeed(Seed seedToAdd, SeedDropZone dz)
    {
        seeds[dz.nb] = Instantiate<Seed>(seedToAdd);
        seeds[dz.nb].mySquare = this;
        seeds[dz.nb].actualStage = seeds[dz.nb].stages[0];
    }

    public void Harvest(SeedDropZone dz)
    {
        DeckManager.instance.AddCard(seeds[dz.nb].droppedRessource.cardName);

        int nb = dz.nb;
        Destroy(seeds[nb]);
        seeds[nb] = null;        
    }
}
