using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Main Author: Marcus Lundqvist

public class CheckpointUpdate : MonoBehaviour
{
    [SerializeField] private Transform[] respawnPositions = null;

    private GameObject firstPlayer = null;
    private GameObject secondPlayer = null;
    private int counter = 0;

    /// <summary>
    /// Updates the respawn position of the players who enter the trigger zone.
    /// When 2 different players have entered the object destroys itself.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(firstPlayer == null)
            {
                firstPlayer = other.gameObject;
                other.GetComponent<NewPlayerScript>().ChangeSpawnPoint(respawnPositions[counter].position);
                counter++;
            }
            else if(firstPlayer != other.gameObject && secondPlayer == null)
            {
                secondPlayer = other.gameObject;
                other.GetComponent<NewPlayerScript>().ChangeSpawnPoint(respawnPositions[counter].position);
                counter++;
            }
        }
        
        if(counter == 2)
        {
            Destroy(gameObject);
        }
        

    }

}
