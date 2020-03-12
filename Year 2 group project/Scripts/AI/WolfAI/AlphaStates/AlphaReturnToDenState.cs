using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Author: Hjalmar Andersson


[CreateAssetMenu(menuName = "Alpha/AlphaReturnToDenState")]
public class AlphaReturnToDenState : AlphaBaseState
{
    bool isInDen;
    // Start is called before the first frame update
    public override void Enter()
    {
        isInDen = false;
        AIagent.speed = MoveSpeed * 1.5f;
        AIagent.SetDestination(DenLocation);
    }

    public override void Update()
    {
       if( AIagent.remainingDistance<2f && !isInDen)
        {
            isInDen = true;

        }
            
    }

    public override void Exit()
    {

    }
}
