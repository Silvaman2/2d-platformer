using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : PlayerBaseState
{
    public override void StartState(PlayerController player)
    {
        Animations(player);
        Jump(player);
    }
    public override void UpdateState(PlayerController player)
    {
        if (HasHitGround(player)) player.ChangeState(player.idleState);


    }
    public override void FixedUpdateState(PlayerController player)
    {
        player.actions.Move();
        if (player.movementInput == 0) return;
        player.SetFacing(player.movementInput);
    }
    public override void Actions(PlayerController player)
    {
        player.actions.DropWeapon();
        player.actions.Attack();
    }

    public override void Animations(PlayerController player)
    {
        player.animator.SetBool("isGrounded", false);
    }

    private void Jump(PlayerController player)
    {
        player.rb.velocity = new Vector2(player.rb.velocity.x, player.jumpHeight);
    }

    private bool HasHitGround(PlayerController player)
    {
        return player.IsGrounded() && player.rb.velocity.y <= 0;
    }
}
