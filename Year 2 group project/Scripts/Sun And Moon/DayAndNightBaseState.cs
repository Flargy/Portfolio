using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Author: Marcus Lundqvist

public class DayAndNightBaseState : State
{

    protected float NightTime { get { return owner.NightTime; } }
    protected GameObject Sun { get { return owner.Sun; } }
    protected GameObject GameComponent { get { return owner.GameComponent; } }
    protected GameObject Moon { get { return owner.Moon; } }

    protected DayAndNightSM owner;

    public override void Initialize(StateMachine owner)
    {
        this.owner = (DayAndNightSM)owner;
    }

    public override void Update()
    {
        
    }
}
