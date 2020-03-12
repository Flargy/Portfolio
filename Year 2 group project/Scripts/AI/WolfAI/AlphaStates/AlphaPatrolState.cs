using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Author: Hjalmar Andersson

[CreateAssetMenu(menuName = "Alpha/AlphaPatrolState")]
public class AlphaPatrolState : AlphaBaseState
{
    [SerializeField] private float patrolArea = 20f;

    /// <summary>
    /// Sets values upon entry.
    /// </summary>
    public override void Enter()
    {
        PatrolSprite.SetActive(true);
        AIagent.speed = GetPatrolSpeed();
        SetNewDestination();
    }

    /// <summary>
    /// Contains conditions for state transition to <see cref="AlphaChaseState"/> and <see cref="AlphaIdleState"/>
    /// </summary>
    public override void Update()
    {
        if (CanSeePrey())
        {
            owner.TransitionTo<AlphaChaseState>();
            PreyLocated = true;
        }

        if (AIagent.remainingDistance < 1f)
        {
            owner.TransitionTo<AlphaIdleState>();
        }
        setVelocity();
        base.Update();
    }

    /// <summary>
    /// Disables sprite upon exit.
    /// </summary>
    public override void Exit()
    {
        PatrolSprite.SetActive(false);
    }

    /// <summary>
    /// Sets a new destination for the NavMeshAgent owner.
    /// </summary>
    private void SetNewDestination()
    {
        Vector3 position = new Vector3(Random.Range(-patrolArea, patrolArea), 0, Random.Range(-patrolArea, patrolArea));
        AIagent.SetDestination(Position.position + position);
        Destination = AIagent.destination;
    }
}
