using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Main Author: Marcus Lundqvist

[CreateAssetMenu(menuName = "Giant/GiantPatrolState")]
public class GiantPatrol : GiantBase
{
    [SerializeField] private float patrolArea = 30f;
    private Vector3 position;
    private int currentTry;

    /// <summary>
    /// Sets a new desination upon entry
    /// </summary>
    public override void Enter()
    {
        PatrolSprite.SetActive(true);
        AIagent.speed = MovementSpeed;
        SetNewDestination();
        
    }

    
    /// <summary>
    /// Sets a destination within a radius of <see cref="patrolArea"/> from the game objects starting position.
    /// </summary>
    private void SetNewDestination()
    {
        currentTry = 0;
        do
        {
            position = new Vector3(Random.Range(-patrolArea, patrolArea), 0, Random.Range(-patrolArea, patrolArea));
            currentTry++;
        } while (position.magnitude < 10 && currentTry < 10);

        position = StartPosition + position;
        AIagent.SetDestination(position);
    }

    /// <summary>
    /// Checks if it has the player within line of sight or if it has reached it's destination and changes state accordingly.
    /// </summary>
    public override void Update()
    {
        if (CanSeePrey())
        {
            owner.TransitionTo<GiantChaseState>();
        }
        
        if (CheckRemainingDistance(AIagent.destination, 2f))
        {
            float randomNumber = Random.Range(0, 2);
            if (randomNumber < 0.5)
                owner.TransitionTo<GiantIdleState>();

            SetNewDestination();


        }
        base.Update();
    }

    
    public override void Exit()
    {
        PatrolSprite.SetActive(false);
    }


}
