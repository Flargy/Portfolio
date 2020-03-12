using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTrigger : MonoBehaviour
{

    private AudioSource audioSource;
    [SerializeField] private AudioClip triggerSound;

    private List<GameObject> playersInArea = new List<GameObject>();

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            playersInArea.Add(other.gameObject);
            audioSource.PlayOneShot(triggerSound);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (playersInArea.Contains(other.gameObject))
            playersInArea.Remove(other.gameObject);

        if (playersInArea.Count == 0)
            audioSource.Stop();
    }

}
