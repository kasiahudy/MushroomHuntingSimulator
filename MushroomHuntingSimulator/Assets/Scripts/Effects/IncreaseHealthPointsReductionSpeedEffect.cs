﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseHealthPointsReductionSpeedEffect : Effect
{
    [SerializeField]
    private float increasePercent = 50.0f;

    private float timeDelta;

    private string effectInfo = "Health points reduce faster for 10s!";

    public override void Activate()
    {
        float initial = gameManager.GetDecreaseOfHealthPointsTimeSeconds();
        timeDelta = initial * increasePercent * 0.01f ;
        gameManager.SetDecreaseOfHealthPointsTimeSeconds(initial - timeDelta);
        mushroomCollectInfo.setEffectInfo(effectInfo, IsNegative());
    }

    public override void Deactivate()
    {
        gameManager.SetDecreaseOfHealthPointsTimeSeconds(gameManager.GetDecreaseOfHealthPointsTimeSeconds() + timeDelta);
    }

    public override bool IsNegative()
    {
        return true;
    }
}
