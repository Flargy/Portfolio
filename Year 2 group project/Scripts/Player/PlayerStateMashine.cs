using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Main Author: Debatable

public class PlayerStateMashine : StateMachine
{
    #region Get and Set properties
    public PhysicsComponent OwnerPhysics { get; set; }
    public Vector3 Direction { get; set; }
    public Vector3 LookDirection { get; set; }
    public Vector3 FaceDirection { get; set; }
    public List<string> Inventory { get; set; }
    public Rune[] Runes { get; set; }
    public int RuneNumber { get; set; }
    public Rune CurrentRune { get; set; }
    public float VerticalDirection { get; set; }
    public float HorizontalDirection { get; set; }
    public float AnimationMovementDelay { get; set; }
    public int ClipIndex { get; set; }
    public float CurrentTorchLifetime { get; set; }
    public float JumpTimer { get; set; }
    public bool Jumped { get; set; }
    public bool XboxInputDownNotOnCooldown { get; set; }
    public bool XboxInputLeftTriggerNotOnCooldown { get; set; }

    #endregion

    #region Properties returning existing variables
    public LayerMask CollisionMask { get { return collisionMask; } }
    public AudioClip[] CallCowSounds { get { return callCowSounds; } }
    public AudioClip[] StopCowSounds { get { return stopCowSounds; } }
    public AudioClip StunGiantSound { get { return stunGiantSound; } }
    public GameObject Bonfire { get { return bonfire; } }
    public Animator Anim { get { return anim; } }
    public AudioSource Source { get { return source; } }
    public float StunRange { get { return stunRange; } set { stunRange = value; } }
    public int NrOfTorches { get { return nrOfTorches; } set { nrOfTorches = value; } }
    public int Health { get { return health; } set { health = value; } }
    public float StunGiantTimer { get { return stunGiantTimer; } set { stunGiantTimer = value; } }
    public float TorchTimer { get { return torchTimer; } set { torchTimer = value; } }
    public Vector3 Velocity { get { return OwnerPhysics.Velocity; } set {OwnerPhysics.Velocity = value; } } // move this to somewhere?? - vibben
    #endregion


    [SerializeField] private LayerMask collisionMask;
    [SerializeField] private float stunRange;
    [SerializeField] private int nrOfTorches;
    [SerializeField] private int health;
    [SerializeField] private GameObject bonfire;
    [SerializeField] private float stunGiantTimer;
    [SerializeField] private Animator anim;
    [SerializeField] private float torchTimer;
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip[] callCowSounds;
    [SerializeField] private AudioClip[] stopCowSounds;
    [SerializeField] private AudioClip stunGiantSound;


    protected override void Awake()
    {
        OwnerPhysics = GetComponent<PhysicsComponent>();
        anim = GetComponent<Animator>();
        Inventory = new List<string>();
        Runes = new Rune[3];
        RuneNumber = 0;
        Jumped = false;
        LookDirection = transform.position;
        FaceDirection = transform.position;
        AnimationMovementDelay = 0;
        XboxInputDownNotOnCooldown = true;
        XboxInputLeftTriggerNotOnCooldown = true;
        base.Awake();
       
    }

  

    /// <summary>
    /// Adds the received item to the inventory list.
    /// </summary>
    /// <param name="item"> The name of the object</param>
    /// <param name="GO"> A reference to the object</param>
    public void AddItem(string item, GameObject GO)
    {
        Inventory.Add(item);
        Destroy(GO);
    }

    /// <summary>
    /// Removes the item from the inventory list.
    /// </summary>
    /// <param name="item"> Removes the item from the inventory</param>
    public void RemoveItem(string item)
    {
        Inventory.Remove(item);
    }

    //public void SavePlayer()
    //{
    //    SaveSystem.SaveData(this);
    //}

    //public void SavePlayer()
    //{
    //    SaveSystem.SaveData(this);
    //}

    //public void LoadPlayer()
    //{
    //    Data data = SaveSystem.LoadData();

    //    health = data.Health;

    //    Vector3 position;

    //    position.x = data.Position[0];
    //    position.y = data.Position[1];
    //    position.z = data.Position[2];
    //    transform.position = position;
    //}

    public void SavePlayer(EventInfo eventInfo)
    {
        GameDataController.SetPlayerState(this);
    }

