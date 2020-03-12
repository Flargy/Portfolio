using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserScript : MonoBehaviour
{

    private AudioSource laserSound;

    [SerializeField] private AudioClip laserBeamSound = null;
    [SerializeField] private Transform startPoint = null;
    [SerializeField] private Transform endPoint = null;
    private LineRenderer laserLine = null;

    [SerializeField] private GameObject Laser = null;
    [SerializeField] private bool trapEnable = true;
    // Start is called before the first frame update
    void Start()
    {
        laserSound = gameObject.GetComponent<AudioSource>();

        InvokeRepeating("ToggleLaserTrap", 0, 2.5f);
        laserLine = GetComponent<LineRenderer>();
        laserLine.SetWidth(.2f, .2f);
    }

    // Update is called once per frame
    void Update()
    {
        laserLine.useWorldSpace = true;
        laserLine.SetPosition(0, startPoint.position);
        laserLine.SetPosition(1, endPoint.position);
    }

    private void ToggleLaserTrap()
    {
        trapEnable = !trapEnable;
        Laser.SetActive(trapEnable);
        laserSound.clip = laserBeamSound;
        laserSound.Play();
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
        }
    }
}
