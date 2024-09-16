using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBullet : MonoBehaviour
{
    [SerializeField] public float travelSpeed;
    [SerializeField] public float lifeSpanInSeconds;

    protected Rigidbody2D rb;
    protected SpriteRenderer spriteRenderer;
    protected CountdownTimer lifeSpanTimer;

    protected void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = rb.GetComponent<SpriteRenderer>();
        lifeSpanTimer = new CountdownTimer(lifeSpanInSeconds);
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
        Object.Destroy(this.gameObject);
    }
}
