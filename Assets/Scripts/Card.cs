using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour {

    public CardData cardData;
    public Image artwork;
    public Text cardName;

	// Use this for initialization
	void Start () {
        artwork.sprite = cardData.artwork;
        //cardName.text = cardData.name;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
