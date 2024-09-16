using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actions
{
    private PlayerController player;

    public Actions(PlayerController player)
    {
        this.player = player;
    }
    public void Jump()
    {
        if (!CanJump()) return;

        player.rb.velocity = new Vector2(player.rb.velocity.x, player.jumpHeight);
    }

    private bool CanJump()
    {
        return player.IsGrounded() && player.jumpInput;
    }

    public void PickUp()
    {
        if (!CanPickUp()) return;

        player.holding = player.WeaponWithinRange();
        player.holding.holder = player;
    }

    private bool CanPickUp()
    {
        return !player.holding && player.WeaponWithinRange() && player.actionInput;
    }

    public void Attack()
    {
        if (!CanAttack()) return;
        player.holding.Attack();
    }

    private bool CanAttack()
    {
        return player.holding && player.attackInput;
    }

    public void DropWeapon()
    {
        if (!CanDropWeapon()) return;
        Weapon heldWeapon = player.holding;
        heldWeapon.holder = null;
        player.holding = null;
    }

    private bool CanDropWeapon()
    {
        return player.holding && player.dropInput;
    }
}
