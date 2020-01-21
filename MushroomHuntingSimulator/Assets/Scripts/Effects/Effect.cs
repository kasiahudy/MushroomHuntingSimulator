using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour, IEffect
{
    [SerializeField]
    protected float durationSeconds = 10.0f;

    protected GameManager gameManager;
    protected MushroomCollectInfo mushroomCollectInfo;
    protected CountdownTimer durationTimer = new CountdownTimer();

    public virtual void Activate() {}

    public virtual void Deactivate() {}

    public virtual bool IsNegative()
    {
        return false;
    }

    void Start()
    {
        LoadGameManager();
        Activate();
        StartTimer();
    }

    void Awake()
    {
        mushroomCollectInfo = GameObject.Find("MushroomCollectInfo").GetComponent<MushroomCollectInfo>();
    }

    void Update()
    {
        DecrementTime();
        if (HasDurationPassed())
            EndEffect();
    }

    void OnDestroy()
    {
        EndEffect();
    }

    private void LoadGameManager()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void StartTimer()
    {
        durationTimer.StartCountdown(durationSeconds);
    }

    private void DecrementTime()
    {
        durationTimer.DecrementTime(Time.deltaTime);
    }

    private bool HasDurationPassed()
    {
        return durationTimer.HasEnded();
    }

    private void EndEffect()
    {
        Deactivate();
        DestroySelf();
    }

    private void DestroySelf()
    {
        Destroy(this.gameObject);
    }    
}
