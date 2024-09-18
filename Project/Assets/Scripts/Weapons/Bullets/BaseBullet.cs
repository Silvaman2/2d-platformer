using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBullet : MonoBehaviour
{
    [SerializeField] public float travelSpeed;
    [SerializeField] public float lifeSpanInSeconds;
    [SerializeField] public float randomizedLifeSpanOffset;
    [SerializeField] public GameObject bulletDeath;

    protected Rigidbody2D rb;
    protected SpriteRenderer spriteRenderer;
    protected CountdownTimer lifeSpanTimer;

    protected void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = rb.GetComponent<SpriteRenderer>();
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
        GameObject.Instantiate(bulletDeath, transform.position, Quaternion.identity);
        GameObject.Destroy(gameObject);
    }

    protected void LifeSpanCounter()
    {
        float randomOffset = Random.Range(0, randomizedLifeSpanOffset);
        float resultLifeSpan = lifeSpanInSeconds + randomOffset;
        lifeSpanTimer = new CountdownTimer(resultLifeSpan);
    }
}
