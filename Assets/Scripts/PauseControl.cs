using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseControl : MonoBehaviour
{
    public static PauseControl instance { get; private set; }
    public static bool gameIsPaused;
    public GameObject pauseDisplay;
    public GameObject alertDisplay;

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
            if (gameIsPaused)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        AudioListener.pause = true;
        pauseDisplay.SetActive(true);
        alertDisplay.SetActive(false);

    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        AudioListener.pause = false;
        pauseDisplay.SetActive(false);
    }

    public void ExitToMainMenu()
    {
        gameIsPaused = false;
        ResumeGame();
        SceneManager.LoadSceneAsync(0);
    }
}
