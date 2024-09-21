using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PhysicsUtils
{
    
    public static void Accelerate(Rigidbody2D rb, float speedIncrease, float maxSpeed)
    {
        rb.AddForce(new Vector2(speedIncrease, 0));
        float finalSpeed = Mathf.Clamp(rb.velocity.x, -maxSpeed, maxSpeed);
        rb.velocity = new Vector2(finalSpeed, rb.velocity.y);
    }

    public static void LerpMovement(Rigidbody2D rb, float decceleration)
    {
        rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, 0, decceleration), rb.velocity.y);
    }
}
