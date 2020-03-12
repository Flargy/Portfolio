using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Main Author: Marcus Lundqvist

public class NightAndDayObjectsSM : StateMachine
{

    protected override void Awake()
    {
        base.Awake();
    }

    /// <summary>
    /// Registers event listeners.
    /// </summary>
    public void Start()
    {
        EventHandeler.Current.RegisterListener(EventHandeler.EVENT_TYPE.SwapToNight, SwapToNight);
        EventHandeler.Current.RegisterListener(EventHandeler.EVENT_TYPE.SwapToDay, SwapToDay);

    }

    /// <summary>
    /// Reacts to <see cref="DayEventInfo"/> to transition state to <see cref="NightAndDayDayState"/>
    /// </summary>
    /// <param name="info"></param>
    public void SwapToDay(EventInfo info)
    {
        TransitionTo<NightAndDayDayState>();
    }

    /// <summary>
    /// Reacts to <see cref="NightEventInfo"/> to transition state to <see cref="NightAndDayNightState"/>
    /// </summary>
    /// <param name="info"></param>
    public void SwapToNight(EventInfo info)
    {
        TransitionTo<NightAndDayNightState>();
    }
   
}
