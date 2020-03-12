using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Author: Marcus Lundqvist

[CreateAssetMenu(menuName ="Dynamic/NightState")]
public class NightState : DayAndNightBaseState
{
    public override void Exit()
    {
        DayEventInfo dei = new DayEventInfo { };

        EventHandeler.Current.FireEvent(EventHandeler.EVENT_TYPE.SwapToDay, dei);
    }

    /// <summary>
    /// Cecks the current Y value of the Sun object and uses that as a condition to swap state.
    /// </summary>
    public override void Update()
    {
        if (Moon.transform.position.y <= NightTime && Moon.transform.position.z > 0)
        {
            owner.TransitionTo<DayState>();
        }
    }
}
