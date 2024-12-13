using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance { get; private set; }

    public GameObject trayDisplay;
    public GameObject beerDisplay;
    public GameObject poopDisplay;

    public Text trayCount;
    public Text beerCount;
    public Text poopCount;

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

    public void SetPoopValue(int value)
    {
        poopCount.text = value.ToString();

        if (value != 0)
        {
            ShowPoopDisplay();
        }
        else
        {
            ShowTrayAndBeerDisplay();
        }
    }

    public void ShowPoopDisplay()
    {
        trayDisplay.SetActive(false);
        beerDisplay.SetActive(false);
        poopDisplay.SetActive(true);
    }

    public void ShowTrayAndBeerDisplay()
    {
        trayDisplay.SetActive(true);
        beerDisplay.SetActive(true);
        poopDisplay.SetActive(false);
    }

    public void ResetAllDisplayValues()
    {
        beerCount.text = "0";
        beerCount.color = Color.black;

        trayCount.text = "0";
        trayCount.color = Color.white ;

        poopCount.text = "0";
    }
}
