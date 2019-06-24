using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildingToSelect : MonoBehaviour, IPointerDownHandler
{

    public BuildingCardData data;

    public void Init(BuildingCardData _data)
    {
        data = _data;

        GetComponent<Image>().sprite = data.buildingToSelectSprite;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            DeckManager.instance.AddCard(data.cardName);
            BuildingEventManager.instance.CloseBuildingSelectionScreen();
        }
    }
}
