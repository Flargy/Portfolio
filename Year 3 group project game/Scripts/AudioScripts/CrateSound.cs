using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateSound : MonoBehaviour
{
    public enum HowToPlaySound { Height , Pickup}
    public HowToPlaySound howToPlaySound;

    public AudioSource source;

    public bool shouldPlaySound { get; set; }
    private float floorYCoordinate;

    private void Awake()
    {
        var floor = GameObject.FindGameObjectWithTag("Floor");
        floorYCoordinate = floor.transform.position.y;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(!shouldPlaySound) return;

        if (collision.gameObject.tag == "Floor")
        {
            shouldPlaySound = false;
            source.Play();
        }
    }

    private void Update()
    {
        if (howToPlaySound == HowToPlaySound.Height && transform.position.y > floorYCoordinate + 1f)
            shouldPlaySound = true;
    }
}
