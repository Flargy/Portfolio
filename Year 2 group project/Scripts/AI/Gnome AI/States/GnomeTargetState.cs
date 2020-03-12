using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Author: Hjalmar Andersson

[CreateAssetMenu(menuName = "Gnome/GnomeTargetState")]
public class GnomeTargetState : GnomeBaseState
{

    [SerializeField] private float speed;
    /// <summary>
    /// Sets the navmesh agents speed upon entry.
    /// </summary>
    public override void Enter()
    {
        AIagent.speed = speed;
    }
    
    /// <summary>
    /// Sets the navmesh agents destination to the targets position and swaps to <see cref="GnomeAnnoyState"/> when within 1 units distance of the target.
    /// </summary>
    public override void Update()
    {
        if (Target != null)
            AIagent.SetDestination(Target.transform.position);//.x, 0, Target.transform.position.z));

        if (AIagent.remainingDistance < 1f)
        {
            owner.TransitionTo<GnomeAnnoyState>();
        }
        
    }
}
