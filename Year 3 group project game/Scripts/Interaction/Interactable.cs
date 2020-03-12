using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Main Author: Marcus Lundqvist

public class Interactable : MonoBehaviour
{
    public float interactionRadius = 2.0f; // Position for player to stand when interacting
    public float interactionCooldownTimer = 2.0f;
    public bool interacting = false;
    public GameObject interactionIcon = null;
    private Coroutine interactionCoroutine = null;
    
    //Possible improvements: Break out the list and cooldown start function to this script

    public virtual void Interact(GameObject player)
    {
        // will be overwritten by inheritage
    }

    public virtual void Toss()
    {
        // will be overwritten by inheritage
    }

    public virtual void Teleport()
    {
        // will be overwritten by inheritage
    }


    /// <summary>
    /// Draws the interaction radius in the editor
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, interactionRadius);
    }

    public void DistanceCheck(GameObject player)
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, (player.transform.position + Vector3.up * 0.6f) - transform.position, out hit, interactionRadius, LayerMask.GetMask("Walls")) == false)
        {
            if ((player.transform.position - transform.position).sqrMagnitude < interactionRadius * interactionRadius)
            {
                Interact(player);
            }
        }
       
        
    }

    /// <summary>
    /// Displays the interaction icon
    /// </summary>
    public void ShowInteraction()
    {
        if(interactionIcon != null)
        {
            interactionIcon.SetActive(true);
        }
    }

    /// <summary>
    /// Hides the interaction icon
    /// </summary>
    public void HideInteraction()
    {
        if (interactionIcon != null)
        {
            interactionIcon.SetActive(false);
        }
    }

    /// <summary>
    /// Initiates cooldown coroutine
    /// </summary>
    public void StartInteraction()
    {
        if (interactionCoroutine == null)
        {
            interactionCoroutine = StartCoroutine(InteractionCooldown());
        }
        else
        {
            StopCoroutine(interactionCoroutine);
            interactionCoroutine = StartCoroutine(InteractionCooldown());
        }
    }

    public void StopInteraction()
    {
        interacting = true;
    }

    public void EnableInteraction()
    {
        interacting = false;
    }

    /// <summary>
    /// Sets a cooldown before the object can be interacted with again
    /// </summary>
    /// <returns></returns>
    public virtual IEnumerator InteractionCooldown()
    {
        interacting = true;
        yield return new WaitForSeconds(interactionCooldownTimer);
        interacting = false;
    }

}
