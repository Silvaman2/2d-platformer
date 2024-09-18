using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Weapon
{
    [SerializeField] protected int bulletInstanceCount;
    public override void Attack()
    {
        if (!attackCooldown.hasPassed()) return;
        for (int i = 0; i < bulletCount; i++)
        {
            SummonBullet();
        }

        resetAttackCooldown();
        ApplyRecoil();
    }

    protected override int GetBulletInstanceCount()
    {
        return bulletInstanceCount + 1;
    }
}
