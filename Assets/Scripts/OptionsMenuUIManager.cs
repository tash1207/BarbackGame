using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OptionsMenuUIManager : MonoBehaviour
{
    public GameObject mobileOptionTurnOnButton;
    public GameObject mobileOptionTurnOffButton;
    public GameObject soundOptionTurnOnButton;
    public GameObject soundOptionTurnOffButton;

    void Start()
    {
        UpdateOptionsValues();
    }

    public void ToggleMobileOption()
    {
        OptionsControl.instance.ToggleMobileOptionValue();
        UpdateOptionsValues();
    }

    public void ToggleSoundOption()
    {
        OptionsControl.instance.ToggleSoundOptionValue();
        UpdateOptionsValues();
    }

    public void UpdateOptionsValues()
    {
        UpdateMobileOptionValue();
        UpdateSoundOptionValue();
    }

    void UpdateMobileOptionValue()
    {
        bool mobileOptionEnabled = OptionsControl.instance.GetMobileOptionValue();

        if (mobileOptionEnabled)
        {
            mobileOptionTurnOnButton.SetActive(false);
            mobileOptionTurnOffButton.SetActive(true);
        }
        else
        {
            mobileOptionTurnOnButton.SetActive(true);
            mobileOptionTurnOffButton.SetActive(false);
        }
    }

    void UpdateSoundOptionValue()
    {
        bool soundOptionEnabled = OptionsControl.instance.GetSoundOptionValue();

        if (soundOptionEnabled)
        {
            // TODO: Grab the BackgroundMusic object and unmute the audio source
            soundOptionTurnOnButton.SetActive(false);
            soundOptionTurnOffButton.SetActive(true);
        }
        else
        {
            // TODO: Grab the BackgroundMusic object and mute the audio source
            // TODO: Make sure this sticks around to scene 1 as well
            soundOptionTurnOnButton.SetActive(true);
            soundOptionTurnOffButton.SetActive(false);
        }
    }
}
