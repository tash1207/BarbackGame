using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip buttonClickSound;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayGame()
    {
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
