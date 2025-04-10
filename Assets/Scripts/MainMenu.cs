using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern bool IsMobile();

    AudioSource audioSource;
    public AudioClip buttonClickSound;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    bool IsMobileWebGL()
    {
        #if !UNITY_EDITOR && UNITY_WEBGL
            return IsMobile();
        #endif
        return false;
    }

    public void PlayGame()
    {
        if (IsMobileWebGL())
        {
            OptionsControl.instance.SetMobileOptionValue(true);
        }
        SceneManager.LoadSceneAsync(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ClickButton()
    {
        PlaySound(buttonClickSound);
    }

    void PlaySound(AudioClip clip)
    {
        if (audioSource == null)
        {
            Debug.Log("null audio source");
        }
        if (clip == null)
        {
            Debug.Log("null clip " + clip);
        }
        audioSource.PlayOneShot(clip);
    }
}
