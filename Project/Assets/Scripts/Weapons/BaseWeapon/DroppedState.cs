using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedState : BaseWeaponState
{

    public override void StartState(Weapon stateManager)
    {
        SetPhysics(stateManager);
        SetSprite(stateManager);
    }

    public override void UpdateState(Weapon stateManager)
    {
        if(stateManager.IsHeld())
        {
            stateManager.ChangeState(stateManager.holdingState);
            return;
        }
    }

    public override void FixedUpdateState(Weapon stateManager)
    {

    }

    private void SetPhysics(Weapon stateManager)
    {
        stateManager.UnfreezeGunRotation();
        EnableWeaponGravity(stateManager);
    }

    private void SetSprite(Weapon stateManager)
    {
        stateManager.spriteRenderer.sprite = stateManager.weaponSprite;
    }

    private void EnableWeaponGravity(Weapon stateManager)
    {
        stateManager.rb.gravityScale = stateManager.weaponGravity;
    }

    private void EnableCollision(Weapon stateManager)
    {
        stateManager.coll.enabled = true;
    }

    
}
