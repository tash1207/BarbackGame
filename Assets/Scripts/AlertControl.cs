using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlertControl : MonoBehaviour
{
    public static AlertControl instance { get; private set; }
    public GameObject alertDisplay;
    public Text alertText;
    bool showingAlert;
    float alertTimer;
    float alertChangeTime = 4.0f;

    void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (showingAlert)
        {
            alertTimer -= Time.deltaTime;

            if (alertTimer < 0)
            {
                HideAlert();
            }
        }
    }

    public void ShowAlert(string text)
    {
        alertText.text = text;
        alertDisplay.SetActive(true);
        showingAlert = true;
        alertTimer = alertChangeTime;
    }

    public void HideAlert()
    {
        alertDisplay.SetActive(false);
        showingAlert = false;
    }
}
