using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingState : PlayerBaseState
{
    public override void StartState(PlayerController player)
    {
        Animations(player);
    }
    public override void UpdateState(PlayerController player)
    {
        if(player.IsGrounded())
        {
            player.ChangeState(player.idleState);
            return;
        }
        Actions(player);
    }
    public override void FixedUpdateState(PlayerController player)
    {
        player.actions.Walk();
    }
    public override void Actions(PlayerController player)
    {
        player.actions.Dash();
        player.actions.PickUp();
        player.actions.DropWeapon();
        player.actions.Attack();
    }
    public override void Animations(PlayerController player)
    {
        player.animator.SetBool("isFalling", true);
        player.animator.SetBool("isGrounded", false);
    }
}
