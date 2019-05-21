using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingEventManager : MonoBehaviour {

    #region Singleton

    public static BuildingEventManager instance { get; private set; }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    #endregion

    public GameObject buildingSelectionScreen, buildingToSelectPrefab;
    public BuildingCardData[] buildingToBuild;

    /// <summary>
    /// Open the building selection screen and initialize it
    /// </summary>
    public void OpenBuildingSelectionScreen()
    {
        buildingSelectionScreen.SetActive(true);

        for (int i = 0; i < 3; i++)
        {
            BuildingCardData data = buildingToBuild[Random.Range(0, buildingToBuild.Length)];
            GameObject b = Instantiate(buildingToSelectPrefab, buildingSelectionScreen.transform.GetChild(1));
            b.GetComponent<BuildingToSelect>().Init(data);
        }
    }

    /// <summary>
    /// Close the building selection screen and clear it
    /// </summary>
    public void CloseBuildingSelectionScreen()
    {
        buildingSelectionScreen.SetActive(false);

        foreach (Transform child in buildingSelectionScreen.transform.GetChild(1))
        {
            Destroy(child.gameObject);
        }

        GameManager.instance.StartTurn();
    }
}
