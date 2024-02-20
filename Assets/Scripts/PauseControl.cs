using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseControl : MonoBehaviour
{
    public static PauseControl instance { get; private set; }
    public static bool gameIsPaused;
    public GameObject pauseDisplay;

    void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameIsPaused = !gameIsPaused;
            PauseGame();
        }
    }

    public void PauseGame()
    {
        if (gameIsPaused)
        {
            Time.timeScale = 0;
            AudioListener.pause = true;
            pauseDisplay.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            AudioListener.pause = false;
            pauseDisplay.SetActive(false);
        }
    }
}
