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
        if(!player.IsMoving())
        {
            player.ChangeState(player.idleState);
            return;
        }

        Actions(player);
        player.SetFacing(player.input.movementInput);
    }

    public override void FixedUpdateState(PlayerController player)
    {
        Walk(player);
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

    private void Walk(PlayerController player)
    {
        player.actions.Move();
    }


}
