using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightEdibleMushroomsEffect : Effect
{
    private List<Mushroom> mushrooms;
    private string effectInfo = "All edible mushrooms are hightlighted fo 10s!";

    public override void Activate()
    {
        mushrooms = gameManager.GetMushrooms();
        foreach (var mushroom in mushrooms)
        {
            if (mushroom.IsEdible())
            {
                mushroom.AddStrongHighlightForCollection();
            }
        }
        mushroomCollectInfo.setEffectInfo(effectInfo, IsNegative());
    }

    public override void Deactivate()
    {
        foreach (var mushroom in mushrooms)
        {
            if (mushroom.IsEdible() && mushroom != null)
            {
                mushroom.RemoveHighlightForCollection();
            }
        }
    }

    public override bool IsNegative()
    {
        return false;
    }
}