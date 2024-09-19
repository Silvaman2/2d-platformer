using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountdownTimer
{
    private float startTime;
    private double countdownTime;

    public CountdownTimer(double seconds)
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

    public bool hasPassedEqual()
    {
        float currentTime = Time.time;
        float timePassed = currentTime - startTime;
        return timePassed >= countdownTime;
    }
}
