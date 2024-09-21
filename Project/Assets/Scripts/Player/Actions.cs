using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actions
{
    private PlayerController player;

    public Actions(PlayerController player)
    {
        this.player = player;
    }
    public void Jump()
    {
        if (!CanJump()) return;
        player.ChangeState(player.jumpState);
    }

    private bool CanJump()
    {
        return player.IsGrounded() && player.input.jumpInput;
    }

    public void PickUp()
    {
        if (!CanPickUp()) return;
        player.animator.SetBool("isHoldingWeapon", true);
        player.holding = player.WeaponWithinRange();
        player.holding.holder = player;
    }

    private bool CanPickUp()
    {
        return !player.holding && player.WeaponWithinRange() && player.input.actionInput;
    }

    public void Attack()
    {
        if (!CanAttack()) return;
        player.holding.Attack();
    }

    private bool CanAttack()
    {
        return player.holding && player.input.attackInput;
    }

    public void DropWeapon()
    {
        if (!CanDropWeapon()) return;
        player.animator.SetBool("isHoldingWeapon", false);
        Weapon heldWeapon = player.holding;
        heldWeapon.holder = null;
        player.holding = null;
    }

    private bool CanDropWeapon()
    {
        return player.holding && player.input.dropInput;
    }

    public void Dash()
    {
        if (!CanDash()) return;
        player.ChangeState(player.dashState);
    }

    private bool CanDash()
    {
        return player.input.dashInput && player.dashCooldownTimer.hasPassed();
    }

    public void Move()
    {
        if(player.input.movementInput != 0)
        {
            float speedIncrease = player.moveAcceleration * player.input.movementInput;
            PhysicsUtils.Accelerate(player.rb, speedIncrease, player.moveMaxSpeed);

            if (player.input.movementInput != Mathf.Sign(player.rb.velocity.x)) PhysicsUtils.LerpMovement(player.rb, player.moveDecceleration);
            return;
        }
    }

    public void MoveFacing()
    {
        Move();
        if (player.input.movementInput == 0) return;
        player.SetFacing(player.input.movementInput);
    }

    

    public void Fall()
    {
        player.ChangeState(player.fallingState);
    }

    public void Land()
    {
        if (player.IsMoving())
        {
            player.ChangeState(player.movingState);
            return;
        }
        player.ChangeState(player.idleState);
    }

    public void StopMovement()
    {
        player.rb.velocity = new Vector2(0f, player.rb.velocity.y);
    }
}
