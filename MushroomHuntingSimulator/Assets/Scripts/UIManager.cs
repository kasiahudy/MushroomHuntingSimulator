using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private GameManager gameManager;
    private bool welcomeInfoDisplayed = true;

    void Start()
    {
        LoadGameManager();
    }

    void Update()
    {
        if (welcomeInfoDisplayed && gameManager.HasGameStarted())
        {
            transform.Find("WelcomeInfo").gameObject.SetActive(false);
            transform.Find("HealthBar").gameObject.SetActive(true);
            transform.Find("MushroomCollectInfo").gameObject.SetActive(true);
            welcomeInfoDisplayed = false;
        }
        else if (gameManager.HasGameEnded())
        {
            transform.Find("MushroomCollectInfo").gameObject.transform.Find("EffectInfo").gameObject.SetActive(false);
            transform.Find("GameOverInfo").gameObject.SetActive(true);
            DisplayGameOverInfo();
        }
    }

    private void LoadGameManager()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void DisplayGameOverInfo()
    {
        transform.Find("GameOverInfo").GetComponent<TextMeshProUGUI>().text = "Game Over\nYou survived: " + FormatTime(gameManager.GetTimePassed());
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