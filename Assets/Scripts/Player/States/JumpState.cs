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
        if(IsFalling(player))
        {
            player.actions.Fall();
            return;
        }


        CutOffJump(player);
        Actions(player);
    }
    public override void FixedUpdateState(PlayerController player)
    {
        player.actions.Walk();
    }
    public override void Actions(PlayerController player)
    {
        player.actions.DropWeapon();
        player.actions.Attack();
        player.actions.Dash();
    }

    public override void Animations(PlayerController player)
    {
        player.animator.SetBool("isGrounded", false);
        player.animator.SetBool("isFalling", false);
        player.animator.SetTrigger("jump");
    }

    private void Jump(PlayerController player)
    {
        player.rb.velocity = new Vector2(player.rb.velocity.x, player.jumpHeight);
    }

    private bool IsFalling(PlayerController player)
    {
        return player.rb.velocity.y <= 0;
    }

    private void CutOffJump(PlayerController player)
    {
        if (!player.jumpEndInput) return;
        player.rb.velocity = new Vector2(player.rb.velocity.x, player.rb.velocity.y / 2);
    }
}
