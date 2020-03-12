using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Main Author: Marcus Lundqvist

[CreateAssetMenu(menuName = "DynamicObjects/DayState")]
public class NightAndDayDayState : NightAndDayBaseState
{

    public override void Enter()
    {
        owner.gameObject.SetActive(true);
    }

    

    
}
