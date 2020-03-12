using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//Main Author: Hjalmar Andersson

//Secondary Author: Marcus Lundqvist

[CreateAssetMenu(menuName = "Cow/CowMoveState")]
public class CowMoveState : CowBaseState
{
    [SerializeField] private float patrolArea = 10f;
    [SerializeField] private float stoppedPatrolArea = 2f;
    private Vector3 position;


    /// <summary>
    /// Changes values and sets a destination
    /// </summary>
    public override void Enter()
    {
        AIagent.speed = MovementSpeed * 0.6f;
        base.Enter();
        PatrolSprite.SetActive(true);
        if (Stopped)
            SetStoppedDestination();
        else
            SetNewDestination();
    }

    /// <summary>
    /// Sets a new destination and has a 20% chance to go into idle state
    /// </summary>
    public override void Update()
    {
        if (AIagent.remainingDistance < 1.5f && Random.Range(1, 10) <= 8)
        {
            owner.TransitionTo<CowIdleState>();
        }
        else if (AIagent.remainingDistance < 1.5f && !Stopped)
        {
            SetNewDestination();
        }
        else if (Stopped && AIagent.remainingDistance < 1.5f)
        {
            SetStoppedDestination();
        }

        base.Update();
        
    }

    /// <summary>
    /// Changes sprite upon exit
    /// </summary>
    public override void Exit()
    {
        PatrolSprite.SetActive(false);

    }

    /// <summary>
    /// Sets a new destination within a radius of 10
    /// </summary>
    private void SetNewDestination()
    {
        position = new Vector3(Random.Range(-patrolArea, patrolArea), 0, Random.Range(-patrolArea, patrolArea));
        AIagent.SetDestination(Position.position + position);
    }

    /// <summary>
    /// Sets a new destination within a radius of 2
    /// </summary>
    private void SetStoppedDestination()
    {
        position = new Vector3(Random.Range(-stoppedPatrolArea, stoppedPatrolArea), 0, Random.Range(-stoppedPatrolArea, stoppedPatrolArea));
        AIagent.SetDestination(Position.position + position);
    }

    
}