    public void LoadPlayer(EventInfo eventInfo)
    {
        SaveData2 myInfo = GameDataController.GetPlayerState(this);
        float[] pos = myInfo.Position;
        Vector3 loadPos;
        loadPos.x = pos[0];
        loadPos.y = pos[1];
        loadPos.z = pos[2];
        transform.position = loadPos;
        Velocity = Vector3.zero;

    }

    public void Start()
    {
        GameComponents.FairGameList.Add(gameObject); // dynamically adds the player to the list once it awakes
        EventHandeler.Current.RegisterListener(EventHandeler.EVENT_TYPE.TorchPickup, PickUpTorch);
        EventHandeler.Current.RegisterListener(EventHandeler.EVENT_TYPE.QuestReward, AddRune);
        EventHandeler.Current.RegisterListener(EventHandeler.EVENT_TYPE.Save, SavePlayer);
        EventHandeler.Current.RegisterListener(EventHandeler.EVENT_TYPE.Load, LoadPlayer);

        nrOfTorches = 0;
        source = GetComponent<AudioSource>();

    }

    /// <summary>
    /// Increases the value of <see cref="nrOfTorches"/> by one when activated.
    /// </summary>
    /// <param name="eventInfo"> Contains information about the event from <see cref="EventInfo"/></param>
    public void PickUpTorch(EventInfo eventInfo)
    {
        nrOfTorches++;
    }

    /// <summary>
    /// Deals damage to the player.
    /// </summary>
    /// <param name="dmg"> The damage value that will be dealt</param>
    public void GetAttacked(int dmg)
    {
        health -= dmg;
        DamageEventInfo dei = new DamageEventInfo { damage = dmg };
        EventHandeler.Current.FireEvent(EventHandeler.EVENT_TYPE.Damage, dei);
    }

    /// <summary>
    /// Adds a rune to the Rune Array.
    /// </summary>
    /// <param name="rune"> Contains information about the runes values</param>
    public void AddRune(EventInfo eventInfo)
    {
        Rune rune = null;
        RewardQuestInfo rqei = (RewardQuestInfo)eventInfo;
        if (rqei.rewardNumber == 1)
        {
            rune = new Rune(1, "Thunder", 50);
        }
        else if (rqei.rewardNumber == 2) { 
            rune = new Rune(2, "Calm", 45);
        }
        else if (rqei.rewardNumber == 3) { 
            rune = new Rune(3, "Locate", 20);
        }
        if (rune == null)
            return;

        rune.Index = RuneNumber;
        if (RuneNumber == 0) {
            CurrentRune = rune;
            ChangeRuneEventInfo crei = new ChangeRuneEventInfo { newRune = rune };
            EventHandeler.Current.FireEvent(EventHandeler.EVENT_TYPE.ChangeRune, crei);
        }
        Runes[RuneNumber] = rune;
        RuneNumber++;
    }

    public IEnumerator XboxCooldown()
    {
        while (Input.GetAxisRaw("Change rune axis") != 0 || Input.GetAxisRaw("Rune activation axis") != 0)
        {
            yield return new WaitForSeconds(Time.deltaTime);

        }
        XboxInputDownNotOnCooldown = true;
        XboxInputLeftTriggerNotOnCooldown = true;

    }

}

//Main Author: Hjalmar Andersson
//Secondary Author: Marcus Lundqvist
public class Rune
{
    private int value;
    public string runeName;
    private float coolDown = 0;
    private float CDTimer = 0;
    private bool used = false;
    private int index = 0;
    public int Index { get { return index; } set { index = value; } }

    public Rune(int number, string name, float CD)
    {
        value = number;
        runeName = name;
        coolDown = CD;
    }

    public bool ReadyToUse()
    {
        if (!used)
            return true;
        return false;
    }

    public void Used()
    {
        used = true;
        Debug.Log(runeName + " rune was used, CD starts, down from " + coolDown + " seconds");
    }

    public float GetCooldown()
    {
        return coolDown;
    }

    public void CooldownFinish()
    {
        used = false;
    }

    public float GetCurrentCD()
    {
        return CDTimer;
    }

    public int GetRuneValue()
    {
        return value;
    }

    public string GetRuneName()
    {
        return runeName;
    }
}
