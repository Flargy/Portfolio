using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Main Author: Hjalmar Andersson
//Secondary Author: Marcus Lundqvist

[CreateAssetMenu(menuName = "Gnome/GnomeKickedState")]
public class GnomeKickedState : GnomeBaseState
{
    [SerializeField] private float stopTimer;
    [SerializeField] private float kickForce;
    private float timeWaited = 0;

    /// <summary>
    /// Stops the navMesh agent's movement and gives the Vector3 EscapeDirection a nomalized direction facing away from the player.
    /// </summary>
    public override void Enter()
    {
        AIagent.isStopped = true;
        EscapeDirection = Vector3.ProjectOnPlane(Position.position - PlayerKickLocation, Vector3.up).normalized;
    }

    /// <summary>
    /// Stops the game object for a set amount of time and then adds force on the Navmesh agent's velocity. 
    /// </summary>
    public override void Update()
    {
        timeWaited += Time.deltaTime;
        if (timeWaited >= stopTimer)
        {
            AIagent.velocity += Quaternion.Euler(0.0f, Random.Range(-20.0f, 20.0f), 0.0f) * EscapeDirection * kickForce;
            AIagent.isStopped = false;
            owner.TransitionTo<GnomeFleeState>();
        }
    }

    /// <summary>
    /// Resets the timer upon exit.
    /// </summary>
    public override void Exit()
    {
        timeWaited = 0;
    }
}
