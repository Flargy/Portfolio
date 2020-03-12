 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Main Author: Marcus Lundqvist


[CreateAssetMenu(menuName = "Giant/GiantAttackState")]
public class GiantAttackState : GiantBase
{
    [SerializeField] private float cooldown;
    private float timer = 0;

    /// <summary>
    /// Sets values upon entry and activates attack method depending if the target is of type "Cow" or "Player".
    /// </summary>
    public override void Enter()
    {
        timer = 0;
        AIagent.isStopped = true;
        AttackSprite.SetActive(true);
        if (Target.gameObject.CompareTag("Cow"))
            Target.GetComponent<CowSM>().AttackTheCow(Damage);
        else if (Target.gameObject.CompareTag("Player") )
            Target.GetComponent<PlayerStateMashine>().GetAttacked(Damage);


    }
   
    /// <summary>
    /// Counts the timer before the owner can move again after attacking. Swaps state to either <see cref="GiantChaseState"/> or <see cref="GiantDayState"/> 
    /// depending on time of day.
    /// </summary>
    public override void Update()
    {
        timer += Time.deltaTime;
        if (timer >= cooldown)
        {
            if (IsNight)
                owner.TransitionTo<GiantChaseState>();
            else
                owner.TransitionTo<GiantDayState>();
        }
    }

    /// <summary>
    /// Reactivates the movement 
    /// </summary>
    public override void Exit()
    {
        AttackSprite.SetActive(false);
        AIagent.isStopped = false;
    }
}

