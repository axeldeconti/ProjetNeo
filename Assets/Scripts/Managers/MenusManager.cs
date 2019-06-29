using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MenusManager : MonoBehaviour
{
    public string gameScene;
    
    public GameObject mainMenu;
    public GameObject optionsMenu;

    Animator animMainMenu;
    public GameObject objectAnimator;

    public AudioMixer audioMixer;

    public Dropdown resolutionDropdown;

    Resolution[] resolutions;
    

    // Start

    void Start()
    {
        animMainMenu = objectAnimator.GetComponent<Animator>();

        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && 
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    void Update()
    {

    }

    // Functions
    
    public void SetResolution (int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetVolume (float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    public void SetQuality (int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullscreen (bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    // Buttons

    public void OnClickedOptions ()
    {
        animMainMenu.SetTrigger("MainMenu");
    }

    public void OnClickedBack()
    {
        animMainMenu.SetTrigger("Options");
    }

    public void OnClickedPlay ()
    {
        SceneManager.LoadScene(gameScene);
    }

    public void OnCLickedQuit ()
    {
        Application.Quit();
    }
}
