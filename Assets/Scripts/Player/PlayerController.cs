using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public float moveSpeed;
    [SerializeField] public float jumpHeight;
    [SerializeField] public float pickUpRadius;
    [SerializeField] public float dashSpeed;
    [SerializeField] private float dashDuration;
    [SerializeField] private float dashCooldown;


    private float movementInput;
    public bool jumpInput = false;
    public bool actionInput = false;
    public bool attackInput = false;
    public bool dropInput = false;
    public bool dashInput = false;

    public Actions actions;

    private Weapon holding;
    public float playerFacing = 1;
    private bool isDashing = false;
    private CountdownTimer dashDurationTimer;
    private CountdownTimer dashCooldownTimer;
    private float dashDirection;

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
        dashDurationTimer = new CountdownTimer(0.1f);
        dashCooldownTimer = new CountdownTimer(0.1f);
    }

    void Update()
    {
        CheckInput();
        ApplyActions();
        SetPlayerDirection();
        ApplyAnimation();
    }

    void FixedUpdate()
    {
        ApplyMovement();
        Dash();
    }

    private void CheckInput()
    {
        movementInput = Input.GetAxisRaw("Horizontal");
        jumpInput = Input.GetKeyDown(KeyCode.Z);
        actionInput = Input.GetKeyDown(KeyCode.X);
        attackInput = Input.GetKey(KeyCode.X);
        dropInput = Input.GetKeyDown(KeyCode.S);
        dashInput = Input.GetKey(KeyCode.C);
    }

    private void ApplyActions()
    {
        Jump();
        PickUp();
        Attack();
        DropWeapon();
    }

    public void Jump()
    {
        if (!CanJump()) return;

        rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
    }

    private bool CanJump()
    {
        return IsGrounded() && jumpInput;
    }

    public void PickUp()
    {
        if (!CanPickUp()) return;

        holding = WeaponWithinRange();
        holding.holder = this;
    }

    private bool CanPickUp()
    {
        return !holding && WeaponWithinRange() && actionInput;
    }

    public void Attack()
    {
        if (!CanAttack()) return;
        holding.Attack();
    }

    private bool CanAttack() {
        return holding && attackInput;
    }

    public void DropWeapon()
    {
        if (!CanDropWeapon()) return;
        holding.holder = null;
        holding = null;
    }

    private bool CanDropWeapon()
    {
        return holding && dropInput;
    }

    public void Dash()
    {
        if (!dashCooldownTimer.hasPassed()) return;
        if (isDashing)
        {
            if (dashDurationTimer.hasPassed())
            {
                EndDash();
                return;
            }

            rb.velocity = new Vector2(dashSpeed * dashDirection, 0);
            return;
        }

        if(dashInput && IsMoving())
        {
            InitiateDash();
        }
    }

    private void InitiateDash() {
        isDashing = true;
        dashDirection = playerFacing;
        dashDurationTimer = new CountdownTimer(dashDuration);
    }

    private void EndDash()
    {
        isDashing = false;
        dashCooldownTimer = new CountdownTimer(dashCooldown);
    }

    private void SetPlayerDirection()
    {
        if (IsMoving()) playerFacing = movementInput;
        VisualUtils.setSpriteDirection(spriteRenderer, !IsFacingRight());
    }

    private void ApplyMovement()
    {
        rb.velocity = new Vector2(moveSpeed * movementInput, rb.velocity.y);
    }
    private void ApplyAnimation()
    { 
        animator.SetBool("isMoving", IsMoving());
        animator.SetBool("isGrounded", IsGrounded());
    }

    public bool IsFacingRight()
    {
        return playerFacing == 1;
    }

    public bool IsMoving()
    {
        return movementInput != 0f && rb.velocity.x != 0f;
    }

    public bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, 1f, LayerMask.GetMask("Ground")).collider;
    }

    private Weapon WeaponWithinRange()
    {
        Collider2D currentWeapon = Physics2D.CircleCast(coll.bounds.center, pickUpRadius, Vector2.zero, 0f, LayerMask.GetMask("Weapons"))
            .collider;
        if (!currentWeapon) return null; 


        return currentWeapon.gameObject.GetComponent<Weapon>();
    }

    
}
