using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

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
    private GameObject controlsManagerObject;
    [SerializeField]
    private string effectTag = "Effect";

    private List<Mushroom> mushrooms;
    private int playerHealthPoints;
    private int healthPointsReductionDelta;
    private bool gameStarted;
    private bool gameEnded;
    private Stoper timePassedStoper = new Stoper();
    private CountdownTimer decreaseOfHealthPointsTimer = new CountdownTimer();
    private CountdownTimer increaseOfHealthPointsReductionDeltaTimer = new CountdownTimer();
    private TextMeshProUGUI healthBarText;
    private TextMeshProUGUI gameOverInfoText;

    public void RestartGame()
    {
        if (!gameStarted)
        {
            InitGame();
            gameStarted = true;
        }            
        else if (gameEnded)
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void UpdatePlayerHealthPoints(int delta)
    {
        playerHealthPoints += delta;
        if (playerHealthPoints > maxPlayerHealthPoints)
            playerHealthPoints = maxPlayerHealthPoints;
        else if (playerHealthPoints < 0)
            playerHealthPoints = 0;
        CheckIfPlayerDied();
    }

    public int GetPlayerHealthPoints()
    {
        return playerHealthPoints;
    }

    public int GetMaxPlayerHealthPoints()
    {
        return maxPlayerHealthPoints;
    }

    public float GetDecreaseOfHealthPointsTimeSeconds()
    {
        return decreaseOfHealthPointsTimeSeconds;
    }

    public void SetDecreaseOfHealthPointsTimeSeconds(float decreaseOfHealthPointsTimeSeconds)
    {
        this.decreaseOfHealthPointsTimeSeconds = decreaseOfHealthPointsTimeSeconds;
    }

    public float GetTimePassed()
    {
        return timePassedStoper.GetTimePassed();
    }

    public List<Mushroom> GetMushrooms()
    {
        mushrooms.RemoveAll(mushroom => mushroom == null);
        return mushrooms;
    }

    public bool HasGameStarted()
    {
        return gameStarted;
    }

    public bool HasGameEnded()
    {
        return gameStarted && gameEnded;
    }

    void Start()
    {
        gameStarted = false;
        gameEnded = false;
    }

    void Update()
    {
        if (gameStarted && !gameEnded)
            PerformInGameManagement();
    }

    private void InitGame()
    {
        SpawnMushrooms();
        SetInitialParamaterValues();
        ActivatePlayerControl();
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

    private void ActivatePlayerControl()
    {
        controlsManagerObject.GetComponent<ControlsManager>().ActivateControl();
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

    private void CheckIfPlayerDied()
    {
        if (playerHealthPoints <= 0)
            EndGame();
    }

    private void EndGame()
    {
        gameEnded = true;
        DeactivatePlayerControl();
        RemoveAllActiveEffects();
    }

    private void DeactivatePlayerControl()
    {
        ControlsManager controlsManager = controlsManagerObject.GetComponent<ControlsManager>();
        controlsManager.DeactivateControl();
    }

    private void RemoveAllActiveEffects()
    {
        var effects = GameObject.FindGameObjectsWithTag(effectTag);
        foreach (var effect in effects)
            Destroy(effect);
    }

    public void RemoveAllNegativeEffects()
    {
        var effects = GameObject.FindGameObjectsWithTag(effectTag);
        foreach (var effect in effects)
            if (effect.GetComponent<Effect>().IsNegative())
                Destroy(effect);
    }
}
