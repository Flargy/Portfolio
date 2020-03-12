using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Main Author: Marcus Lundqvist

//Secondary Author: Hjalmar Andersson

public class CowSM : StateMachine
{
    #region Standard Get & Set properties
    [HideInInspector] public MeshRenderer Renderer { get; set; }
    [HideInInspector] public UnityEngine.AI.NavMeshAgent Agent { get; set; }
    public GameObject Pen { get; set; }
    public GameObject Player { get; set; }
    public bool Called { get; set; }
    public bool Attacked { get; set; }
    public bool Stopped { get; set; }
    public bool Captured { get; set; }
    public PhysicsComponent OwnerPhysics { get; set; }
    public Vector3 GoToLocation { get; set; }
    public GameObject GameComponent { get; set; }
    #endregion

    #region Properties returning values from inspector

    public LayerMask CollisonMask { get { return collisonMask; } }
    public GameObject FollowSprite { get { return followSprite; } }
    public GameObject PatrolSprite { get { return patrolSprite; } }
    public float HearingRange { get { return hearingRange; } }
    public float CallingRange { get { return hearingRange; } }
    public float ScareRange { get { return scareRange; } }
    public float FollowDistance { get { return followDistance; } }
    public float MovementSpeed { get { return movementSpeed; } }
    public int Health { get { return health; } set { health = value; } }
    public LayerMask VisionMask { get { return visionMask; } }
    public AudioSource AudioSource { get { return audioSource; } }
    public AudioClip MooNoise { get { return mooNoise; } }
    #endregion

    private string Name;
    private Vector3 CurrentPosition;
    private Vector3 DirectionToTarget;
    private float DistanceSqrToTarget;


    [SerializeField] private LayerMask collisonMask;
    [SerializeField] private GameObject followSprite;
    [SerializeField] private GameObject patrolSprite;
    [SerializeField] private float hearingRange = 20;
    [SerializeField] private float callingRange = 50;
    [SerializeField] private float scareRange = 40;
    [SerializeField] private float followDistance;
    [SerializeField] private float movementSpeed;
    [SerializeField] private int health;
    [SerializeField] private LayerMask visionMask;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip mooNoise;

    /// <summary>
    /// Sets values on Awake
    /// </summary>
    protected override void Awake()
    {
        OwnerPhysics = GetComponent<PhysicsComponent>();
        Agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        base.Awake();
        Attacked = false;
        Called   = false;
        Stopped  = false;
        Captured = false;
    }

    /// <summary>
    /// Adds <see cref="GameObject"/> to <see cref="GameComponents.CowList"/> and registers listeners
    /// </summary>
    public void Start()
    {
        Player = GameObject.Find("PlayerAlpha");
        GameComponents.CowList.Add(gameObject);
        EventHandeler.Current.RegisterListener(EventHandeler.EVENT_TYPE.CallCow, CallTheCow);
        EventHandeler.Current.RegisterListener(EventHandeler.EVENT_TYPE.StopCow, StopTheCow);
        EventHandeler.Current.RegisterListener(EventHandeler.EVENT_TYPE.SwedAwayCow, SendAwayCow);
        EventHandeler.Current.RegisterListener(EventHandeler.EVENT_TYPE.CalmCow, CalmTheCow);
        EventHandeler.Current.RegisterListener(EventHandeler.EVENT_TYPE.LocateCow, LocateCow);
        EventHandeler.Current.RegisterListener(EventHandeler.EVENT_TYPE.PetCow, PetTheCow);
        EventHandeler.Current.RegisterListener(EventHandeler.EVENT_TYPE.CollectCow, TurnInTheCow);
        EventHandeler.Current.RegisterListener(EventHandeler.EVENT_TYPE.ScareCow, ScareTheCow);
        EventHandeler.Current.RegisterListener(EventHandeler.EVENT_TYPE.Save, SaveCow);
        EventHandeler.Current.RegisterListener(EventHandeler.EVENT_TYPE.Load, LoadCow);
        audioSource = GetComponent<AudioSource>();

        int rand = Random.Range(0, GameComponents.CowNames.Count);

        Name = GameComponents.CowNames[rand];
    }

    /// <summary>
    /// Makes the cow follow the player if it is within <see cref="hearingRange"/>.
    /// </summary>
    /// <param name="eventInfo">contains infomation brought from <see cref="EventInfo"/></param> 
    public void CallTheCow(EventInfo eventInfo)
    {
        //if(CurrentState.GetType() == typeof(CowDyingState))

        CallCowEventInfo callCowInfo = (CallCowEventInfo)eventInfo;
        if (CurrentState.GetType() != typeof(CowCapturedState) && CurrentState.GetType() != typeof(CowFollowState) && Vector3.Distance(transform.position, callCowInfo.playerPosition) < hearingRange) // cowMove, cowidle
        {
            Called = true;
            Player = callCowInfo.player;
            TransitionTo<CowFollowState>();
        }
        
    }

    /// <summary>
    /// Makes all the cows following the player stop.
    /// </summary>
    /// <param name="eventInfo"></param>
    public void StopTheCow(EventInfo eventInfo)
    {
        if(CurrentState.GetType() == typeof(CowFollowState))
        {
            Called = false;
            Stopped = true;
        }
    }

