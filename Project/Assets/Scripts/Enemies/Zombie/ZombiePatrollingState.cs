using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class ZombiePatrollingState : ZombieBaseState
{
    public override void StartState(Zombie enemy)
    {
        Animations(enemy);
    }
    public override void UpdateState(Zombie enemy)
    {
        if(Mathf.Sign(GetPointDistance(enemy)) == enemy.patrollingDirection) TogglePatrollingDirection(enemy);

    }
    public override void FixedUpdateState(Zombie enemy)
    {
        if (Mathf.Sign(enemy.rb.velocity.x) != enemy.patrollingDirection) PhysicsUtils.LerpMovement(enemy.rb, enemy.patrolDecceleration);
        PhysicsUtils.Accelerate(enemy.rb, enemy.patrolAcceleration * enemy.patrollingDirection, enemy.patrolMaxSpeed);
    }
    public override void EndState(Zombie enemy)
    {
    }
    public override void Animations(Zombie enemy)
    {
    }
    public void TogglePatrollingDirection(Zombie enemy)
    {
        enemy.patrollingDirection *= -1;
        enemy.ToggleSpriteDirection();
        if (enemy.patrollingDirection == 1)
        {
            enemy.targetPoint = enemy.pointB;
            return;
        }
        enemy.targetPoint = enemy.pointA;
    }

    public float GetPointDistance(Zombie enemy)
    {
        return enemy.transform.position.x - enemy.targetPoint.position.x;
    }
}
