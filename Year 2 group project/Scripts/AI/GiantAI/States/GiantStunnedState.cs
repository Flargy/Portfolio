using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Main Author: Marcus Lundqvist

[CreateAssetMenu(menuName = "Giant/GiantStunnedState")]
public class GiantStunnedState : GiantBase
{
    [SerializeField] private float stunDuration;
    private float currentTimeStunned;

    /// <summary>
    /// Sets values upon enter and freezes movement.
    /// </summary>
    public override void Enter()
    {
        currentTimeStunned = 0;
        AIagent.isStopped = true;

    }

    /// <summary>
    /// Increases the timer by <see cref="Time.deltaTime"/> every update and then transitions state depedning on time of day.
    /// </summary>
    public override void Update()
    {
        currentTimeStunned += Time.deltaTime;

        if (IsNight == true && currentTimeStunned >= stunDuration)
        {
            owner.TransitionTo<GiantPatrol>();
        }
        else if(IsNight == false)
            owner.TransitionTo<GiantDayState>();
        base.Update();
    }

    /// <summary>
    /// Makes the game object move again.
    /// </summary>
    public override void Exit()
    {
        AIagent.isStopped = false;
    }
}
