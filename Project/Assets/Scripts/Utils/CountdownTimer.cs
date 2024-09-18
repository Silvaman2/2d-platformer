using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountdownTimer
{
    private float startTime;
    private float countdownTime;

    public CountdownTimer(float seconds)
    {
        startTime = Time.time;
        countdownTime = seconds;
    }

    public bool hasPassed()
    {
        float currentTime = Time.time;
        float timePassed = currentTime - startTime;
        return timePassed > countdownTime;
    }
}
