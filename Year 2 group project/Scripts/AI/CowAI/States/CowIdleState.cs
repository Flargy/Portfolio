using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Main Author: Marcus Lundqvist

[CreateAssetMenu(menuName = "Cow/CowIdleState")]
public class CowIdleState : CowBaseState
{
    private float waitTime;
    private float timer = 0;

    /// <summary>
    /// Sets values upon enter.
    /// </summary>
    public override void Enter()
    {
        waitTime = Random.Range(4, 8);
    }

    /// <summary>
    /// Counts the idle timer
    /// </summary>
    public override void Update()
    {
        timer += Time.deltaTime;
        base.Update();
        
        if(timer >= waitTime)
        {
            owner.TransitionTo<CowMoveState>();
        }


    }

    /// <summary>
    /// Resets the idle timer
    /// </summary>
    public override void Exit()
    {
        timer = 0;
    }

}
