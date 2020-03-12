using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Main Author: Marcus Lundqvist

public class DisplayInteraction : MonoBehaviour
{
    [SerializeField] private int otherPlayer = 0;

    /// <summary>
    /// Displays interaction icon on object that entered trigger
    /// </summary>
    /// <param name="other">The oject that entered the trigger zone</param>
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 9 || other.gameObject.layer == otherPlayer) { 
        other.GetComponent<Interactable>().ShowInteraction();
        }
    }

    /// <summary>
    /// Disables the interation icon of objects that exits the trigger
    /// </summary>
    /// <param name="other">The oject that entered the trigger zone</param>
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 9 || other.gameObject.layer == otherPlayer)
        { 

            other.GetComponent<Interactable>().HideInteraction();
        }
    }
}
