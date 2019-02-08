using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour {

    public CardData cardData;
    public Image artwork, icon;
    public Text cardName;

    public void InitializeCard(CardData _cardData)
    {
        cardData = _cardData;
        artwork.sprite = cardData.artwork;
    }

    public void ChangeAspectToIcon()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        icon.gameObject.SetActive(true);
    }

    public void ChangeAspectToCard()
    {
        icon.gameObject.SetActive(false);
        transform.GetChild(0).gameObject.SetActive(true);
    }

    public void DropCardOnNodePannel(NodePannel np)
    {
        Instantiate(CardManager.instance.boardCardPrefab, np.transform).GetComponent<BoardCard>().Init(cardData);
        Destroy(gameObject);
    }
}
