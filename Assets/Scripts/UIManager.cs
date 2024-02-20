using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance { get; private set; }

    public Text beerCount;
    public Text trayCount;

    void Awake()
    {
        instance = this;
    }

    public void SetBeerValue(string value)
    {
        beerCount.text = value;
    }

    public void SetTrayValue(string value)
    {
        trayCount.text = value;
    }
}
