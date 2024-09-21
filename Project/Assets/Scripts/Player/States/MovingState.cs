using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingState : PlayerBaseState
{
    public override void StartState(PlayerController player)
    {
        Animations(player);
    }

    public override void UpdateState(PlayerController player)
    {
        Actions(player);
        if (!player.IsNewState(this)) return;
        if (!player.IsMoving())
        {
            player.ChangeState(player.idleState);
            return;
        }
        player.SetFacing(player.input.movementInput);
        SetAnimationSpeed(player);
    }

    public override void FixedUpdateState(PlayerController player)
    {
        player.actions.Move();
    }
    public override void EndState(PlayerController player)
    {
        ResetAnimationSpeed(player);
    }
    public override void Actions(PlayerController player)
    {
        player.actions.Jump();
        player.actions.DropWeapon();
        player.actions.Attack();
        player.actions.PickUp();
        player.actions.Dash();
        if (!player.IsGrounded())
        {
            player.actions.Fall();
        }
    }

    public override void Animations(PlayerController player)
    {
        player.animator.SetBool("isMoving", true);
        player.animator.SetBool("isGrounded", true);
        player.animator.SetBool("isFalling", false);
    }

    private void SetAnimationSpeed(PlayerController player)
    {
        float animationSpeed = Mathf.Abs(player.rb.velocity.x) / player.moveMaxSpeed;
        player.animator.speed = animationSpeed;
    }

    private void ResetAnimationSpeed(PlayerController player)
    {
        player.animator.speed = 1;
    }
}
