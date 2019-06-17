using UnityEngine;

public class AgriSquareScreen : MonoBehaviour {

    public Transform screens;

    public void CloseScreen()
    {
        for (int i = 0; i < screens.childCount; i++)
        {
            screens.GetChild(i).gameObject.SetActive(false);
        }

        gameObject.SetActive(false);
    }
}
