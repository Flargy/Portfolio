using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Main Author: Marcus Lundqvist

[CreateAssetMenu(menuName = "DynamicObjects/NightState")]
public class NightAndDayNightState : NightAndDayBaseState
{

    public override void Enter()
    {
        owner.gameObject.SetActive(false);
    }

    
}
