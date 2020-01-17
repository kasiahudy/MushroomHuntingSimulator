using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddLightFogEffect : Effect
{
    private string effectInfo = "Health points reduce faster for 10s!";

    public override void Activate()
    {
        RenderSettings.fog = true;
        mushroomCollectInfo.setEffectInfo(effectInfo, IsNegative());
    }

    public override void Deactivate()
    {
        RenderSettings.fog = false;
    }

    public override bool IsNegative()
    {
        return true;
    }
}
