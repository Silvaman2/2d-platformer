using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : Enemy
{
    [SerializeField] public float patrolAcceleration;
    [SerializeField] public float patrolDecceleration;
    [SerializeField] public float patrolMaxSpeed;

    [SerializeField] public Transform pointA;
    [SerializeField] public Transform pointB;
    


    private ZombieBaseState currentState;
    private ZombieBaseState newState;

    public ZombieBaseState patrollingState = new ZombiePatrollingState();

    public float patrollingDirection = 1;
    public Transform targetPoint;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        newState = patrollingState;
        currentState = newState;
        currentState.StartState(this);
        targetPoint = pointB;
    }
    void Update()
    {
        base.Update();
        if(!currentState.Equals(newState))
        {
            currentState = newState;
            currentState.StartState(this);
        }
        currentState.UpdateState(this);
    }
    void FixedUpdate()
    {
        currentState.FixedUpdateState(this);
    }

    public void ChangeState(ZombieBaseState newState)
    {
        currentState.EndState(this);
        this.newState = newState;
    }

    public void ToggleSpriteDirection()
    {
        spriteRenderer.flipX = !spriteRenderer.flipX;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(pointA.position, 5f);
        Gizmos.DrawWireSphere(pointB.position, 5f);
    }
}
