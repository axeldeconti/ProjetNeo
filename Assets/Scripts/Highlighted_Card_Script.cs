using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlighted_Card_Script : MonoBehaviour
{
    Animation animHover;
	// Use this for initialization
	void Start ()
    {
        animHover = GetComponent<Animation>();
    }

    public void playAnimHover()
    {
        animHover.Play();
    }

    public void stopAnimHover()
    {
        animHover.Stop();
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
