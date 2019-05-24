using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool : CardTypeComponent {

    public ToolCardData data;

    public override void Init(CardData cardData)
    {

    }

    public override void InitBoardCard(CardTypeComponent _cardTypeComp)
    {
        
    }

    /// <summary>
    /// Call when dropped on a Human to apply changes
    /// </summary>
    public void DropOnHuman(ToolCardData toolData, Human h)
    {
        h.tool = this;
        h.metier = toolData.metier;
        h.AddAtk(toolData.bonusAtk);
        h.AddLife(toolData.bonusLife);

        data = toolData;
    }
}
