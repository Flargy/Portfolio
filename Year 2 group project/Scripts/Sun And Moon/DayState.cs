using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Author: Marcus Lundqvist

[CreateAssetMenu(menuName = "Dynamic/DayState")]
public class DayState : DayAndNightBaseState
{

    
    public override void Exit()
    {
        NightEventInfo nei = new NightEventInfo { };
        EventHandeler.Current.FireEvent(EventHandeler.EVENT_TYPE.SwapToNight, nei);
    }

    /// <summary>
    /// Cecks the current Y value of the Sun object and uses that as a condition to swap state.
    /// </summary>
    public override void Update()
    {
        if (Sun.transform.position.y < NightTime && Sun.transform.position.z > 0)
        {
            Debug.Log("swapping to night state");
            owner.TransitionTo<NightState>();
        }
    }

    
}
