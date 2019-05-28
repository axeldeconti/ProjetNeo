using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[CreateAssetMenu(fileName = "New card", menuName = "CardData/HumanCardData")]
public class HumanCardData : CardData {

    public int minAge, maxAge, minLife, maxLife, minAtk, maxAtk;
    public Human human;
    public Color lifeColor, ageColor, attackColor;

    public override string GetTooltipInfoText()
    {
        StringBuilder builder = new StringBuilder();

        builder.Append("<size=35>").Append(ColouredName).Append("</size>").AppendLine();
        builder.Append("Category : Human").AppendLine();
        builder.Append("Metier : ").Append(human.metier);

        return builder.ToString();
    }
}
