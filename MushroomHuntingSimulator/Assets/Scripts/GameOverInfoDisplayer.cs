using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverInfoDisplayer : MonoBehaviour
{
    private GameManager gameManager;

    void Start()
    {
        LoadGameManager();
    }

    void Update()
    {
        if (PlayerHealthReachedZero())
            ShowGameOverInfo();
    }

    private void LoadGameManager()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private bool PlayerHealthReachedZero()
    {
        return gameManager.GetPlayerHealthPoints() == 0;
    }

    private void ShowGameOverInfo()
    {
        GetComponent<TextMeshProUGUI>().text = "Game Over\nYou survived: " + FormatTime(gameManager.GetTimePassed());
    }

    private string FormatTime(float time)
    {
        string text = "";
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
