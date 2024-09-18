using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AK47 : Weapon
{

    public override void Attack()
    {
        if (!attackCooldown.hasPassed()) return;
        SummonBullet();
        resetAttackCooldown();
        ApplyRecoil();
    }

    protected override int GetBulletInstanceCount()
    {
        return GetBulletInstanceCountRapidFire();
    }
}