    /// <summary>
    /// A method that enemies activate to scare and deal damage to the cow.
    /// </summary>
    /// <param name="damage"></param>
    public void AttackTheCow(int damage)
    {
        Attacked = true;
        health -= damage;
        TransitionTo<CowFleeState>();
    }

    /// <summary>
    /// Turn following cows in to the animal pen if it is within distance
    /// </summary>
    /// <param name="eventInfo">contains infomation brought from <see cref="EventInfo"/></param> 
    public void TurnInTheCow(EventInfo eventInfo)
    {
        if (CurrentState.GetType() == typeof(CowFollowState))
        { 
            CollectCowEventInfo ccei = (CollectCowEventInfo)eventInfo;
            this.Pen = ccei.closestPen;
            TransitionTo<CowCapturedState>();
        }
    }

    /// <summary>
    /// Makes the cow run faster towards a point in front of the player but makes them stop following the player
    /// </summary>
    /// <param name="eventInfo"> contains infomation brought from <see cref="EventInfo"/></param> 
    public void SendAwayCow(EventInfo eventInfo)
    {
        if (CurrentState.GetType() == typeof(CowMoveState))
        {
            SendAwayCowEvent sendAwayCowInfo = (SendAwayCowEvent)eventInfo;
            GoToLocation = sendAwayCowInfo.destination;
            Stopped = true;
            TransitionTo<CowRunAheadState>();
        }
    }

    /// <summary>
    /// Makes cows return to their normal move state.
    /// </summary>
    /// <param name="eventInfo"> contains infomation brought from <see cref="EventInfo"/></param>
    public void CalmTheCow(EventInfo eventInfo)
    {
        if (CurrentState.GetType() == typeof(CowFleeState))
        {
            CalmCowEvent calmCowEventInfo = (CalmCowEvent)eventInfo;
            if (Vector3.Distance(calmCowEventInfo.playerPosition, transform.position) < hearingRange)
            {
                TransitionTo<CowMoveState>();
            }
        }
    }

    /// <summary>
    /// Makes cows emit a sound if they are within range of the player.
    /// </summary>
    /// <param name="eventInfo"> contains infomation brought from <see cref="EventInfo"/></param>
    public void LocateCow(EventInfo eventInfo)
    {
        LocateCowEventInfo locateCowInfo = (LocateCowEventInfo)eventInfo;
        float distance = Vector3.Distance(locateCowInfo.playerPosition, transform.position);
        if (distance < callingRange*2 && distance > callingRange/2f)
        {
            if (!audioSource.isPlaying) { 
                //audioSource.volume = distance ;
                audioSource.pitch = Random.Range(0.8f, 1.5f);
                audioSource.loop = false;
                audioSource.clip = mooNoise;
                audioSource.Play();
            }
            //make sound
        }
    }
    /// <summary>
    /// Allows the player to interact with the cow to bring up the cows name
    /// </summary>
    /// <param name="eventInfo"> contains infomation brought from <see cref="EventInfo"/></param>
    public void PetTheCow(EventInfo eventInfo)
    {
        PetCowEventInfo petCowEventInfo = (PetCowEventInfo)eventInfo;

        if (Vector3.Distance(petCowEventInfo.playerPosition, transform.position) < 3f && CurrentState.GetType() != typeof(CowFleeState))
        {
            if (CurrentState.GetType() != typeof(CowCapturedState) && CurrentState.GetType() != typeof(CowFollowState)) {
                Debug.Log(CurrentState.GetType());
                Called = true;
                TransitionTo<CowFollowState>();
            }
            Debug.Log("The Player pets " + Name + ". She seems to enyoj it");
        }
    }

    /// <summary>
    /// Makes the cow run in the opposite direction from the object that scared it by activating the <see cref="AttackTheCow(int)"/> using 0 damage.
    /// </summary>
    /// <param name="eventInfo"> contains infomation brought from <see cref="EventInfo"/></param>
    public void ScareTheCow(EventInfo eventInfo)
    {

        if (CurrentState.GetType() != typeof(CowFleeState))
        {
            ScareCowEventInfo scei = (ScareCowEventInfo)eventInfo;

            DirectionToTarget = scei.scarySoundLocation - transform.position;
            DistanceSqrToTarget = DirectionToTarget.sqrMagnitude;
            if (DistanceSqrToTarget < scareRange)
            {
                AttackTheCow(0);
            }
        }
    }

    public void SaveCow(EventInfo eventInfo)
    {
        
        Debug.Log("State from cow: " + CurrentState.Index);
        GameDataController.SetCowState(this);
    }

    public void LoadCow(EventInfo eventInfo)
    {
        CowsData myInfo = GameDataController.GetCowState(this);
        float [] pos = myInfo.Position;
        Vector3 loadPos;
        loadPos.x = pos[0];
        loadPos.y = pos[1];
        loadPos.z = pos[2];
        transform.position = loadPos;
        int stateIndex = myInfo.StateIndex;

        TransitionTo(stateIndex);
    }
}
