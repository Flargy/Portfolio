using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour {


    private bool masterSound = true;
    private float effectValue;
    private float musicValue;

    public UnityEngine.UI.Slider master;
    public UnityEngine.UI.Slider effect;
    public UnityEngine.UI.Slider music;
    // Use this for initialization
    void Start () {
        masterSound = true;
        effectValue = effect.value;
        musicValue = music.value;
	}




	
	// Update is called once per frame
	void Update () {
        if (masterSound)
        {
            effectValue = 0;
            musicValue = 0;
        }
        else
        {
            effectValue = effect.value;
            musicValue = music.value;
        }
	}

    public void masterSwitch()
    {
        masterSound = !masterSound;
    }
}
