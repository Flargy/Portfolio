using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//Main Author: Marcus Lundqvist

//Secondary Author: Hjalmar Andersson

public class CowBaseState : State
{
    #region properties fetched from the State Machine
    protected Transform Position { get { return owner.transform; } }
    protected Quaternion Rotation { get { return owner.transform.rotation; } set { owner.transform.rotation = value; } }
    protected NavMeshAgent AIagent { get { return owner.Agent; } set { owner.Agent = value; } }
    protected int Health { get { return owner.Health; } set { owner.Health = value; } }
    protected GameObject FollowSprite { get { return owner.FollowSprite; } }
    protected GameObject PatrolSprite { get { return owner.PatrolSprite; } }
    protected bool Called { get { return owner.Called; } set { owner.Called = value; } }
    protected bool Attacked { get { return owner.Attacked; } set { owner.Attacked = value; } }
    protected bool Stopped { get { return owner.Stopped; } set { owner.Stopped = value; } }
    protected float HearingRange { get { return owner.HearingRange; } }
    protected float MovementSpeed { get { return owner.MovementSpeed; } }
    protected Vector3 GoToLocation { get { return owner.GoToLocation; } }
    protected GameObject Pen { get { return owner.Pen; } }
    protected bool Captured { get { return owner.Captured; } }
    protected GameObject GameComponent { get { return owner.GameComponent; } }
    protected PhysicsComponent OwnerPhysics{get { return owner.GetComponent<PhysicsComponent>(); } }
    protected float FollowDistance { get { return owner.FollowDistance; } }
    protected LayerMask CollisionMask { get { return owner.CollisonMask; } }
    protected GameObject Player { get { return owner.Player; } }
    private CapsuleCollider CapsuleCollider { get { return owner.GetComponentInChildren<CapsuleCollider>(); } }
    #endregion


    private RaycastHit capsuleRaycast;



    //values that can change from state to state
    protected CowSM owner;

    /// <summary>
    /// Initializes values upon awake   
    /// </summary>
    /// <param name="owner"> The sate machine used by the script</param>
    public override void Initialize(StateMachine owner)
    {
        this.owner = (CowSM)owner;
        OwnerPhysics.SetAirResistance(0.95f);
        FollowSprite.SetActive(false);
        PatrolSprite.SetActive(false);

    }

    /// <summary>
    /// Keeps track of the units health value and triggers the dying state if it reaches a value of 0 or less
    /// </summary>
    public override void Update()
    {


        if (Health <= 0)
        {
            owner.TransitionTo<CowDyingState>();
        }

        //OwnerPhysics.SetVelocity(AIagent.velocity / Time.deltaTime);

        //if (Vector3.Distance(Position.position, GameComponent.GetComponent<GameComponetns>().PlayerPosition) > GameComponent.GetComponent<GameComponetns>().renderDistance)
        //    owner.TransitionTo<CowNonRenderState>();
    }
    
}
