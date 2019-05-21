using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildingToSelect : MonoBehaviour, IPointerDownHandler
{

    public BuildingCardData data;
    public Text buildingName, ressource1, ressource2;

    public void Init(BuildingCardData _data)
    {
        data = _data;

        GetComponent<Image>().sprite = data.artwork;

        buildingName.text = data.cardName;

        ressource1.text = data.ressource1 + " - " + data.nbRessource1;

        if (data.nbRessource2 != 0)
            ressource2.text = data.ressource2 + " - " + data.nbRessource2;
        else
            ressource2.text = "";
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
