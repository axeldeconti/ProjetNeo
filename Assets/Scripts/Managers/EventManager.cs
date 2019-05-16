using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour {

    #region Singleton

    public static EventManager instance { get; private set; }

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

    public EventButton[] firstEventButtons;
    public BuildingEventCardData[] buildingData;
    public NegativeEventCardData[] negativeData;
    public RandomEventCardData[] randomData;
    public EncounterEventCardData[] encounterData;

    private void Start()
    {
        foreach (EventButton firstEventButton in firstEventButtons)
        {
            firstEventButton.Init(0);
        }
    }

    /// <summary>
    /// Return a random EventData of the type in parameter
    /// </summary>
    public EventCardData ChooseRandomEventType(EventType type)
    {
        switch (type)
        {
            case EventType.Building:
                return buildingData[Random.Range(0, buildingData.Length)];
            case EventType.Random:
                return randomData[Random.Range(0, randomData.Length)];
            case EventType.Negative:
                return negativeData[Random.Range(0, negativeData.Length)];
            case EventType.Encounter:
                return encounterData[Random.Range(0, encounterData.Length)];
            default:
                Debug.Log("Couldn't return EventCardData for " + type.ToString());
                return null;
        }
    }
}
