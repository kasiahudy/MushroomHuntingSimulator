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

    public GameObject mainMushroom;
    public GameObject basicMushroom;

    private GameManager gameManager;
    private Material mushroomMaterial;
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
        mushroomMaterial.SetColor("_EmissionColor", grey);
    }

    public void AddHighlightForCollection()
    {
        Color grey = new Color(0.2f, 0.2f, 0.2f, 1);
        mushroomMaterial.SetColor("_EmissionColor", grey);
    }

    public void RemoveHighlightForCollection()
    {
        mushroomMaterial.SetColor("_EmissionColor", Color.black);
    }

    public void Collect()
    {
        AffectPlayerHealthPoints();
        if (!gameManager.HasGameEnded())
            ActivateEffect();
        DestroySelf();
    }

    void Start()
    {
        LoadGameManager();
        SetMushroomMaterial(mainMushroom);
        mushroomCollectInfo = GameObject.Find("MushroomCollectInfo").GetComponent<MushroomCollectInfo>();
        originalPlayerHealthPointsDelta = playerHealthPointsDelta;
    }

    private void SetMushroomMaterial(GameObject mushroom)
    {
        Renderer renderer = mushroom.GetComponent<Renderer>();
        mushroomMaterial = renderer.material;
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

    public void changeMushroomToBasic()
    {
        mainMushroom.SetActive(false);
        basicMushroom.SetActive(true);
        SetMushroomMaterial(basicMushroom);
    }

    public void changeMushroomToOriginal()
    {
        mainMushroom.SetActive(true);
        basicMushroom.SetActive(false);
        SetMushroomMaterial(mainMushroom);
    }
}
