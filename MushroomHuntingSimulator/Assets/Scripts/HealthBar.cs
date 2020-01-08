using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private GameObject healthBarImageObject;
    [SerializeField]
    private GameObject healthBarTextObject;

    private GameManager gameManager;
    private int maxPlayerHealthPoints;
    private int playerHealthPoints;
    private bool shouldUpdateHealthBar = true;

    void Start()
    {
        LoadGameManager();
        LoadMaxPlayerHealthPoints();
    }
       
    void Update()
    {
        UpdatePlayerHealthPoints();
        if (shouldUpdateHealthBar)
            UpdateHealthBar();
    }

    private void LoadGameManager()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void LoadMaxPlayerHealthPoints()
    {
        maxPlayerHealthPoints = gameManager.GetMaxPlayerHealthPoints();
    }

    private void UpdatePlayerHealthPoints()
    {
        int fetched = gameManager.GetPlayerHealthPoints();
        if (fetched != playerHealthPoints)
        {
            playerHealthPoints = fetched;
            shouldUpdateHealthBar = true;
        }
    }

    private void UpdateHealthBar()
    {
        healthBarImageObject.transform.localScale = new Vector3(playerHealthPoints / (float)maxPlayerHealthPoints, 1, 1);
        healthBarTextObject.GetComponent<TextMeshProUGUI>().text = playerHealthPoints + "/" + maxPlayerHealthPoints;
        healthBarImageObject.GetComponent<Image>().color = GetFittingHealthBarColor();
        shouldUpdateHealthBar = false;
    }

    private Color GetFittingHealthBarColor()
    {
        float halfOfPlayerHealthPoints = maxPlayerHealthPoints / 2;
        if ((float)playerHealthPoints > halfOfPlayerHealthPoints)
            return new Color(-((float)playerHealthPoints - (float)maxPlayerHealthPoints) / halfOfPlayerHealthPoints, 1.0f, 0.0f);
        else
            return new Color(1.0f, (float)playerHealthPoints / halfOfPlayerHealthPoints, 0.0f);
    }
}
