using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Ressource : CardTypeComponent, IPointerDownHandler {

    public RessourceCardData cardData;

    public override void Init(CardData _cardData)
    {
        RessourceCardData tmp;

        if (tmp = (_cardData as RessourceCardData))
        {
            cardData = tmp;
        }
    }

    public override void InitBoardCard(CardTypeComponent _cardTypeComp)
    {
        Ressource ressourceComp, tmp;

        if (tmp = (_cardTypeComp as Ressource))
        {
            ressourceComp = tmp;
        }
        else
        {
            Debug.Log("Not HumanComponent in InitBoardCard");
            ressourceComp = null;
        }

        cardData = ressourceComp.cardData;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) && (GetComponent<BoardCard>() != null))
        {
            Workbench wb = transform.parent.GetComponent<DropZone_Base>().cardParent.GetComponent<Workbench>();

            if (wb != null)
            {
                wb.RemoveRessource(this.gameObject, true);
            }
            else
                Debug.Log("Not a ressource on workbench");
        }
    }
}
