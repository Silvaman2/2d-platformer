using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Weapon
{
    public override void Attack()
    {
        if (!attackCooldown.hasPassed()) return;
        for (int i = 0; i < bulletCount; i++)
        {
            SpawnBullet();
        }

        resetAttackCooldown();
        ApplyRecoil();
    }
}
