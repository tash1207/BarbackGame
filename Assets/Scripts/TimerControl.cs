using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerControl : MonoBehaviour
{
    public static TimerControl instance { get; private set; }
    public Text timerText;
    public AudioClip beepAudioClip;
    AudioSource audioSource;

    float defaultLevelTime = 60f;
    float timeLeft;
    float beepTime = 6.1f;
    bool timerOn = false;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timerOn)
        {
            if (timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
                UpdateTimer(timeLeft);
            }
            else
            {
                timeLeft = 0;
                timerOn = false;
                GameManager.instance.EndGame();
            }
        }
        
    }

    void UpdateTimer(float currentTime)
    {
        currentTime += 1;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        // Show final 5 seconds in red and play beep noises
        if (currentTime < beepTime)
        {
            timerText.color = Color.red;
            audioSource.PlayOneShot(beepAudioClip);
            beepTime -= 1.0f;
        }
    }

    public void ResetTimer()
    {
        timerOn = true;
        timeLeft = defaultLevelTime;
        beepTime = 6.1f;
        timerText.color = Color.black;
    }
}
