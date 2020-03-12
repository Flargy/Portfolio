using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceLineTrigger : MonoBehaviour
{
    [SerializeField] private AudioSource previous = null;
    private AudioSource source = null;
    private bool hasActivated = false;

    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && hasActivated == false)
        {
            source.Play();
            hasActivated = true;
            if(previous != null && previous.isPlaying)
            {
                previous.Stop();
            }
        }
    }
}
