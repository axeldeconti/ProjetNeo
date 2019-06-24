using UnityEngine.EventSystems;
using UnityEngine;

public class DisplayTooltipText : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string tooltipText;

    public void OnPointerEnter(PointerEventData eventData)
    {
        TooltipPopup.instance.DisplayInfo(tooltipText);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipPopup.instance.HideInfo();
    }
}
