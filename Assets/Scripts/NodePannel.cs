using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodePannel : MonoBehaviour {

    public enum NodePannelState { Empty, WithCard}

    public NodePannelState state;

    private Sprite defaultImage;
    private Image myImage;

    private void Start()
    {
        myImage = GetComponent<Image>();

        defaultImage = myImage.sprite;

        state = NodePannelState.Empty;
    }

    // Called when a card is drop on this pannel
    public void DropCard(Card card)
    {
        card.DropCardOnNodePannel(this);
        state = NodePannelState.WithCard;
    }
}
