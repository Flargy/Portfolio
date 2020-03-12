using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Main Author: Marcus Lundqvist


public class NightAndDayBaseState : State
{

    protected NightAndDayObjectsSM owner;

    public override void Initialize(StateMachine owner)
    {
        this.owner = (NightAndDayObjectsSM)owner;
    }



}
