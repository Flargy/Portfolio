using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Main Author: Marcus Lundqvist

[CreateAssetMenu(menuName = "Giant/GiantChaseState")]
public class GiantChaseState : GiantBase
{
    /// <summary>
    /// Increases movement speed upon entry
    /// </summary>
    public override void Enter()
    {
        AIagent.speed = MovementSpeed * 2.5f;
        ChaseSprite.SetActive(true);
    }

    /// <summary>
    /// Attacks if it comes within 5 units or goes back to <see cref="GiantPatrol"/> if it looses it's target.
    /// </summary>
    public override void Update()
    {
        Target = GetClosestTarget();
        if (Target == null )
        {
            owner.TransitionTo<GiantPatrol>();
        }
        if (CheckRemainingDistance(Target.transform.position, 5f))
        {
            owner.TransitionTo<GiantAttackState>();
        }

        SetNewDestination();
        base.Update();
    }

    
    public override void Exit()
    {
        ChaseSprite.SetActive(false);
    }

    /// <summary>
    /// Sets the game objects destination to it's target's position.
    /// </summary>
    public void SetNewDestination()
    {
        AIagent.SetDestination(Target.transform.position);
    }
}
