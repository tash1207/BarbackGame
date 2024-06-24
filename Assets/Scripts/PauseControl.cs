using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseControl : MonoBehaviour
{
    public static PauseControl instance { get; private set; }
    public static bool gameIsPaused;
    public GameObject pauseDisplay;
    public GameObject alertDisplay;
    public TMP_Text mobileOptionButtonText;

    void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        // If game is over, don't allow pausing.
        if (GameManager.gameIsEnded) {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        gameIsPaused = true;
        Time.timeScale = 0;
        AudioListener.pause = true;
        pauseDisplay.SetActive(true);
        alertDisplay.SetActive(false);
        UpdateOptionsValues();
    }

    public void ResumeGame()
    {
        gameIsPaused = false;
        Time.timeScale = 1;
        AudioListener.pause = false;
        pauseDisplay.SetActive(false);
    }

    public void ToggleMobileMode()
    {
        OptionsControl.instance.ToggleMobileOptionValue();
        UpdateOptionsValues();
    }

    void UpdateOptionsValues()
    {
        bool value = OptionsControl.instance.GetMobileOptionValue();
        mobileOptionButtonText.text = value ? "KEYBOARD" : "MOBILE";
    }

    public void ExitToMainMenu()
    {
        ResumeGame();
        SceneManager.LoadSceneAsync(0);
    }
}
