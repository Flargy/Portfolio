using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Main Author: Marcus Lundqvist

public class GiantSM : StateMachine
{
    #region Get and Set properties
    [HideInInspector] public MeshRenderer Renderer { get; set; }
    [HideInInspector] public UnityEngine.AI.NavMeshAgent Agent { get; set; }
    public PhysicsComponent OwnerPhysics { get; set; }
    public bool IsNight { get; set; }
    public GameObject Target { get; set; }
    public float ShoutTimer { get; set; }
    public Vector3 StartPosition { get; set; }
    public Vector3 SearchPosition { get; set; }
    #endregion

    #region Properties returning private variables
    public Material DayMaterial { get { return dayMaterial; } }
    public GameObject GameComponent { get { return gameComponent; } }
    public int Damage { get { return damage; } }
    public LayerMask VisionMask { get { return visionMask; } }
    public LayerMask CollisonMask { get { return collisonMask; } }
    public Material NightMaterial { get { return nightMaterial; } }
    public float GiantHearingRange { get { return giantHearingRange; } }
    public float DetectionRange { get { return detectionRange; } }
    public float ShoutCooldown { get { return shoutCooldown; } }
    public float MovementSpeed { get { return movementSpeed; } }
    public float FollowRange { get { return followRange; } }
    public GameObject PatrolSprite { get { return patrolSprite; } }
    public GameObject ChaseSprite { get { return chaseSprite; } }
    public GameObject AttackSprite { get { return attackSprite; } }
    #endregion

    [SerializeField] private Material dayMaterial;
    [SerializeField] private GameObject gameComponent;
    [SerializeField] private int damage;
    [SerializeField] private LayerMask visionMask;
    [SerializeField] private LayerMask collisonMask;
    [SerializeField] private Material nightMaterial;
    [SerializeField] private float giantHearingRange;
    [SerializeField] private float detectionRange;
    [SerializeField] private float shoutCooldown;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float followRange;
    [SerializeField] private GameObject patrolSprite;
    [SerializeField] private GameObject chaseSprite;
    [SerializeField] private GameObject attackSprite;

    /// <summary>
    /// Assigns values upon awake
    /// </summary>
    protected override void Awake()
    {

        OwnerPhysics = GetComponent<PhysicsComponent>();
        Agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        base.Awake();
        StartPosition = transform.position;

    }

    /// <summary>
    /// Registers listeners for events.
    /// </summary>
    public void Start()
    {
        GameComponents.GiantList.Add(gameObject);
        EventHandeler.Current.RegisterListener(EventHandeler.EVENT_TYPE.SwapToNight ,SwapToNight);
        EventHandeler.Current.RegisterListener(EventHandeler.EVENT_TYPE.SwapToDay, SwapToDay);
        EventHandeler.Current.RegisterListener(EventHandeler.EVENT_TYPE.Song, SearchForPlayer);
        EventHandeler.Current.RegisterListener(EventHandeler.EVENT_TYPE.StunGiant, StunGiant);
        EventHandeler.Current.RegisterListener(EventHandeler.EVENT_TYPE.Save, SaveGiant);
        EventHandeler.Current.RegisterListener(EventHandeler.EVENT_TYPE.Load, LoadGiant);

    }

    public void SaveGiant(EventInfo eventInfo)
    {
        GameDataController.SetGiantState(this);
    }

    public void LoadGiant(EventInfo eventInfo)
    {
        GiantsData myInfo = GameDataController.GetGiantState(this);
        float[] pos = myInfo.Position;
        Vector3 loadPos;
        loadPos.x = pos[0];
        loadPos.y = pos[1];
        loadPos.z = pos[2];
        transform.position = loadPos;
        //State newState = myInfo.CurrentState;

        //TransitionTo(newState);
    }

    /// <summary>
    /// Transitions the giant into <see cref="GiantDayState"/>
    /// </summary>
    /// <param name="dayEventInfo"> Contains info from the <see cref="EventInfo"/></param>
    public void SwapToDay(EventInfo dayEventInfo)
    {
        IsNight = false;
        TransitionTo<GiantDayState>();
    }

    /// <summary>
    /// Transitions the giant into <see cref="GiantPatrol"/>
    /// </summary>
    /// <param name="nightInfo"> Contains info from the <see cref="EventInfo"/></param>
    public void SwapToNight(EventInfo nightInfo)
    {
        IsNight = true;
        TransitionTo<GiantPatrol>();
    }

    /// <summary>
    /// Transitions the giant to <see cref="GiantStunnedState"/> if it is within range of the player
    /// </summary>
    /// <param name="eventInfo"> Contains info from the <see cref="EventInfo"/></param>
    public void StunGiant(EventInfo eventInfo)
    {
        StunGiantEventInfo se = (StunGiantEventInfo)eventInfo;
        if(Vector3.Distance(se.playerPosition, transform.position) < se.stunDistance)
            TransitionTo<GiantStunnedState>();
    }

    /// <summary>
    /// Reacts to player noise and goes to search the players position if it is within hearing range.
    /// </summary>
    /// <param name="eventInfo"> Contains info from the <see cref="EventInfo"/></param>
    public void SearchForPlayer(EventInfo eventInfo)
    {
        ShoutEventInfo se = (ShoutEventInfo)eventInfo;

        if (IsNight && Vector3.Distance(transform.position, se.playerPosition) < giantHearingRange && Target != null)
        {
            SearchPosition = se.playerPosition;
            TransitionTo<GiantSearchState>();
        }
    }
}
