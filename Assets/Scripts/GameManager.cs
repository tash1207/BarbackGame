using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    public static bool gameIsEnded;

    [SerializeField] GameObject playerChar;
    [SerializeField] List<GameObject> tables;
    [SerializeField] GameObject beerPrefab;
    [SerializeField] GameObject blueTrayPrefab;
    [SerializeField] GameObject redTrayPrefab;

    [SerializeField] GameObject pauseDisplay;
    [SerializeField] GameObject alertDisplay;
    [SerializeField] GameObject gameOverDisplay;
    [SerializeField] Text finalScoreText;

    PlayerController playerController;

    public AudioClip gameOverAudioClip;
    AudioSource audioSource;

    float beerTimer;
    float beerChangeTime = 4.0f;
    float trayTimer;
    float trayChangeTime = 9.0f;
    int tableIndex = 0;

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        audioSource = GetComponent<AudioSource>();
        beerTimer = beerChangeTime;
        trayTimer = trayChangeTime;

        StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        beerTimer -= Time.deltaTime;
        trayTimer -= Time.deltaTime;

        if (beerTimer < 0)
        {
            SpawnBeer();
        }
        if (trayTimer < 0)
        {
            SpawnTray();
        }
    }

    void SpawnBeer()
    {
        tableIndex = Random.Range(0, 5);
        GameObject table = tables[tableIndex];
        float randomX = Random.Range(-0.7f, 0.7f);
        float randomY = Random.Range(-0.7f, 0.7f);
        Vector2 beerPosition = new Vector2(table.transform.position.x + randomX, table.transform.position.y + randomY);
        GameObject newBeer = Instantiate(beerPrefab, beerPosition, Quaternion.identity) as GameObject;
        newBeer.transform.parent = table.transform;
        // TODO: Adjust time depending on how many removables are on screen.
        beerTimer = beerChangeTime;
    }

    void SpawnTray()
    {
        tableIndex = Random.Range(1, 5);
        GameObject table = tables[tableIndex];
        float randomX = Random.Range(-0.4f, 0.4f);
        float randomY = Random.Range(-0.5f, 0.7f);
        Vector2 trayPosition = new Vector2(table.transform.position.x + randomX, table.transform.position.y + randomY);
        GameObject newTray = Instantiate(Random.Range(0, 2) < 1 ? blueTrayPrefab : redTrayPrefab, trayPosition, Quaternion.identity) as GameObject;
        newTray.transform.parent = table.transform;
        trayTimer = trayChangeTime;
    }

    public void StartGame()
    {
        // TODO: Set start location of the beers and trays here.
        ResetPlayerChar();
        UIManager.instance.ResetAllDisplayValues();
        UIManager.instance.ShowTrayAndBeerDisplay();
        BackgroundMusic.instance.PlayGameMusic();
        TimerControl.instance.ResetTimer();
        ScoreControl.instance.ResetScore();
        if (!OptionsControl.instance.GetMobileOptionValue())
        {
            AlertControl.instance.ShowAlert("Press E to interact with objects");
        }
        else if (OptionsControl.instance.GetMobileOptionValue())
        {
            AlertControl.instance.ShowAlert("Make sure your phone is in landscape mode");
        }
        Time.timeScale = 1;
        gameOverDisplay.SetActive(false);
        gameIsEnded = false;
    }

    void ResetPlayerChar()
    {
        // Move player character object to start position.
        Vector2 startPosition = new Vector2(-3f, 3f);
        playerChar.transform.position = startPosition;

        playerController.ResetAllInteractables();
    }

    public void EndGame()
    {
        Time.timeScale = 0;
        //AudioListener.pause = true;
        pauseDisplay.SetActive(false);
        alertDisplay.SetActive(false);
        gameOverDisplay.SetActive(true);
        finalScoreText.text = "Final Score: " + ScoreControl.instance.GetScore().ToString();
        gameIsEnded = true;
        // Play game over audio
        // audioSource.PlayOneShot(gameOverAudioClip);
    }
}
