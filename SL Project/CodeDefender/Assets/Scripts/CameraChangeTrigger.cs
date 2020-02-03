using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChangeTrigger : MonoBehaviour
{
    [SerializeField] private float yRotation;
    [SerializeField] private float xDistance;
    [SerializeField] private float zDistance;
    [SerializeField] private float xRotation = 0;
    [SerializeField] private float heightOffset = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && xRotation == 0)
        {
            Camera.main.GetComponent<CameraFollowPlayer>().ChangeCamera(yRotation, xDistance, zDistance);
        }
        else if (other.CompareTag("Player"))
        {
            Camera.main.GetComponent<CameraFollowPlayer>().ChangeCamera(yRotation, xDistance, zDistance, xRotation, heightOffset);
        }
    }
}
