using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class MajorImageDistortionEffect : Effect
{
    [SerializeField]
    private string effectTag = "Effect";
    public PostProcessingProfile ppProfile;
    private string effectInfo = "The image distortion will disappear in 10s";
    private string effectName = "MajorImageDistortionEffect";

    public override void Activate()
    {
        ppProfile.colorGrading.enabled = true;
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
        ppProfile.colorGrading.enabled = false;
    }

    public override bool IsNegative()
    {
        return true;
    }
}
