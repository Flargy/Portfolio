using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Main Author: Marcus Lundqvist

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private GameObject holdingPlayer = null;

    private bool followPlayer = true;

    /// <summary>
    /// Follows the position of <see cref="holdingPlayer"/> if <see cref="followPlayer"/> is true.
    /// </summary>
    void Update()
    {
        if(followPlayer == true)
        {
            transform.position = holdingPlayer.transform.position;
        }
        else
        {
            transform.position = new Vector3(holdingPlayer.transform.position.x, transform.position.y, holdingPlayer.transform.position.z);
        }
    }

    /// <summary>
    /// Changes the value of <see cref="followPlayer"/> to false.
    /// </summary>
    public void StopFollowingPlayer()
    {
        followPlayer = false;
    }

    /// <summary>
    /// Changes the value of <see cref="followPlayer"/> to true.
    /// </summary>
    public void StartFollowingPlayer()
    {
        followPlayer = true;
    }
}
