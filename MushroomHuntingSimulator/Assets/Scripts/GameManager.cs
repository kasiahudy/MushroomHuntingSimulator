using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    private Text healthBarText;

    private List<Mushroom> mushrooms;
    private int playerHealthPoints;
    private int healthPointsReductionDelta;
    private CountdownTimer decreaseOfHealthPointsTimer = new CountdownTimer();
    private CountdownTimer increaseOfHealthPointsReductionDeltaTimer = new CountdownTimer();

    public void UpdatePlayerHealthPoints(int delta)
    {
        playerHealthPoints += delta;
        CheckPlayerHealth();
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
        StartTimers();
        healthBarText = healthBarTextObject.GetComponent<Text>();
    }

    void Update()
    {
        DecrementTime();
        if (increaseOfHealthPointsReductionDeltaTimer.HasEnded())
        {
            IncreaseHealthPointsReductionDelta(1);
            StartIncreaseOfHealthPointsReductionDeltaTimer();
        }
        if (decreaseOfHealthPointsTimer.HasEnded())
        {
            DecreasePlayerHealthPoints();
            StartDecreaseOfHealthPointsTimer();
            Debug.Log(playerHealthPoints); // to delete later
            ChangeHealthBar();
        }
        CheckPlayerHealth();
    }

    private void ChangeHealthBar()
    {
        healthBar.localScale = new Vector3(playerHealthPoints / (float)maxPlayerHealthPoints, 1, 1);
        healthBarText.text = playerHealthPoints + "/" + maxPlayerHealthPoints;
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

    private void StartTimers()
    {
        StartDecreaseOfHealthPointsTimer();
        StartIncreaseOfHealthPointsReductionDeltaTimer();
    }

    private void StartDecreaseOfHealthPointsTimer()
    {
        decreaseOfHealthPointsTimer.StartCountdown(decreaseOfHealthPointsTimeSeconds);
    }

    private void StartIncreaseOfHealthPointsReductionDeltaTimer()
    {
        increaseOfHealthPointsReductionDeltaTimer.StartCountdown(increaseOfHealthPointsReductionDeltaTimeSeconds);
    }

    private void DecrementTime()
    {
        decreaseOfHealthPointsTimer.DecrementTime(Time.deltaTime);
        increaseOfHealthPointsReductionDeltaTimer.DecrementTime(Time.deltaTime);
    }

    private void DecreasePlayerHealthPoints()
    {
        playerHealthPoints -= healthPointsReductionDelta;
    }

    private void IncreaseHealthPointsReductionDelta(int value)
    {
        healthPointsReductionDelta += value;
    }

    private void CheckPlayerHealth()
    {
        if (playerHealthPoints <= 0)
            EndGame();
    }

    private void EndGame()
    {
        Debug.Log("Koniec gry");
        //TODO w tym miejscu implementacja konca gry
    }
}
