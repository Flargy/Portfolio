using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceAngleDirection : MonoBehaviour
{
    private RaycastHit hit;

    /// <summary>
    /// Sends 2 raycasts downwards, one from of the actors forward position and a one at its backward position.
    /// The Actors <see cref="Transform.forward"/> is then set to the front raycasts hit location subtracted by the back raycasts hit location
    /// </summary>
    public void FixedUpdate()
    {
        if (Physics.Raycast(transform.position + transform.forward, Vector3.down, out hit, LayerMask.GetMask("Default")))
        {
            Vector3 forwardHit = hit.point; 
            if (Physics.Raycast(transform.position - transform.forward, Vector3.down, out hit, LayerMask.GetMask("Default")))
            {
                Vector3 backwardsHit = hit.point; 
                transform.forward = forwardHit - backwardsHit;
            }
        }
    }
}
