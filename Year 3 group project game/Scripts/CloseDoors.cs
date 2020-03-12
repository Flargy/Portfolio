using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Main Author: Marcus Lundqvist

public class CloseDoors : MonoBehaviour
{
    [SerializeField] private GameObject[] doorsToClose;
    [SerializeField] private GameObject[] doorsToOpen;
    [SerializeField] private Vector3 closedRotation = Vector3.zero;
    [SerializeField] private Vector3 openRotation = Vector3.zero;

    private float t = 0.0f;
    private int playerCount = 0;
    private bool activated = false;

    /// <summary>
    /// Increases the value of <see cref="playerCount"/> each time a player enters the trigger zone.
    /// If the counter has reached a value of 2 it starts a coroutine.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        
        playerCount++;
        if(playerCount == 2 && activated == false)
        {
            activated = true;
            StartCoroutine(OpenAndCloseDoors());
        }
        
    }

    /// <summary>
    /// Lowers the counter if a player leaves the trigger zone.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerCount--;
        }
        
    }

    /// <summary>
    /// Opens and closes doors through <see cref="Quaternion.Lerp(Quaternion, Quaternion, float)"/>
    /// </summary>
    /// <returns></returns>
    private IEnumerator OpenAndCloseDoors()
    {
        
        while(t < 1.0f)
        {
            t += Time.deltaTime;
            foreach(GameObject door in doorsToClose)
            {
                door.transform.rotation = Quaternion.Euler(Vector3.Lerp(openRotation, closedRotation, t));
            }

            foreach(GameObject door in doorsToOpen)
            {
                door.transform.rotation = Quaternion.Euler(Vector3.Lerp(closedRotation, openRotation, t));
            }
            yield return null;
        }
        Destroy(gameObject);
    }
}
