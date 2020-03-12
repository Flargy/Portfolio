using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Author: Hjalmar Andersson

[CreateAssetMenu(menuName ="Gnome/GnomeAnnoyState")]
public class GnomeAnnoyState : GnomeBaseState
{
    private float timer = 0;
    [SerializeField] public float cooldown;
    
    /// <summary>
    /// Adds <see cref="Time.deltaTime"/> to timer in order to create a cooldown on how often the game object can attack.
    /// </summary>
    public override void Update()
    {
        timer += Time.deltaTime;
        if (CheckRemainingDistance(Target.transform.position, 3f))
        {
            if(timer >= cooldown)
            {
                Target.GetComponent<CowSM>().AttackTheCow(0);
                timer = 0f;
            }
            
        }
            AIagent.SetDestination(Target.transform.position);
        
    }

    
}
