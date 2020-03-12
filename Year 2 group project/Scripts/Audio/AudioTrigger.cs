using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTrigger : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip clip;

    

    [SerializeField] private int audioIndex;
    // Start is called before the first frame update
    void Start()
    {
        //source = GetComponent<AudioSource>();
        source.clip = clip;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            // triggar du ett event
            AudioSoundEventInfo asei = new AudioSoundEventInfo { Ambient = true, soundIndex = -1 };
            EventHandeler.Current.FireEvent(EventHandeler.EVENT_TYPE.AudioSound, asei);
            source.mute = false;

            if (!source.isPlaying)
            {
                source.loop = true;
                source.Play();
            }

        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            // triggar du ett event
            AudioSoundEventInfo asei = new AudioSoundEventInfo { Ambient = true, soundIndex = -1 };
            EventHandeler.Current.FireEvent(EventHandeler.EVENT_TYPE.AudioSound, asei);

           // AudioSoundEventInfo asei2 = new AudioSoundEventInfo { Ambient = true, soundIndex = audioIndex};
            //EventHandeler.Current.FireEvent(EventHandeler.EVENT_TYPE.AudioSound, asei2);

            source.mute = true;

        }
    }


}
