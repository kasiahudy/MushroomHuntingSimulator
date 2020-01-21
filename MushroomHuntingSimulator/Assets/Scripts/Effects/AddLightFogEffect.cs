using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddLightFogEffect : Effect
{
    [SerializeField]
    private string effectTag = "Effect";
    private string effectInfo = "The fog will disappear in 10s";
    private string effectName = "AddLightFogEffect";

    public override void Activate()
    {
        RenderSettings.fog = true;
        mushroomCollectInfo.setEffectInfo(effectInfo, IsNegative());
    }

    public override void Deactivate()
    {
        var effects = GameObject.FindGameObjectsWithTag(effectTag);
        foreach (var effect in effects)
        {
            if (effect.name.Replace("(Clone)", "") == effectName)
            {
                return;
            }
        }
        RenderSettings.fog = false;
    }

    public override bool IsNegative()
    {
        return true;
    }
}
