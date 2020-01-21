using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class MajorImageDistortionEffect : Effect
{
    public PostProcessingProfile ppProfile;
    private string effectInfo = "The image distortion will disappear in 10s";

    public override void Activate()
    {
        ppProfile.colorGrading.enabled = true;
        mushroomCollectInfo.setEffectInfo(effectInfo, IsNegative());
    }

    public override void Deactivate()
    {
        ppProfile.colorGrading.enabled = false;
    }

    public override bool IsNegative()
    {
        return true;
    }
}
