using UnityEngine;

//Main Author: Marcus Lundqvist

public class GiantBase : State
{
    #region Fetched properties from GiantSM
    private CapsuleCollider CapsuleCollider { get { return owner.GetComponent<CapsuleCollider>(); } } 
    protected float DetectionRange { get { return owner.DetectionRange; } }
    protected float FollowRange { get { return owner.DetectionRange; } }
    protected Transform Position { get { return owner.transform; } }
    protected Quaternion rotatin { get { return owner.transform.rotation; } set { owner.transform.rotation = value; } }
    protected UnityEngine.AI.NavMeshAgent AIagent { get { return owner.Agent; } set { owner.Agent = value; } }
    protected GameObject Target { get { return owner.Target; } set { owner.Target = value; } }
    protected GameObject GameComponent { get { return owner.GameComponent; } }
    protected int Damage { get { return owner.Damage; } }
    protected bool IsNight { get { return owner.IsNight; } set { owner.IsNight = value; } }
    protected Vector3 StartPosition { get { return owner.StartPosition; } }
    protected float MovementSpeed { get { return owner.MovementSpeed ; } }
    protected Vector3 SearchPosition { get { return owner.SearchPosition; } }
    public LayerMask CollisionMask { get { return owner.CollisonMask; } }
    public Material DayMaterial { get { return owner.DayMaterial; } }
    public Material NightMaterial { get { return owner.NightMaterial; } }
    public float ShoutTimer { get { return owner.ShoutTimer; } set { owner.ShoutTimer = value; } }
    public float ShoutCooldown { get { return owner.ShoutCooldown; } }
    protected PhysicsComponent OwnerPhysics { get { return owner.OwnerPhysics; } }
    public GameObject PatrolSprite { get { return owner.PatrolSprite; } }
    public GameObject ChaseSprite { get { return owner.ChaseSprite; }  }
    public GameObject AttackSprite { get { return owner.AttackSprite; }  }
    #endregion

    protected GiantSM owner;
    private RaycastHit capsuleRaycast;


    /// <summary>
    /// Sets values upon initialization
    /// </summary>
    /// <param name="owner"></param>
    public override void Initialize(StateMachine owner)
    {
        this.owner = (GiantSM)owner;
        OwnerPhysics.SetAirResistance(0.95f);
        PatrolSprite.SetActive(false);
        AttackSprite.SetActive(false);
        ChaseSprite.SetActive(false);
        //patrolPointCenter = owner.transform.GetComponentInParent<Transform>().transform.position; //giants kommer ha en parent som är mitten av dess patrolpoint
    }

    /// <summary>
    /// Controlls the time interval and % chance for the game object to activate the ScareCow event.
    /// </summary>
    public override void Update()
    {
        OwnerPhysics.SetVelocity(AIagent.velocity);
        ShoutTimer += Time.deltaTime;
        if (ShoutTimer >= ShoutCooldown && Random.Range(1, 10) > 5)
        {
            ScareCowEventInfo scei = new ScareCowEventInfo
            {
                scarySoundLocation = Position.position
            };
            EventHandeler.Current.FireEvent(EventHandeler.EVENT_TYPE.ScareCow, scei);
            ShoutTimer = 0;
        }
       
    }

    /// <summary>
    /// Selects the closest target and send a linecast to determine if the game object has a clear line of sight on it
    /// </summary>
    /// <returns></returns>
    protected bool CanSeePrey()
    {
        Target = GetClosestTarget();

        if(Target != null)
            return !Physics.Linecast(owner.transform.position, Target.transform.position, owner.VisionMask);
        return false;
    }

    /// <summary>
    /// Goes through the fair game list to pick the closest target
    /// </summary>
    /// <returns></returns>
    public GameObject GetClosestTarget()
    {
        GameObject closestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = Position.position;
        foreach (GameObject potentialTarget in GameComponents.FairGameList)
        {
            Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
            float distanceSqrToTarget = directionToTarget.sqrMagnitude;
            if (distanceSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = distanceSqrToTarget;
                closestTarget = potentialTarget;
            }
        }

        if (closestDistanceSqr < DetectionRange * DetectionRange)
        {
            Debug.Log("Someone is close enough");
            return closestTarget;
        }
        else
        {
            return null;
        }
    }

    /// <summary>
    /// Determines if the destination is as close as or closer than the acceptable range.
    /// </summary>
    /// <param name="destination"> The desired destination of the navmesh agent.</param>
    /// <param name="acceptableRange"> The range which the condition is acceptable.</param>
    /// <returns></returns>
    public bool CheckRemainingDistance(Vector3 destination, float acceptableRange)
    {
        return (destination - Position.position).sqrMagnitude < acceptableRange * acceptableRange;
    }

}
