using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeAllMushroomsToIndistinguishableEffect : Effect
{
    private List<Mushroom> mushrooms;
    private string effectInfo = "Mushrooms are indistinguishable!";

    public override void Activate()
    {
        mushrooms = gameManager.GetMushrooms();
        foreach (var mushroom in mushrooms)
        {
            mushroom.changeMushroomToBasic();
        }
        mushroomCollectInfo.setEffectInfo(effectInfo, IsNegative());
    }

    public override void Deactivate()
    {
        foreach (var mushroom in mushrooms)
        {
            if (mushroom != null)
            {
                mushroom.changeMushroomToOriginal();
            }
        }
    }

    public override bool IsNegative()
    {
        return true;
    }
}
