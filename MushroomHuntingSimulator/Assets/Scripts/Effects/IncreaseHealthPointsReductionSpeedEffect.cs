using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseHealthPointsReductionSpeedEffect : Effect
{
    [SerializeField]
    private float increasePercent = 50.0f;

    private float initialSpeed;

    public override void Activate()
    {
        initialSpeed = gameManager.GetDecreaseOfHealthPointsTimeSeconds();
        gameManager.SetDecreaseOfHealthPointsTimeSeconds(initialSpeed + initialSpeed * increasePercent);
    }

    public override void Deactivate()
    {
        gameManager.SetDecreaseOfHealthPointsTimeSeconds(initialSpeed);
    }

    public override bool IsNegative()
    {
        return false;
    }
}
