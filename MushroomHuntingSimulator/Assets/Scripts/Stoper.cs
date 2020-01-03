using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stoper
{
    private float timePassed;

    public float GetTimePassed()
    {
        return timePassed;
    }

    public void Start()
    {
        timePassed = 0.0f;
    }

    public void IncrementTime(float delta)
    {
        timePassed -= delta;
    }
}
