using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private int maxPlayerHealthPoints = 100;
    [SerializeField]
    private int initialHealthPointsReductionDelta = 1;
    [SerializeField]
    private float decreaseOfHealthPointsTimeSeconds = 1.0f;
    [SerializeField]
    private float increaseOfHealthPointsReductionDeltaTimeSeconds = 60.0f;
    [SerializeField]
    private Transform healthBar;
    [SerializeField]
    private GameObject healthBarTextObject;
    [SerializeField]
    private GameObject gameOverInfoTextObject;

    private List<Mushroom> mushrooms;
    private int playerHealthPoints;
    private int healthPointsReductionDelta;
    private Stoper timePassedStoper = new Stoper();
    private CountdownTimer decreaseOfHealthPointsTimer = new CountdownTimer();
    private CountdownTimer increaseOfHealthPointsReductionDeltaTimer = new CountdownTimer();
    private TextMeshProUGUI healthBarText;
    private TextMeshProUGUI gameOverInfoText;

    public void UpdatePlayerHealthPoints(int delta)
    {
        playerHealthPoints += delta;
        if (playerHealthPoints > maxPlayerHealthPoints)
            playerHealthPoints = maxPlayerHealthPoints;
        else if (playerHealthPoints < 0)
            playerHealthPoints = 0;
        CheckIfPlayerDied();
    }

    public float GetDecreaseOfHealthPointsTimeSeconds()
    {
        return decreaseOfHealthPointsTimeSeconds;
    }

    public void SetDecreaseOfHealthPointsTimeSeconds(float decreaseOfHealthPointsTimeSeconds)
    {
        this.decreaseOfHealthPointsTimeSeconds = decreaseOfHealthPointsTimeSeconds;
    }

    void Start()
    {
        SpawnMushrooms();
        SetInitialParamaterValues();
        StartTimeMeasurement();
        LoadHealthBarText();
        LoadGameOverInfoText();
    }

    void Update()
    {
        IncrementTimeForStopers();
        DecrementTimeForCountdownTimers();
        if (increaseOfHealthPointsReductionDeltaTimer.HasEnded())
        {
            IncreaseHealthPointsReductionDelta(1);
            StartIncreaseOfHealthPointsReductionDeltaTimer();
        }
        if (decreaseOfHealthPointsTimer.HasEnded())
        {
            DecreasePlayerHealthPoints();
            StartDecreaseOfHealthPointsTimer();
        }
        UpdateHealthBar();
        CheckIfPlayerDied();
    }

    private void SpawnMushrooms()
    {
        MushroomSpawnManager mushroomSpawnManager = GetComponent<MushroomSpawnManager>();
        mushroomSpawnManager.SpawnMushrooms();
        mushrooms = mushroomSpawnManager.GetMushrooms();
    }

    private void SetInitialParamaterValues()
    {
        playerHealthPoints = maxPlayerHealthPoints;
        healthPointsReductionDelta = initialHealthPointsReductionDelta;
    }

    private void StartTimeMeasurement()
    {
        StartTimePassedStoper();
        StartDecreaseOfHealthPointsTimer();
        StartIncreaseOfHealthPointsReductionDeltaTimer();
    }

    private void StartTimePassedStoper()
    {
        timePassedStoper.Start();
    }

    private void StartDecreaseOfHealthPointsTimer()
    {
        decreaseOfHealthPointsTimer.StartCountdown(decreaseOfHealthPointsTimeSeconds);
    }

    private void StartIncreaseOfHealthPointsReductionDeltaTimer()
    {
        increaseOfHealthPointsReductionDeltaTimer.StartCountdown(increaseOfHealthPointsReductionDeltaTimeSeconds);
    }

    private void LoadHealthBarText()
    {
        healthBarText = healthBarTextObject.GetComponent<TextMeshProUGUI>();
    }

    private void LoadGameOverInfoText()
    {
        gameOverInfoText = gameOverInfoTextObject.GetComponent<TextMeshProUGUI>();
    }

    private void IncrementTimeForStopers()
    {
        timePassedStoper.IncrementTime(Time.deltaTime);
    }

    private void DecrementTimeForCountdownTimers()
    {
        decreaseOfHealthPointsTimer.DecrementTime(Time.deltaTime);
        increaseOfHealthPointsReductionDeltaTimer.DecrementTime(Time.deltaTime);
    }

    private void DecreasePlayerHealthPoints()
    {
        playerHealthPoints -= healthPointsReductionDelta;
        if (playerHealthPoints < 0)
            playerHealthPoints = 0;
    }

    private void IncreaseHealthPointsReductionDelta(int value)
    {
        healthPointsReductionDelta += value;
    }

    private void UpdateHealthBar()
    {
        healthBar.localScale = new Vector3(playerHealthPoints / (float)maxPlayerHealthPoints, 1, 1);
        healthBarText.text = playerHealthPoints + "/" + maxPlayerHealthPoints;
        healthBar.GetComponent<Image>().color = GetFittingHealthBarColor();
    }

    private Color GetFittingHealthBarColor()
    {
        if (playerHealthPoints >= 50)
            return new Color(0.0f, 1.0f, 0.0f);
        else if (playerHealthPoints >= 20)
            return new Color(1.0f, 1.0f, 0.0f);
        else
            return new Color(1.0f, 0.0f, 0.0f);
    }

    private void CheckIfPlayerDied()
    {
        if (playerHealthPoints <= 0)
            EndGame();
    }

    private void EndGame()
    {
        Debug.Log("Koniec gry");
        gameOverInfoText.text = "Game Over\nYou survived: " + FormatTime(timePassedStoper.GetTimePassed());
    }

    private string FormatTime(float time)
    {
        String text = "";
        int minutes = (int)(time / 60.0f);
        int seconds = (int)(time % 60.0f);
        if (minutes < 10)
            text += "0";
        text += minutes + ":";
        if (seconds < 10)
            text += "0";
        text += seconds;

        return text;
    }
}
