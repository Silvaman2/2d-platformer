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
        if(player.movementInput == 0)
        {
            player.ChangeState(player.idleState);
            return;
        }

        Actions(player);

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
    }

    public override void Animations(PlayerController player)
    {
        player.animator.SetBool("isMoving", true);
        player.animator.SetBool("isGrounded", true);
    }

    private void Walk(PlayerController player)
    {
        player.actions.Move();
        player.SetFacing(player.movementInput);
    }


}
