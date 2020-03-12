using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaSoundLoop : MonoBehaviour
{
    [SerializeField] private bool isForTwoPlayers = false;
    [SerializeField] private AudioClip sound = null;

    private int playerCounter = 0;
    private AudioSource source = null;
    private bool soundIsPlaying = false;
    void Start()
    {
        source = GetComponent<AudioSource>();
        if(sound != null)
        {
            source.clip = sound;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && soundIsPlaying == false)
        {
            if(isForTwoPlayers == false)
            {
                source.Play();
                soundIsPlaying = true;
            }
            else
            {
                playerCounter = Mathf.Min(playerCounter + 1, 2);
                if(playerCounter == 2)
                {
                    source.Play();
                    soundIsPlaying = true;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player") && soundIsPlaying == true)
        {
            if(isForTwoPlayers == false)
            {
                source.Stop();
                soundIsPlaying = false;
            }
            else
            {
                playerCounter = Mathf.Max(playerCounter - 1, 0);
                if(playerCounter == 0)
                {
                    source.Stop();
                    soundIsPlaying = false;
                }
            }
        }
    }
}
