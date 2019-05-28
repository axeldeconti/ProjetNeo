using UnityEngine;

public abstract class CardData : ScriptableObject
{

    public CardType cardType;
    public Sprite frame, artwork;
    public string cardName;
    public Color nameColor;
    public AudioClip soundEffect;
    public string tooltipDescription;

    /// <summary>
    /// Call to apply the effect of the card
    /// </summary>
    public virtual void ApplyCardEffect() { }

    public abstract string GetTooltipInfoText();

    public string ColouredName
    {
        get
        {
            string hexColour = ColorUtility.ToHtmlStringRGB(nameColor);
            return $"<color=#{hexColour}>{cardName}</color>";
        }
    }
}

/// <summary>
/// All type a card can be
/// </summary>
public enum CardType { Human, Ressource, Tool, Building, Event }
