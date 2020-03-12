using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerSounds : MonoBehaviour
{

    private AudioSource audioSource;
    [SerializeField] private AudioClip[] footStepsSounds = null;
    [SerializeField] private AudioClip[] jumpingSounds = null;
    [SerializeField] private AudioClip landingSound = null;
    [SerializeField] private AudioClip rustleSound = null;
    [SerializeField] private AudioClip pickupSound = null;



    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }


    public void PlayFootStepSound()
    {
        int n = Random.Range(1, footStepsSounds.Length);
        audioSource.clip = footStepsSounds[n];
        audioSource.PlayOneShot(audioSource.clip);
        footStepsSounds[n] = footStepsSounds[0];
        footStepsSounds[0] = audioSource.clip;

    }

    public void PlayJumpSounds()
    {
        int n = Random.Range(1, jumpingSounds.Length);
        audioSource.clip = jumpingSounds[n];
        audioSource.PlayOneShot(audioSource.clip);
        jumpingSounds[n] = jumpingSounds[0];
        jumpingSounds[0] = audioSource.clip;
    }

    public void PlayLandingSound()
    {
        audioSource.PlayOneShot(landingSound);
    }

    public void PlayRustle()
    {

        audioSource.PlayOneShot(rustleSound);

    }

    public void PlayPickup() 
    {
        audioSource.PlayOneShot(pickupSound);
    }

}
