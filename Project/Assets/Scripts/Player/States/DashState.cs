using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashState : PlayerBaseState
{
    private CountdownTimer dashDurationTimer;
    private float playerGravity;
    public override void StartState(PlayerController player)
    {
        AdjustGravity(player);
        Animations(player);
        InitializeDurationTimer(player);
        player.visualEffects.dashAfterImage.ResetAfterImageEffect();
    }

    public override void UpdateState(PlayerController player)
    {
        if (dashDurationTimer.hasPassed())
        {
            player.ChangeState(player.idleState);
            ResetDashCooldown(player);
            EnableGravity(player);
            return;
        }
        Actions(player);
    }
    public override void FixedUpdateState(PlayerController player)
    {
        player.visualEffects.dashAfterImage.SummonImage();
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

    private void AdjustGravity(PlayerController player)
    {
        playerGravity = player.rb.gravityScale;
        player.rb.gravityScale /= 2;
        player.rb.velocity = new Vector2(player.rb.velocity.x, 0f);
    }

    private void EnableGravity(PlayerController player)
    {
        player.rb.gravityScale = playerGravity;
    }
}
