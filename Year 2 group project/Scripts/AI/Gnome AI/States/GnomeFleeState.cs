using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Author: Hjalmar Andersson

[CreateAssetMenu(menuName = "Gnome/GnomeFleeState")]
public class GnomeFleeState : GnomeBaseState
{
   
    private bool despawned;

    /// <summary>
    /// Sets values upon entry.
    /// </summary>
    public override void Enter()
    {
        AIagent.speed = Speed * 1.5f;
        FleeToGnomergan();
        AIagent.SetDestination(Position.position + Gnomergan);
        despawned = false;
    }

    /// <summary>
    /// Destroys the game object once it reaches it's destination.
    /// </summary>
    public override void Update()
    {
        if (AIagent.remainingDistance < 1f && !despawned)
        {
            despawned = true;
            //Target.GetComponent<CowSM>().SetGnomes(-1);
            //GameComponetns.gnomeList.Remove(owner.gameObject);
            Destroy(owner.gameObject, 1f);

        }
    }
}
