using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//Main Author: Hjalmar Andersson

//Secondary Author: Marcus Lundqvist

[CreateAssetMenu(menuName = "Cow/CowFleeState")]

public class CowFleeState : CowBaseState
{
    /// <summary>
    /// Sets values and fleeing position upon enter.
    /// </summary>
    public override void Enter()
    {
        AIagent.speed = MovementSpeed * 1.5f;
        Vector3 position;
        int currentTry = 0;
        do{
            position = new Vector3(Random.Range(-30, 30), 0, Random.Range(-30, 30));
            currentTry++;
        } while (position.magnitude < 15f && currentTry < 10f);
        AIagent.SetDestination(Position.position + position);  
    }

    /// <summary>
    /// Checks remaining distance
    /// </summary>
    public override void Update()
    {
        base.Update();

        if (AIagent.remainingDistance < 2f)
        {
            owner.TransitionTo<CowMoveState>();
        }
    }

    /// <summary>
    /// resets the position and changes the value of Attacked
    /// </summary>
    public override void Exit()
    {
        AIagent.SetDestination(Position.position);
        Attacked = false;
    }
}
