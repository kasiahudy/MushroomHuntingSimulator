using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountdownTimer
{
    private float timeLeft = 0.0f;

    public void StartCountdown(int seconds)
    {
        timeLeft = seconds * 1.0f;
    }

    public void DecrementTime(float delta)
    {
        timeLeft -= delta;
    }

    public bool HasEnded()
    {
        return timeLeft <= 0.0f;
    }

    void Update()
    {
        if (timeLeft > 0.0f)
            timeLeft -= Time.deltaTime;
    }
}
