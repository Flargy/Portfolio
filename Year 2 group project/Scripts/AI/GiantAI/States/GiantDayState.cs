using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Main Author: Marcus Lundqvist

[CreateAssetMenu(menuName = "Giant/GiantDayState")]
public class GiantDayState : GiantBase
{
    /// <summary>
    /// Stops the navmesh agent.
    /// </summary>
    public override void Enter()
    {
        owner.GetComponent<Renderer>().material = DayMaterial;
        AIagent.SetDestination(owner.transform.position);
        AIagent.isStopped = true;
    }
    /// <summary>
    /// Reactivates the navmesh agent.
    /// </summary>
    public override void Exit()
    {
        owner.GetComponent<Renderer>().material = NightMaterial;
        AIagent.isStopped = false;

    }
}
