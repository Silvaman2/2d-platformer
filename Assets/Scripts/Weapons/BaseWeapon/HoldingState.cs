using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class HoldingState : BaseWeaponState
{
    private Vector3 targetPosition;


    public override void StartState(Weapon stateManager)
    {
        stateManager.attackCooldown = new CountdownTimer(0.3f);
        SetPhysics(stateManager);
        SetSprite(stateManager);
        targetPosition = stateManager.transform.position;
    }

    public override void UpdateState(Weapon stateManager)
    {
        if (CheckIfDropped(stateManager)) return;

        CalculateTargetPosition(stateManager);

        VisualUtils.setSpriteDirection(stateManager.spriteRenderer, stateManager.holder.IsFacingRight());

        CheckInput(stateManager);
    }

    public override void FixedUpdateState(Weapon stateManager)
    {
        UpdatePosition(stateManager);
    }

    private bool CheckIfDropped(Weapon stateManager)
    {
        bool isDropped = !stateManager.IsHeld();
        if(isDropped)
        {
            stateManager.ChangeState(stateManager.droppedState);
        }
        return isDropped;
    }

    private void SetPhysics(Weapon stateManager)
    {
        stateManager.ResetGunRotation();
        stateManager.FreezeGunRotation();
        DisableGravity(stateManager);
    }

    private void SetSprite(Weapon stateManager)
    {
        stateManager.spriteRenderer.sprite = stateManager.heldWeaponSprite;
    }

    private void DisableGravity(Weapon stateManager)
    {
        stateManager.rb.gravityScale = 0f;
    }

    private void CalculateTargetPosition(Weapon stateManager)
    {
        Vector2 distance = stateManager.holder.transform.position - stateManager.transform.position;
        Vector2 recoilVector = new Vector2(stateManager.currentRecoil * stateManager.holder.playerFacing, 0);

        Vector2 targetVelocity = (distance / stateManager.weaponDrag) - recoilVector;
        targetPosition = stateManager.transform.position + new Vector3(targetVelocity.x, targetVelocity.y, 0);
    }

    private void UpdatePosition(Weapon stateManager)
    {
        float playerFacing = stateManager.holder.IsFacingRight() ? 1f : -1f;
        stateManager.transform.position = targetPosition + new Vector3(stateManager.weaponHoldOffset.x * playerFacing, stateManager.weaponHoldOffset.y, 0f);
    }

    private void CheckInput(Weapon stateManager)
    {
        PlayerController player = stateManager.holder;
        if (player.actionInput) stateManager.Attack();
    }
}
