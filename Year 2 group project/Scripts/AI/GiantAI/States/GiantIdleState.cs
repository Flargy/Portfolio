using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Main Author: Marcus Lundqvist

[CreateAssetMenu(menuName = "Giant/GiantIdleState")]
public class GiantIdleState : GiantBase
{
    public float waitTime;
    private float timer = 0;

    /// <summary>
    /// Sets a random wait time between 1-4 seconds.
    /// </summary>
    public override void Enter()
    {
        waitTime = Random.Range(1, 4);
    }

    /// <summary>
    /// Increases <see cref="timer"/> by <see cref="Time.deltaTime"/> each frame until it reaches <see cref="waitTime"/> and then switches state accordingly.
    /// </summary>
    public override void Update()
    {
        timer += Time.deltaTime;
        base.Update();

        if (CanSeePrey())
        {
            owner.TransitionTo<GiantChaseState>();
        }

        else if(timer >= waitTime)
        {
            owner.TransitionTo<GiantPatrol>();
        }

    }


   
}
