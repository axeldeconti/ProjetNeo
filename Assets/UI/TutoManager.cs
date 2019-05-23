using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoManager : MonoBehaviour
{
    public GameObject tuto1;
    public GameObject tuto2;
    public GameObject tuto3;
    public GameObject tuto4;
    public GameObject tuto5;
    public GameObject tuto6;
    public GameObject tutoPanel;

    public void Next1()
    {
        tuto1.SetActive(false);
        tuto2.SetActive(true);
    }
    public void Next2()
    {
        tuto2.SetActive(false);
        tuto3.SetActive(true);
    }
    public void Next3()
    {
        tuto3.SetActive(false);
        tuto4.SetActive(true);
    }
    public void Next4()
    {
        tuto4.SetActive(false);
        tuto5.SetActive(true);
    }
    public void Next5()
    {
        tuto5.SetActive(false);
        tuto6.SetActive(true);
    }
    public void Back1()
    {
        tutoPanel.SetActive(false);
    }
    public void Back2()
    {
        tuto2.SetActive(false);
        tuto1.SetActive(true);
    }
    public void Back3()
    {
        tuto3.SetActive(false);
        tuto2.SetActive(true);
    }
    public void Back4()
    {
        tuto4.SetActive(false);
        tuto3.SetActive(true);
    }
    public void Back5()
    {
        tuto5.SetActive(false);
        tuto4.SetActive(true);
    }
    public void Back6()
    {
        tuto6.SetActive(false);
        tuto5.SetActive(true);
    }

}
