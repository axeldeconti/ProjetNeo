using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonLock : MonoBehaviour
{
    private bool buttonsMenus = true;

    public GameObject buttonsOptions;
    public GameObject buttonsPlay;
    public GameObject buttonsQuit;
    public GameObject buttonsBack;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void EnableOptions()
    {
        buttonsQuit.GetComponent<Button>().interactable = false;
        buttonsOptions.GetComponent<Button>().interactable = false;
        buttonsPlay.GetComponent<Button>().interactable = false;
        buttonsBack.GetComponent<Button>().interactable = true;
    }

    public void EnableMainMenu()
    {
        buttonsQuit.GetComponent<Button>().interactable = true;
        buttonsOptions.GetComponent<Button>().interactable = true;
        buttonsPlay.GetComponent<Button>().interactable = true;
        buttonsBack.GetComponent<Button>().interactable = false;
    }
}

