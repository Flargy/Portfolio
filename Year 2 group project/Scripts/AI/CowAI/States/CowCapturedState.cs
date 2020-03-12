using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Main Author: Marcus Lundqvist

[CreateAssetMenu(menuName = "Cow/CowCapturedState")]
public class CowCapturedState : CowBaseState
{
    private float penArea;

    /// <summary>
    /// Changes movementspeed and sets the animal pen area
    /// </summary>
    public override void Enter()
    {
        AIagent.speed = MovementSpeed * 0.7f;
        penArea = Pen.GetComponent<AnimalPen>().PenArea;
        GameComponents.FairGameList.Remove(owner.gameObject);
        
    }

    /// <summary>
    /// Sets new destination upon reaching current destination.
    /// </summary>
    public override void Update()
    {
        if (AIagent.remainingDistance < 1.5f)
        {
            
            SetDestination();

        }
    }

    
    /// <summary>
    /// Sets a new destination within the area of the animal pen.
    /// </summary>
    private void SetDestination()
    {
        Vector3 destination;
        Vector3 randomLocationInPen = new Vector3(Random.Range(-penArea, penArea), 0, (Random.Range(-penArea, penArea)));

        destination = Pen.transform.position + randomLocationInPen;
        AIagent.SetDestination(destination);

    }

   

}
