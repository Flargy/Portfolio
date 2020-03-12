using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Main Author: Marcus Lundqvist

[CreateAssetMenu(menuName = "Giant/GiantSearchState")]

public class GiantSearchState : GiantBase
{
    /// <summary>
    /// Increases the movement speed of the game object and sets a new destination for the navmesh.
    /// </summary>
    public override void Enter()
    {
        AIagent.speed = MovementSpeed * 2f;
        AIagent.SetDestination(SearchPosition);
    }

    /// <summary>
    /// Checks if it can see the player or if it has reached it's destination and switches state accordingly.
    /// </summary>
    public override void Update()
    {
        if (CanSeePrey())
        {
            owner.TransitionTo<GiantChaseState>();
        }
        else if (CheckRemainingDistance(SearchPosition, 2f))
            owner.TransitionTo<GiantPatrol>();
        


        base.Update();
    }
}
