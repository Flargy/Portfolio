using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Main Author: Marcus Lundqvist

[CreateAssetMenu(menuName = "Cow/CowDyingState")]
public class CowDyingState : CowBaseState
{
    /// <summary>
    /// Removes the cow from various lists and then plays a small animation before removal.
    /// </summary>
    public override void Enter()
    {
        AIagent.SetDestination(owner.gameObject.transform.position);
        if (GameComponents.FollowingCowsList.Contains(owner.gameObject))
            GameComponents.FollowingCowsList.Remove(owner.gameObject);
        GameComponents.FairGameList.Remove(owner.gameObject);
        Position.rotation = Quaternion.Euler(0, 0, 90);
        Destroy(owner.gameObject, 3f);
    }
}
