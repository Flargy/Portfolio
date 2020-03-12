using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//Main Author: Marcus Lundqvist

//Secondary Author: Hjalmar Andersson

[CreateAssetMenu(menuName = "Cow/CowFollowState")]
public class CowFollowState : CowBaseState
{

    private float isFollowing;

    /// <summary>
    /// Changes movement speed and adds the cow to the fair game list.
    /// </summary>
    public override void Enter()
    {
        Debug.Log("Following");
        Stopped = false;
        AIagent.speed = MovementSpeed;
        isFollowing = 0;
        FollowSprite.SetActive(true);
        GameComponents.FollowingCowsList.Add(owner.gameObject);
        if(GameComponents.FairGameList.Contains(owner.gameObject) == false)
            GameComponents.FairGameList.Add(owner.gameObject);
    }

    /// <summary>
    /// Updates navmesh destination to the player's position and checks distance between game object and player.
    /// Swaps to <see cref="CowMoveState"/> if the distance becomes to great.
    /// </summary>
    public override void Update()
    {
        base.Update();
        isFollowing += Time.deltaTime;
        if(isFollowing > 0.5)
        {
            AIagent.SetDestination(Player.transform.position);
            if (Vector3.Distance(Position.position, Player.transform.position) > 10)
                AIagent.speed = MovementSpeed * 1.6f;
            else
                AIagent.speed = MovementSpeed;
            isFollowing = 0;
        }
       
        if (Vector3.Distance(Position.position, Player.transform.position) > FollowDistance)
        {
            //Debug.Log(Vector3.Distance(Position.position, Player.transform.position) > FollowDistance);
            //Debug.Log(FollowDistance);
            //Debug.Log(Vector3.Distance(Position.position, Player.transform.position));
            owner.TransitionTo<CowMoveState>();
            
        }
       
    }

    /// <summary>
    /// Removes game object from the cow following list.
    /// </summary>
    public override void Exit()
    {
        Called = false;
        FollowSprite.SetActive(false);
        GameComponents.FollowingCowsList.Remove(owner.gameObject);
    }
}
