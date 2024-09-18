using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAfterImage : DisposableGameObject
{
    [SerializeField] public float afterImageLifeSpan;
    [SerializeField] public Color dashColor;

    private CountdownTimer lifeSpanTimer;

    public new void Start()
    {
        ResetLifeSpanTimer();
        base.Start();
    }

    public void Update()
    {
        if (!lifeSpanTimer.hasPassed()) return;
        Hide();
    }

    protected override void SummonStart()
    {
        ResetLifeSpanTimer();
    }
    protected void ResetLifeSpanTimer()
    {
        lifeSpanTimer = new CountdownTimer(afterImageLifeSpan);
    }
}
