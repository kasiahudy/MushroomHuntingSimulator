using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    [SerializeField]
    private float spawnProbability;
    [SerializeField]
    private int affectedPlayerHealthPoints;
    [SerializeField]
    private bool edible;
    [SerializeField]
    private GameObject effectPrefab;

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
        gameManager.UpdatePlayerHealthPoints(affectedPlayerHealthPoints);
    }

    private void ActivateEffect()
    {
        if (effectPrefab != null)
            Instantiate(effectPrefab, new Vector3(0, 0, 0), Quaternion.identity);
    }

    private void DestroySelf()
    {
        Destroy(this.gameObject);
    }
}
