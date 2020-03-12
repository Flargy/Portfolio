using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Main Author: Marcus Lundqvist

public class RespawnTrigger : MonoBehaviour
{
    /// <summary>
    /// Triggers the respawn function on the object that entered the trigger zone.
    /// </summary>
    /// <param name="other">The collider which entered the trigger zone</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<NewPlayerScript>().Respawn();
        }
        else if (other.CompareTag("CarryBox"))
        {
            other.GetComponent<InteractionPickUp>().Respawn();
        }
    }
}
