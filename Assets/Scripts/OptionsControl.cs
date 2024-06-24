using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsControl : MonoBehaviour
{
    public static OptionsControl instance { get; private set; }

    bool mobileOption = false;
    bool soundOption = true;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public bool GetMobileOptionValue()
    {
        return mobileOption;
    }

    public void ToggleMobileOptionValue()
    {
        mobileOption = !mobileOption;
    }

    public bool GetSoundOptionValue()
    {
        return soundOption;
    }

    public void ToggleSoundOptionValue()
    {
        soundOption = !soundOption;
    }
}
