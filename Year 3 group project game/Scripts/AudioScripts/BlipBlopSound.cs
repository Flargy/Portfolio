using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlipBlopSound : MonoBehaviour
{
    [SerializeField] private AudioClip blipblopClip;
    private AudioSource blipBlopSource;

    // Start is called before the first frame update
    void Start()
    {
        blipBlopSource = GetComponent<AudioSource>();
        blipBlopSource.clip = blipblopClip;
        blipBlopSource.loop = true;

        blipBlopSource.time = Random.Range(0, blipblopClip.length);

        blipBlopSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
