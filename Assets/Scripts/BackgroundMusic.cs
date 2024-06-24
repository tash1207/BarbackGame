using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public static BackgroundMusic instance { get; private set; }

    public AudioClip menuMusic;
    public AudioClip gameMusic;

    AudioSource audioSource;
    bool backgroundMusicEnabled;

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

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        backgroundMusicEnabled = OptionsControl.instance.GetSoundOptionValue();
        audioSource.mute = !backgroundMusicEnabled;
    }

    public void PlayMenuMusic()
    {
        audioSource.clip = menuMusic;
        audioSource.Play();
    }

    public void PlayGameMusic()
    {
        audioSource.clip = gameMusic;
        audioSource.Play();
    }
}
