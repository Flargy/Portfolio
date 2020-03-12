using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateBoxRespawn : MonoBehaviour
{
    [SerializeField] Transform spawnPoint = null;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CarryBox"))
        {
            other.GetComponent<InteractionPickUp>().ChangeRespawn(spawnPoint);
        }
    }
}
