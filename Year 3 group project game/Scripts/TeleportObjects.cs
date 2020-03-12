using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Main Author: Marcus Lundqvist

public class TeleportObjects : MonoBehaviour
{
    [SerializeField] private Transform teleportToLocation = null;
    [SerializeField] private float transferTime = 2.0f;
    [SerializeField] private float transportForce = 2.0f;
    private AudioSource audioSource;
    public AudioClip boxTeleportSound;

    /// <summary>
    /// Sets starting values to variables
    /// </summary>
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Starts coroutine if a box or misc item enters the trigger zone.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CarryBox") || other.CompareTag("Misc"))
        {
            
            StartCoroutine(MoveBox(other.gameObject));
            
        }
    }

    /// <summary>
    /// Deactivates the item and relocates it to <see cref="teleportToLocation"/> after a delay of <see cref="transferTime"/>.
    /// </summary>
    /// <param name="box"></param>
    /// <returns></returns>
    private IEnumerator MoveBox(GameObject box)
    {
        box.GetComponent<Interactable>().Teleport();
        box.SetActive(false);
        box.transform.rotation = teleportToLocation.rotation * Quaternion.Euler(0, 90, 0);
        audioSource.PlayOneShot(boxTeleportSound);
        box.GetComponent<Rigidbody>().velocity = teleportToLocation.forward * transportForce;
        yield return new WaitForSeconds(transferTime);
        box.transform.position = teleportToLocation.position;
        box.SetActive(true);

    }
}
