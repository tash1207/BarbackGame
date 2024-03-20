using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OptionsMenuUIManager : MonoBehaviour
{
    public TMP_Text mobileOptionButtonText;

    void Start()
    {
        UpdateOptionsValues();
    }

    public void ToggleMobileOption()
    {
        OptionsControl.instance.ToggleMobileOptionValue();
        UpdateOptionsValues();
    }

    public void UpdateOptionsValues()
    {
        bool value = OptionsControl.instance.GetMobileOptionValue();
        mobileOptionButtonText.text = value ? "Turn Off" : "Turn On";
    }
}
