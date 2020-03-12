using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuteToggleHandler : MonoBehaviour
{
    public Toggle toggle; 

    public void Awake()
    {
        toggle.isOn = PlayerPrefs.HasKey("mute") ? PlayerPrefs.GetInt("mute") == 1 : false;
        MuteToggleHandlerMethod(toggle.isOn);
    }

    public void MuteToggleHandlerMethod(bool value)
    {
        AudioListener.pause = value;
        PlayerPrefs.SetInt("mute", value ? 1 : 0);
    }
}
