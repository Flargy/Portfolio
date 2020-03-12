using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicLight : MonoBehaviour
{
    [SerializeField] private Material daySky;
    [SerializeField] private Material nightSky;
    void Start()
    {
        //EventHandeler.Current.RegisterListener(EventHandeler.EVENT_TYPE.DAY_EVENT, SwapToDay);
        //EventHandeler.Current.RegisterListener(EventHandeler.EVENT_TYPE.NIGHT_EVENT, SwapToNight);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SwapToNight(EventInfo evenInfo)
    {
        RenderSettings.skybox = nightSky;
    }

    private void SwapToDay(EventInfo eventInfo)
    {
        RenderSettings.skybox = daySky;
    }
}
