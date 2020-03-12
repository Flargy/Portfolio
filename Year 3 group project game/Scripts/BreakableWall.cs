using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Main Author: Marcus Lundqvist

public class BreakableWall : MonoBehaviour
{
    [SerializeField] private float force = 200.0f;
    [SerializeField] private float timeToDestroy = 4.0f;
    private Rigidbody[] rb;
    private bool hasBeenUsed = false;

    /// <summary>
    /// Sets starting values to <see cref="rb"/>
    /// </summary>
    void Start()
    {
        rb = GetComponentsInChildren<Rigidbody>();
    }

    /// <summary>
    /// Applies force to each <see cref="Rigidbody"/> in the <see cref="rb"/> list if the object entering the trigger zone has a velocity magnitude greater than 5.5f.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody.velocity.magnitude > 5.5f && hasBeenUsed == false)
        {

            foreach(Rigidbody rig in rb)
            {
                rig.isKinematic = false;
                rig.useGravity = true;
                rig.AddForce(rig.gameObject.transform.forward * force);
            }
            hasBeenUsed = true;
            StartCoroutine(DestroyDebris());
        }
    }

    /// <summary>
    /// Destroys the object after a delay of <see cref="timeToDestroy"/>.
    /// </summary>
    /// <returns></returns>
    private IEnumerator DestroyDebris()
    {
        yield return new WaitForSeconds(timeToDestroy);
        Destroy(gameObject);
    }
}
