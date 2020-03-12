using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Main Author: Marcus Lundqvist

[CreateAssetMenu(menuName = "Cow/RunAheadState")]
public class CowRunAheadState : CowBaseState
{
    [SerializeField] private float randomArea = 2;
    private Vector3 randomLocation;

    /// <summary>
    /// Sets a random destination within a radius of 2 within the assigned destination.
    /// </summary>
    public override void Enter()
    {
        AIagent.speed = MovementSpeed * 1.5f;
        randomLocation = new Vector3(Random.Range(-randomArea, randomArea), 0, Random.Range(-randomArea, randomArea)) + GoToLocation;
        AIagent.SetDestination(randomLocation);
    }

    /// <summary>
    /// Transitions to <see cref="CowMoveState"/> if it comes within 2 units of it's destination.
    /// </summary>
    public override void Update()
    {
        base.Update();
       
        if(AIagent.remainingDistance < 2.0f)
        {
            owner.TransitionTo<CowMoveState>();
        }
    }

  
}
