using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidBodyLauncher : MonoBehaviour
{
    [SerializeField] private LayerMask launchObjectMask = 0;
    [SerializeField] private Vector3 halfBoxSize = Vector3.one;
    [SerializeField] private RigidBodyLauncher launcher = null;

    private void FillAndLaunch(float launchStrength)
    {
        Collider[] launchObjects = Physics.OverlapBox(transform.position + Vector3.up, halfBoxSize, Quaternion.identity, launchObjectMask);

        foreach(Collider col in launchObjects)
        {
            col.GetComponent<Rigidbody>().velocity += Vector3.up * Mathf.Abs(launchStrength);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //if(other.TryGetComponent(out Rigidbody rb))
        //{
        //    launcher.FillAndLaunch(rb.velocity.y);
        //}
        
    }
    
}
