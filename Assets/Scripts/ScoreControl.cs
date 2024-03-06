using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreControl : MonoBehaviour
{
    public static ScoreControl instance { get; private set; }

    public Text scoreText;
    int currentScore = 0;
    int glasswareValue = 1;
    int trayValue = 1;
    int poopValue = 5;

    void Awake()
    {
        instance = this;
    }

    public void IncrementGlassware(int amount)
    {
        IncrementScore(amount * glasswareValue);
    }

    public void IncrementTrays(int amount)
    {
        IncrementScore(amount * trayValue);
    }

    public void IncrementPoop(int amount)
    {
        IncrementScore(amount * poopValue);
    }

    void IncrementScore(int amount)
    {
        currentScore += amount;
        scoreText.text = currentScore.ToString();
    }
}
