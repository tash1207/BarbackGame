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

    public void SetBeerValue(int value)
    {
        beerCount.text = value.ToString();
        beerCount.color = value >= 5 ? Color.red : Color.black;
    }

    public void SetTrayValue(int value)
    {
        trayCount.text = value.ToString();
        trayCount.color = value == 10 ? Color.red : Color.white ;
    }
}
