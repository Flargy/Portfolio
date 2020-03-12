using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Author: Marcus Lundqvist

public class StickTrigger : MonoBehaviour
{

    /// <summary>
    /// Adds a torch to the player upon entering the trigger zone.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TorchPickup tp = new TorchPickup { };
            EventHandeler.Current.FireEvent(EventHandeler.EVENT_TYPE.TorchPickup, tp);
            Destroy(gameObject);
        }
            
    }
}
