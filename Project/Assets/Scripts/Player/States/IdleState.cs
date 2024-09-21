using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : PlayerBaseState
{
    public override void StartState(PlayerController player)
    {
        Animations(player);
    }

    public override void UpdateState(PlayerController player)
    {
        Actions(player);
        if (!player.IsNewState(this)) return;
        if (player.input.movementInput != 0)
        {
            player.ChangeState(player.movingState);
            return;
        }
    }

    public override void FixedUpdateState(PlayerController player)
    {
        PhysicsUtils.LerpMovement(player.rb, player.moveDecceleration);
    }

    public override void EndState(PlayerController player)
    {
    }

    public override void Actions(PlayerController player)
    {
        player.actions.Jump();
        player.actions.DropWeapon();
        player.actions.Attack();
        player.actions.PickUp();
        player.actions.Dash();
        if(!player.IsGrounded())
        {
            player.actions.Fall();
        }
    }

    public override void Animations(PlayerController player)
    {
        player.animator.SetBool("isMoving", false);
        player.animator.SetBool("isGrounded", true);
        player.animator.SetBool("isFalling", false);
    }
}
