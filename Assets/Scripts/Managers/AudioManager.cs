using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    #region Singleton

    public static AudioManager instance { get; private set; }

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

    public AudioSource effectSource;
    public AudioClip click;

    public void PlaySoundEffects(AudioClip audio)
    {
        effectSource.PlayOneShot(audio);
    }

    public void PlayClickSound()
    {
        effectSource.PlayOneShot(click);
    }
}
