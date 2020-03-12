using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDoor : MonoBehaviour
{
    [SerializeField] private Rigidbody leftDoor;
    [SerializeField] private Rigidbody rightDoor;

    [SerializeField] private bool itsATrap;

    void Start()
    {

        leftDoor.useGravity = false;
        leftDoor.isKinematic = true;
        rightDoor.useGravity = false;
        rightDoor.isKinematic = true;
    }

     void OnTriggerEnter(Collider other)
    {
        if (itsATrap)
        {
            leftDoor.useGravity = true;
            leftDoor.isKinematic = false;
            rightDoor.useGravity = true;
            rightDoor.isKinematic = false;
        }
    }
}
