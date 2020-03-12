using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientSoundPlayer : MonoBehaviour
{
    public AudioClip sound1;
    public AudioClip sound2;

    public AudioCrossfade crossFader;

    // Start is called before the first frame update
    void Start()
    {
        PlaySound1();
    }

    public void PlaySound1() 
    {
        crossFader.Play(sound1);
    }

    public void PlaySound2()
    {
        crossFader.Play(sound2);
    }
}
