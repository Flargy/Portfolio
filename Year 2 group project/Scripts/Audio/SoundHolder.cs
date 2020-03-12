using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundHolder : MonoBehaviour
{

    [SerializeField] private List<AudioClip> soundClips = new List<AudioClip>();
    [SerializeField] private AudioSource mainAudioSource;
    [SerializeField] private AudioSource oneShotAudioSource;

    [SerializeField] private Animator musicAnim;
    private bool playing;
    // Start is called before the first frame update
    void Start()
    {
        EventHandeler.Current.RegisterListener(EventHandeler.EVENT_TYPE.AudioSound, AmbientSoundChanger);
        mainAudioSource.clip = soundClips[0];
        mainAudioSource.Play();
        mainAudioSource.volume = 1;
        playing = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // siffran -1 betyder att den sätter på eller stänger av.
    private void AmbientSoundChanger(EventInfo eventInfo)
    {
        AudioSoundEventInfo asei = (AudioSoundEventInfo)eventInfo;
        if(asei.Ambient == true) { 
            if(asei.soundIndex == -1 && playing)
            {
                musicAnim.SetTrigger("fadeOut");
                playing = false;
                Debug.Log("In");
            }
            else if(asei.soundIndex == -1 && playing == false)
            {
                Debug.Log("Ut");
                musicAnim.SetTrigger("fadeIn");
               // mainAudioSource.Play();
                playing = true;
            }
            else
            {
                mainAudioSource.clip = soundClips[asei.soundIndex];
                mainAudioSource.Play();
            }
        }
        else
        {
            oneShotAudioSource.clip = soundClips[asei.soundIndex];
            oneShotAudioSource.Play();

        }
    }
}
