using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//Author: Hjalmar Andersson

public class GnomeBaseState : State
{
    protected LayerMask CollisionMask { get { return owner.CollisionMask; } }
    protected Transform Position { get { return owner.transform; } }
    protected NavMeshAgent AIagent { get { return owner.Agent; } set { owner.Agent = value; } }
    protected PhysicsComponent OwnerPhysics { get { return owner.GetComponent<PhysicsComponent>(); } }
    protected Vector3 Velocity { get { return owner.Velocity; } set { owner.Velocity = value; } }
    protected GameObject Target { get { return owner.Target; } }
    protected Vector3 Gnomergan { get { return owner.Gnomergan; } set { owner.Gnomergan = value; } }
    protected float Speed { get { return owner.Speed; } }
    protected Vector3 PlayerKickLocation { get { return owner.PlayerKickLocation; } }
    protected Vector3 EscapeDirection { get { return owner.EscapeDirection; } set { owner.EscapeDirection = value; } }

    //values that can change from state to state
    protected GnomeStateMachine owner;
    protected RaycastHit capsuleRaycast;
    protected float skinWidth = 0.1f;


    /// <summary>
    /// Assigns values to variables on initialization.
    /// </summary>
    /// <param name="owner"> Reference to the object the <see cref="GnomeStateMachine"/> is attached to</param>
    public override void Initialize(StateMachine owner)
    {
        this.owner = (GnomeStateMachine)owner;
        OwnerPhysics.SetAirResistance(0.95f);
        AIagent.speed = Speed;
    }

   
    public override void Update()
    {
    }

    /// <summary>
    /// Assignes a direction in which the game object will move towards.
    /// </summary>
    protected void FleeToGnomergan()
    {
        Gnomergan = EscapeDirection * Random.Range(10, 20);
    }

    public bool CheckRemainingDistance(Vector3 destination, float acceptableRange)
    {
        return (destination - Position.position).sqrMagnitude < acceptableRange * acceptableRange;
    }
}
