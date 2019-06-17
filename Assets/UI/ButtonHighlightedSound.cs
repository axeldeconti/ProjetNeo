using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHighlightedSound : MonoBehaviour
{
    public AudioSource HighlightedSound;

    void Start()
    {
        HighlightedSound = GetComponent<AudioSource>();
    }

    public void OnSelect(BaseEventData eventData)
    {
        HighlightedSound.Play();
    }
}
