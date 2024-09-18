using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBullet : DisposableGameObject
{
    [SerializeField] public float travelSpeed;
    [SerializeField] public float lifeSpanInSeconds;
    [SerializeField] public float randomizedLifeSpanOffset;
    [SerializeField] public GameObject bulletDeath;

    protected Rigidbody2D rb;
    protected SpriteRenderer spriteRenderer;
    protected CountdownTimer lifeSpanTimer;
    protected DisposableGameObject bulletDeathInstance;

    public new void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = rb.GetComponent<SpriteRenderer>();
        bulletDeathInstance = GameObject.Instantiate(bulletDeath, Weapon.bulletParent).GetComponent<DisposableGameObject>();
    }

    protected override void SummonStart()
    {
        LifeSpanCounter();
    }

    protected void Update()
    {
        CheckLifeSpan();
    }

    protected void FixedUpdate()
    {
        MoveBulletForward();
    }


    protected void MoveBulletForward()
    {
        Vector3 relativeVelocity = transform.TransformDirection(new Vector2(travelSpeed, 0));
        rb.velocity = relativeVelocity;
    }

    protected void CheckLifeSpan()
    {
        if (!lifeSpanTimer.hasPassed()) return;
        DestroyBullet();
    }

    protected void DestroyBullet()
    {
        bulletDeathInstance.Summon(transform.position, Quaternion.identity);
        this.Hide();
    }

    protected void LifeSpanCounter()
    {
        float randomOffset = GetRandomOffset();
        float resultLifeSpan = lifeSpanInSeconds + randomOffset;
        lifeSpanTimer = new CountdownTimer(resultLifeSpan);
    }

    protected float GetRandomOffset()
    {
        return Random.Range(0, randomizedLifeSpanOffset);
    }
}
