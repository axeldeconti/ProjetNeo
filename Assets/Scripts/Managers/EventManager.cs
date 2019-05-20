using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public GameObject eventTreeScreen, eventScreen;
    public Text EventTitle, EventText;
    public EventButton[] firstEventButtons;
    public BuildingEventCardData[] buildingData;
    public NegativeEventCardData[] negativeData;
    public EventCardData[] randomData;
    public EncounterEventCardData[] encounterData;

    private int level;
    private Dictionary<int, List<EventButton>> allEventButtons;
    private EventCardData currentEvent;

    private void Start()
    {
        level = 0;
        allEventButtons = new Dictionary<int, List<EventButton>>();

        foreach (EventButton firstEventButton in firstEventButtons)
        {
            firstEventButton.Init(0);
            firstEventButton.GetComponent<Button>().interactable = true;
        }
    }

    /// <summary>
    /// Add the EventButton in parameter to the allEventButtons
    /// </summary>
    public void AddEventButton(EventButton button)
    {
        if (allEventButtons.ContainsKey(button.lvl))
        {
            List<EventButton> actualList = allEventButtons[button.lvl];

            if (!actualList.Contains(button))
            {
                allEventButtons[button.lvl].Add(button);
            }
        }
        else
        {
            allEventButtons[button.lvl] = new List<EventButton>();
            allEventButtons[button.lvl].Add(button);
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

    public void GoToNextLevel(EventButton eventButton)
    {
        foreach (EventButton button in allEventButtons[eventButton.lvl])
        {
            button.GetComponent<Button>().interactable = false;
        }

        currentEvent = eventButton.eventdata;

        level++;

        foreach (EventButton button in eventButton.nextEvents)
        {
            button.GetComponent<Button>().interactable = true;
        }

        CloseEventTreeScreen();
        OpenEventScreen();
    }

    public void OpenEventTreeScreen()
    {
        eventTreeScreen.SetActive(true);
    }

    public void CloseEventTreeScreen()
    {
        eventTreeScreen.SetActive(false);
    }

    public void OpenEventScreen()
    {
        EventTitle.text = currentEvent.cardName;
        EventText.text = currentEvent.description;
        eventScreen.SetActive(true);
    }

    public void CloseEventScreen()
    {
        eventScreen.SetActive(false);
        GameManager.instance.StartTurn();
    }
}
