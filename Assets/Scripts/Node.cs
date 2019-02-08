using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour {

    [HideInInspector] public Vector3 worldPosition;
    [HideInInspector] public int gridX, gridY;

    //Builder
    public Node Init(Vector3 _worldPos, int _gridX, int _gridY, GameObject _gridPannel)
    {
        worldPosition = _worldPos;
        gridX = _gridX;
        gridY = _gridY;

        transform.position = _worldPos;

        return this;
    }
}
