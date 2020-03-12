using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Main Author: Marcus Lundqvist

public class DynamicObject : MonoBehaviour
{
    void Start()
    {
        EventHandeler.Current.RegisterListener(EventHandeler.EVENT_TYPE.SwapToNight, SwapToNight);
        EventHandeler.Current.RegisterListener(EventHandeler.EVENT_TYPE.SwapToDay, SwapToDay);
    }

    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="info"> Information brought from the <see cref="EventInfo"/></param>
    private void SwapToNight(EventInfo info)
    {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="info">Information brought from the <see cref="EventInfo"/></param>
    private void SwapToDay(EventInfo info)
    {
        gameObject.SetActive(true);
    }
}
