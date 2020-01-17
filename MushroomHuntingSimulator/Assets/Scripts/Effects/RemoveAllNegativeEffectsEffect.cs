using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveAllNegativeEffectsEffect : Effect
{
    private string effectInfo = "All negative effects are removed!";

    public override void Activate()
    {
        gameManager.RemoveAllNegativeEffects();
        mushroomCollectInfo.setEffectInfo(effectInfo, IsNegative());
    }

    public override void Deactivate()
    {

    }

    public override bool IsNegative()
    {
        return false;
    }
}