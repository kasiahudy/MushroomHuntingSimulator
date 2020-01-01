using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    [SerializeField]
    private float spawnProbability;
    [SerializeField]
    private int healthPointsDelta;
    [SerializeField]
    private bool edible;

    private GameManager gameManager;

    public float GetSpawnProbability()
    {
        return spawnProbability;
    }

    public bool IsEdible()
    {
        return edible;
    }

    public void Collect()
    {
        AffectPlayerHealthPoints();
        ActivateEffect();
        DestroySelf();
    }

    void Start()
    {
        LoadGameManager();
    }

    private void LoadGameManager()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void AffectPlayerHealthPoints()
    {
        gameManager.UpdatePlayerHealthPoints(healthPointsDelta);
    }

    private void ActivateEffect()
    {
        //TODO implement effect functionality
    }

    private void DestroySelf()
    {
        Destroy(this.gameObject);
    }
}
