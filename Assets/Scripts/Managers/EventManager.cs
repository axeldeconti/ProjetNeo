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
    public PositiveEventCardData[] positiveData;
    public EventCardData[] randomData;
    public EncounterEventCardData[] encounterData;
    public bool canGoToNextLevel;
    public Button ButtonsCloseTree;
    public Sprite encouterScreen, normalScreen;
    public GameObject animalImage;

    private int level;
    private Dictionary<int, List<EventButton>> allEventButtons;
    private EventCardData currentEvent;
    private GameObject tree1, tree2, tree3, lastOpenTree;

    private void Start()
    {
        level = 0;
        canGoToNextLevel = false;
        allEventButtons = new Dictionary<int, List<EventButton>>();

        foreach (EventButton firstEventButton in firstEventButtons)
        {
            firstEventButton.Init(0);
            firstEventButton.GetComponent<Button>().interactable = true;
        }

        tree1 = eventTreeScreen.transform.GetChild(0).gameObject;
        tree2 = eventTreeScreen.transform.GetChild(1).gameObject;
        tree3 = eventTreeScreen.transform.GetChild(2).gameObject;
        lastOpenTree = tree1;
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
            case EventType.Positive:
                return positiveData[Random.Range(0, positiveData.Length)];
            case EventType.Encounter:
                return encounterData[Random.Range(0, encounterData.Length)];
            default:
                Debug.Log("Couldn't return EventCardData for " + type.ToString());
                return null;
        }
    }

    public void GoToNextLevel(EventButton eventButton)
    {
        if (!canGoToNextLevel)
            return;

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

        OpenLastTree();
    }

    public void CloseEventTreeScreen()
    {
        eventTreeScreen.SetActive(false);
        tree1.SetActive(false);
        tree2.SetActive(false);
        tree3.SetActive(false);
    }

    public void OpenEventScreen()
    {
        EncounterEventCardData encounter;

        if(encounter = (currentEvent as EncounterEventCardData))
        {
            eventScreen.transform.GetChild(0).GetComponent<Image>().sprite = encouterScreen;
            animalImage.SetActive(true);
            animalImage.GetComponent<Image>().sprite = currentEvent.artwork;
            animalImage.transform.GetChild(0).GetComponent<Text>().text = "Attack points : " + encounter.atk.ToString();
        }
        else
        {
            eventScreen.transform.GetChild(0).GetComponent<Image>().sprite = normalScreen;
            animalImage.SetActive(false);
        }
            

        if(currentEvent.type != EventType.Building)
        {
            EventTitle.text = currentEvent.cardName;
            EventText.text = currentEvent.description;
            eventScreen.SetActive(true);
        }
        else
        {
            BuildingEventManager.instance.OpenBuildingSelectionScreen();
        }
    }

    public void CloseEventScreen()
    {
        eventScreen.SetActive(false);
        GameManager.instance.StartTurn();

        if (currentEvent.type == EventType.Encounter)
        {
            FightManager.instance.ResolveFight(currentEvent as EncounterEventCardData);
        }
    }

    public void OpenTree(int i)
    {
        tree1.SetActive(false);
        tree2.SetActive(false);
        tree3.SetActive(false);

        switch (i)
        {
            case 1:
                tree1.SetActive(true);
                lastOpenTree = tree1;
                break;
            case 2:
                tree2.SetActive(true);
                lastOpenTree = tree2;
                break;
            case 3:
                tree3.SetActive(true);
                lastOpenTree = tree3;
                break;
            default:
                Debug.Log("Tree" + i + " can't be open");
                break;
        }
    }

    public void OpenLastTree()
    {
        if (lastOpenTree == tree1)
            tree1.SetActive(true);
        else if (lastOpenTree == tree2)
            tree2.SetActive(true);
        else if (lastOpenTree == tree3)
            tree3.SetActive(true);
    }
}
