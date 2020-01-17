using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreasePointsInEdibleDecreaseInNotEdibleEffect : Effect
{
    private List<Mushroom> mushrooms;
    private string effectInfo = "All edible mushrooms give 10% more hp and all not edible take 10% less!";

    private float positiveChange = 1.5f;
    private float negativeChange = 0.5f;

    public override void Activate()
    {
        mushrooms = gameManager.GetMushrooms();
        foreach (var mushroom in mushrooms)
        {
            if (mushroom.IsEdible())
            {
                mushroom.changePlayerHealthPointsDelta(positiveChange);
            }
            else
            {
                mushroom.changePlayerHealthPointsDelta(negativeChange);
            }

        }
        mushroomCollectInfo.setEffectInfo(effectInfo, IsNegative());
    }

    public override void Deactivate()
    {
        foreach (var mushroom in mushrooms)
        {
            mushroom.returnToOriginalPlayerHealthPointsDelta();
        }
    }

    public override bool IsNegative()
    {
        return false;
    }
}