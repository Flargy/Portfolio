using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Main Author: Marcus Lundqvist

public class OutlineController : MonoBehaviour
{
    private Outline line = null;

    /// <summary>
    /// Sets values on start.
    /// </summary>
    void Awake()
    {
        line = GetComponent<Outline>();
        EventHandeler.Current.RegisterListener(EventHandeler.EVENT_TYPE.SwapOutlineEvent, ChangeOutline);
    }

    /// <summary>
    /// Reacts to an event and deactivates/reactivates the component <see cref="Outline"/>.
    /// </summary>
    /// <param name="info"></param>
    private void ChangeOutline(EventInfo info)
    {
        line.enabled = !line.enabled;
    }
}
