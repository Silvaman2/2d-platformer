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

    public PlayerBaseState currentState { get; private set; }

    private PlayerBaseState initialState = new IdleState();
    public PlayerBaseState idleState = new IdleState();
    public PlayerBaseState movingState = new MovingState();
    public PlayerBaseState jumpState = new JumpState();

    public float movementInput;
    public bool jumpInput = false;
    public bool actionInput = false;
    public bool attackInput = false;
    public bool dropInput = false;
    public bool dashInput = false;

    public Actions actions;

    public Weapon holding;

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
        currentState = initialState;
        currentState.StartState(this);
    }

    void Update()
    {
        CheckInput();
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

    private void CheckInput()
    {
        movementInput = Input.GetAxisRaw("Horizontal");
        jumpInput = Input.GetKeyDown(KeyCode.Z);
        actionInput = Input.GetKeyDown(KeyCode.X);
        attackInput = Input.GetKey(KeyCode.X);
        dropInput = Input.GetKeyDown(KeyCode.S);
        dashInput = Input.GetKey(KeyCode.C);
    }

    public bool IsMoving()
    {
        return movementInput != 0f && rb.velocity.x != 0f;
    }

    public bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, 1f, LayerMask.GetMask("Ground")).collider;
    }

    public Weapon WeaponWithinRange()
    {
        Collider2D currentWeapon = Physics2D.CircleCast(coll.bounds.center, pickUpRadius, Vector2.zero, 0f, LayerMask.GetMask("Weapons"))
            .collider;
        if (!currentWeapon) return null; 


        return currentWeapon.gameObject.GetComponent<Weapon>();
    }

    public void SetFacing(float direction)
    {
        spriteRenderer.flipX = direction == 1;
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
