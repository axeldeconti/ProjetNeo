using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AgriculturalSquare : MonoBehaviour, IPointerDownHandler
{

    public Seed[] seeds;
    public SeedDropZone[] seedDZ;
    public Image[] boardImages;
    public GameObject AgriScreenPrefab;
    public GameObject AgriScreen;

	public void Init()
    {
        AgriScreen = Instantiate(AgriScreenPrefab, GameManager.instance.AgriSquareScreen);

        seedDZ = new SeedDropZone[4];
        seeds = new Seed[4];

        for (int i = 0; i < seedDZ.Length; i++)
        {
            seedDZ[i] = AgriScreen.transform.GetChild(i).GetComponent<SeedDropZone>();
            seedDZ[i].mySquare = this;
            seedDZ[i].nb = i;
            seedDZ[i].GetComponent<Image>().sprite = GameManager.instance.invisibleSprite;
            boardImages[i].sprite = GameManager.instance.invisibleSprite;
        }

        AgriScreen.SetActive(false);
    }

    public void StartTurn()
    {
        for (int i = 0; i < seeds.Length; i++)
        {
            if(seeds[i] != null)
            {
                seeds[i].Grow();
                boardImages[i].sprite = seeds[i].actualStage.stageSprite;
                seedDZ[i].transform.GetChild(0).GetComponent<Image>().sprite = seeds[i].actualStage.stageSprite;
            }              
        }
    }

    public void AddSeed(Seed seedToAdd, SeedDropZone dz)
    {
        seeds[dz.nb] = Instantiate<Seed>(seedToAdd);
        seeds[dz.nb].actualStage = seeds[dz.nb].stages[0];
        seeds[dz.nb].currentIndex = 0;

        boardImages[dz.nb].sprite = seeds[dz.nb].actualStage.stageSprite;
        //seedDZ[dz.nb].transform.GetChild(0).GetComponent<Image>().sprite = seeds[dz.nb].actualStage.stageSprite;
    }

    public void Harvest(SeedDropZone dz)
    {
        if (seeds[dz.nb].isDone)
        {
            DeckManager.instance.AddCard(seeds[dz.nb].droppedRessource.cardName);

            int nb = dz.nb;
            Destroy(seeds[nb]);
            seeds[nb] = null;

            boardImages[nb].sprite = GameManager.instance.invisibleSprite;
            dz.transform.GetChild(0).GetComponent<Image>().sprite = GameManager.instance.invisibleSprite;
            dz.isEmpty = true;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (GetComponent<Building>().isBuilt)
        {
            AgriScreen.transform.parent.parent.gameObject.SetActive(true);
            AgriScreen.SetActive(true);
        }
    }
}
