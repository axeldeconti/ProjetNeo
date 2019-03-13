using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {
	
	void Update () {
        if(Input.mouseScrollDelta.y != 0)
        {
            //Update zoom of camera with "zoom += Input.mouseScrollDelta.y"
        }
    }
}
