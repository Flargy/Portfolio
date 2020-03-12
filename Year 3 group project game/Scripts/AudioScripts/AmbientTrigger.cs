using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientTrigger : MonoBehaviour
{
    public AmbientSoundPlayer ambientSoundPlayer;
    public int playersRequired = 2;
    private List<GameObject> players = new List<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !players.Contains(other.gameObject))
        { 
            players.Add(other.gameObject);
        }

        CheckSound();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && players.Contains(other.gameObject))
        {
            players.Remove(other.gameObject);
        }

        CheckSound();
    }

    private void CheckSound() 
    {
        if (players.Count >= playersRequired)
            ambientSoundPlayer.PlaySound2();
        else
            ambientSoundPlayer.PlaySound1();
    }
}
