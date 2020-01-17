using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    [SerializeField]
    private float spawnProbability;
    [SerializeField]
    private int playerHealthPointsDelta;
    private int originalPlayerHealthPointsDelta;
    [SerializeField]
    private bool edible;
    [SerializeField]
    private GameObject effectPrefab;

    private GameManager gameManager;
    private Material mat;
    private MushroomCollectInfo mushroomCollectInfo;

    public float GetSpawnProbability()
    {
        return spawnProbability;
    }

    public bool IsEdible()
    {
        return edible;
    }

    public void AddStrongHighlightForCollection()
    {
        Color grey = new Color(0.2f, 0.4f, 0.2f, 1);
        mat.SetColor("_EmissionColor", grey);
    }

    public void AddHighlightForCollection()
    {
        Color grey = new Color(0.2f, 0.2f, 0.2f, 1);
        mat.SetColor("_EmissionColor", grey);
    }

    public void RemoveHighlightForCollection()
    {
        mat.SetColor("_EmissionColor", Color.black);
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
        Renderer renderer = this.gameObject.transform.GetChild(0).gameObject.GetComponent<Renderer>();
        mat = renderer.material;
        mushroomCollectInfo = GameObject.Find("MushroomCollectInfo").GetComponent<MushroomCollectInfo>();
        originalPlayerHealthPointsDelta = playerHealthPointsDelta;
    }

    private void LoadGameManager()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void AffectPlayerHealthPoints()
    {
        mushroomCollectInfo.setHealthPointDeltaInfo(playerHealthPointsDelta);
        gameManager.UpdatePlayerHealthPoints(playerHealthPointsDelta);
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

    public void changePlayerHealthPointsDelta(float changePercent)
    {
        playerHealthPointsDelta = (int)(playerHealthPointsDelta * changePercent);
    }

    public void returnToOriginalPlayerHealthPointsDelta()
    {
        playerHealthPointsDelta = originalPlayerHealthPointsDelta;
    }
}
