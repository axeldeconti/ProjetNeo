using UnityEngine;
using UnityEngine.EventSystems;

public class SeedDropZone : DropZone_Base
{

    [HideInInspector] public AgriculturalSquare mySquare;
    public int nb;
    public bool hasSeed;

    protected override void DropCard(Draggable d, Card c)
    {
        Seed s;

        if (s = c.cardData as Seed)
        {
            CreateBoardcard(d, c);
            mySquare.AddSeed(s, this);
        }
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);

        if (hasSeed)
            TooltipPopup.instance.DisplayInfo(mySquare.seeds[nb]);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);

        if (hasSeed)
            TooltipPopup.instance.HideInfo();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) && hasSeed)
        {
            mySquare.Harvest(this);
        }
    }
}
