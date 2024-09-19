using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAfterImage : DisposableGameObject
{
    [SerializeField] public float afterImageLifeSpan;
    [SerializeField] public Color dashColor;

    private CountdownTimer lifeSpanTimer;

    public SpriteRenderer spriteRenderer { get; private set; }

    public new void Start()
    {
        ResetLifeSpanTimer();
        spriteRenderer = GetComponent<SpriteRenderer>();
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
