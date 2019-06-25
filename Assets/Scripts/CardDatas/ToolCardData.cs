using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[CreateAssetMenu(fileName = "New card", menuName = "CardData/ToolCardData")]
public class ToolCardData : CardData {

    public HumanMetier metier;
    public int bonusAtk, bonusLife;

    public override string GetTooltipInfoText()
    {
        StringBuilder builder = new StringBuilder();

        builder.Append("<size=35>").Append(ColouredName).Append("</size>").AppendLine();
        builder.Append("Tool").AppendLine();
        builder.Append(metier).AppendLine();
        builder.Append("Attack : ").Append(bonusAtk).AppendLine();
        builder.Append("Life : ").Append(bonusLife);
        //builder.Append("Description : ").Append(tooltipDescription).AppendLine();

        return builder.ToString();
    }
}
