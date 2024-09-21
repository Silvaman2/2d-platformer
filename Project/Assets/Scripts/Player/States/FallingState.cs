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
            player.actions.Land();
            return;
        }
        Actions(player);
    }
    public override void FixedUpdateState(PlayerController player)
    {
        player.actions.MoveFacing();
    }
    public override void EndState(PlayerController player)
    {
    }
    public override void Actions(PlayerController player)
    {
        player.actions.Dash();
        player.actions.DropWeapon();
        player.actions.Attack();
    }
    public override void Animations(PlayerController player)
    {
        player.animator.SetBool("isFalling", true);
        player.animator.SetBool("isGrounded", false);
    }
}
