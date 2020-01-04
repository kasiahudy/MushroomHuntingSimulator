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
    private GameObject player;
    [SerializeField]
    private Transform healthBar;
    [SerializeField]
    private GameObject healthBarTextObject;
    [SerializeField]
    private GameObject gameOverInfoTextObject;

    private List<Mushroom> mushrooms;
    private int playerHealthPoints;
    private int healthPointsReductionDelta;
    private bool gameEnded;
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

    public void RestartGame()
    {
        if (gameEnded)
        {
            DestroyAllMushrooms();
            ClearGameOverInfoText();
            ReactivatePlayerControl();
            InitGame();
        }
    }

    void Start()
    {
        LoadHealthBarText();
        LoadGameOverInfoText();
        InitGame();
    }

    void Update()
    {
        if (!gameEnded)
            PerformInGameManagement();
    }

    private void LoadHealthBarText()
    {
        healthBarText = healthBarTextObject.GetComponent<TextMeshProUGUI>();
    }

    private void LoadGameOverInfoText()
    {
        gameOverInfoText = gameOverInfoTextObject.GetComponent<TextMeshProUGUI>();
    }

    private void InitGame()
    {
        SpawnMushrooms();
        SetInitialParamaterValues();
        StartTimeMeasurement();
        gameEnded = false;
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

    private void IncrementTimeForStopers()
    {
        timePassedStoper.IncrementTime(Time.deltaTime);
    }

    private void PerformInGameManagement()
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
        float halfOfPlayerHealthPoints = maxPlayerHealthPoints / 2;
        if ((float)playerHealthPoints > halfOfPlayerHealthPoints)
            return new Color(-((float)playerHealthPoints - (float)maxPlayerHealthPoints) / halfOfPlayerHealthPoints, 1.0f, 0.0f);
        else
            return new Color(1.0f, (float)playerHealthPoints / halfOfPlayerHealthPoints, 0.0f);
    }

    private void CheckIfPlayerDied()
    {
        if (playerHealthPoints <= 0)
            EndGame();
    }

    private void EndGame()
    {
        gameEnded = true;
        DeactivatePlayerControl();
        ShowGameOverText();
        // TODO trzeba usunac tu wszystkie aktywne efekty
    }

    private void ShowGameOverText()
    {
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

    private void DeactivatePlayerControl()
    {
        PlayerControl playerControl = player.GetComponent<PlayerControl>();
        playerControl.DeactivateControl();
        playerControl.ActivateRestartInput();
    }

    private void DestroyAllMushrooms()
    {
        foreach (Mushroom mushroom in mushrooms)
            if (mushroom != null)
                Destroy(mushroom.gameObject);
    }

    private void ClearGameOverInfoText()
    {
        gameOverInfoText.text = "";
    }

    private void ReactivatePlayerControl()
    {
        PlayerControl playerControl = player.GetComponent<PlayerControl>();
        playerControl.ActivateControl();
        playerControl.DeactivateRestartInput();
    }
}
