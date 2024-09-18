using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashState : PlayerBaseState
{
    private CountdownTimer dashDurationTimer;
    private float playerGravity;
    public override void StartState(PlayerController player)
    {
        DisableGravity(player);
        Animations(player);
        InitializeDurationTimer(player);
    }

    public override void UpdateState(PlayerController player)
    {
        if(dashDurationTimer.hasPassed())
        {
            player.ChangeState(player.fallingState);
            ResetDashCooldown(player);
            EnableGravity(player);
            return;
        }
        player.visualEffects.dashAfterImage.SummonImage();
        Actions(player);
    }
    public override void FixedUpdateState(PlayerController player)
    {
        DashMovement(player);
    }
    public override void Actions(PlayerController player)
    {
        player.actions.DropWeapon();
    }

    public override void Animations(PlayerController player)
    {
        player.animator.SetTrigger("dash");
    }

    private void DashMovement(PlayerController player)
    {
        player.rb.velocity = new Vector2(player.dashSpeed * player.GetFacing(), player.rb.velocity.y);
    }

    private void InitializeDurationTimer(PlayerController player)
    {
        dashDurationTimer = new CountdownTimer(player.dashDuration);
    }

    private void ResetDashCooldown(PlayerController player)
    {
        player.dashCooldownTimer = new CountdownTimer(player.dashCooldown);
    }

    private void DisableGravity(PlayerController player)
    {
        playerGravity = player.rb.gravityScale;
        player.rb.gravityScale = 0;
        player.rb.velocity = new Vector2(player.rb.velocity.x, 0f);
    }

    private void EnableGravity(PlayerController player)
    {
        player.rb.gravityScale = playerGravity;
    }
}
