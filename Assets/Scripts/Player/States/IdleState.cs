using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : PlayerBaseState
{
    public override void StartState(PlayerController player)
    {
        StopPlayer(player);
        Animations(player);
    }

    public override void UpdateState(PlayerController player)
    {
        if(player.movementInput != 0)
        {
            player.ChangeState(player.movingState);
            return;
        }

        Actions(player);
    }

    public override void FixedUpdateState(PlayerController player)
    {

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
        player.animator.SetBool("isMoving", false);
        player.animator.SetBool("isGrounded", true);
    }

    private void StopPlayer(PlayerController player)
    {
        player.rb.velocity = new Vector2(0f, player.rb.velocity.y);
    }
}
