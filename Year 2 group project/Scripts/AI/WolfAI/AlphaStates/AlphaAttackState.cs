using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

//Author: Hjalmar Andersson

[CreateAssetMenu(menuName = "Alpha/AlphaAttackState")]
public class AlphaAttackState : AlphaBaseState
{
    /// <summary>
    /// Assigns values upon entry.
    /// </summary>
    public override void Enter()
    {
        AttackSprite.SetActive(true);
        AIagent.velocity = Vector3.zero;
        Debug.Log(owner.name + " entered AttackState");
        AIagent.speed = MoveSpeed * 2.2f;
    }

    /// <summary>
    /// Checks if the Player has a torch out, if it has lost it's target and if it is close enough to attack.
    /// Then swaps state depending on what condition is true
    /// </summary>
    public override void Update()
    {

        AIagent.SetDestination(Prey.transform.position);

        if (PlayerHasFire)
        {
            owner.TransitionTo<AlphaFleeState>();
        }
        else if (!PreyLocated) { 
            owner.TransitionTo<AlphaPatrolState>();
        }
        else if(CheckRemainingDistance(Prey.transform.position, 3f))
        {
            if(Prey.gameObject.tag == "Cow")
            {
                Prey.GetComponent<CowSM>().AttackTheCow((int)Damage);
            }
            else if(Prey.gameObject.tag == "Player")
            {
                Prey.GetComponent<PlayerStateMashine>().GetAttacked((int)Damage);
                DamageEventInfo dei = new DamageEventInfo { damage = Damage };
                EventHandeler.Current.FireEvent(EventHandeler.EVENT_TYPE.Damage, dei);
                Debug.Log("Damage is done by wolf, dmg " + Damage);
            }
            if(Prey == null)
            {
                PreyLocated = false;
            }
            owner.TransitionTo<AlphaObserveState>();
        }
        base.Update();

        //check it out
        /*
        NavMeshPath path =null;
        NavMesh.CalculatePath(Vector3.zero, Vector3.up, NavMesh.AllAreas, path);
        path.corners.ToList().ForEach(p => Position);
        */

        setVelocity();
    }

    /// <summary>
    /// Disables the sprite upon exit.
    /// </summary>
    public override void Exit()
    {
        AttackSprite.SetActive(false);
    }
}
