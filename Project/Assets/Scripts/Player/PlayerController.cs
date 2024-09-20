using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public float moveSpeed;
    [SerializeField] public float moveAcceleration;
    [SerializeField] public float moveDeceleration;
    [SerializeField] public float moveMaxSpeed;
    [SerializeField] public float jumpHeight;
    [SerializeField] public float pickUpRadius;
    [SerializeField] public float dashSpeed;
    [SerializeField] public float dashDuration;
    [SerializeField] public float dashCooldown;
    [SerializeField] public DashAfterImage dashAfterImageObject;

    public PlayerBaseState currentState { get; private set; }

    private PlayerBaseState initialState = new IdleState();
    public PlayerBaseState idleState = new IdleState();
    public PlayerBaseState movingState = new MovingState();
    public PlayerBaseState jumpState = new JumpState();
    public PlayerBaseState dashState = new DashState();
    public PlayerBaseState fallingState = new FallingState();

    

    public Actions actions;
    public Input input;
    public PlayerVisualEffects visualEffects;
    public Weapon holding;
    public CountdownTimer dashCooldownTimer;

    public Rigidbody2D rb { get; private set; }
    public SpriteRenderer spriteRenderer { get; private set; }
    public BoxCollider2D coll { get; private set; }
    public Animator animator { get; private set; }
    void Start()
    {
        actions = new Actions(this);

        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        input = GetComponent<Input>();

        currentState = initialState;
        currentState.StartState(this);

        dashCooldownTimer = new CountdownTimer(dashCooldown);
        visualEffects = new PlayerVisualEffects(this);

    }

    void Update()
    {
        currentState.UpdateState(this);
    }

    void FixedUpdate()
    {
        currentState.FixedUpdateState(this);
    }

    public void ChangeState(PlayerBaseState newState)
    {
        currentState = newState;
        currentState.StartState(this);
    }

    public bool IsMoving()
    {
        return input.movementInput != 0f && rb.velocity.x != 0f;
    }

    public bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size - new Vector3(0.1f, 0), 0f, Vector2.down, 1f, LayerMask.GetMask("Ground")).collider;
    }

    public Weapon WeaponWithinRange()
    {
        List<Collider2D> weaponsWithinRange = Physics2D.OverlapCircleAll(transform.position, pickUpRadius, LayerMask.GetMask("Weapons")).ToList();
        if (weaponsWithinRange.Count == 0) return null;

        weaponsWithinRange.Sort((a, b) => {
            float distanceA = Vector2.Distance(transform.position, a.gameObject.transform.position);
            float distanceB = Vector2.Distance(transform.position, b.gameObject.transform.position);
            return distanceA.CompareTo(distanceB);
        });

        Collider2D nearestWeapon = weaponsWithinRange[0];

        return nearestWeapon.gameObject.GetComponent<Weapon>();
    }

    public void SetFacing(float direction)
    {
        spriteRenderer.flipX = direction < 0;
    }

    public bool IsFacingRight()
    {
        return spriteRenderer.flipX;
    }

    public float GetFacing()
    {
        if (IsFacingRight()) return -1;
        return 1;
    }
}
