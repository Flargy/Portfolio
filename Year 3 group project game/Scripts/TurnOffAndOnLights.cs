using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Main Author: Marcus Lundqvist

public class TurnOffAndOnLights : MonoBehaviour
{
    [SerializeField] private List<Light> oldLights;
    [SerializeField] private List<Light> newLights;
    [SerializeField] private float litStrength = 0.5f;
    [SerializeField] private float duration = 1f;
    private float lightStrength = 0.0f;
    private float t = 0.0f;
    private int playerCount = 0;
    private bool activated = false;
    private GameObject firstPlayer = null;

    /// <summary>
    /// Increases <see cref="playerCount"/> when a player enters the trigger zone.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.gameObject != firstPlayer)
            {
                firstPlayer = other.gameObject;
                playerCount++;
                if (playerCount >= 2)
                {
                    activated = true;
                    StartCoroutine(ChangeLights());
                }
            }
        }
    }

    /// <summary>
    /// Lowers the value of <see cref="playerCount"/> when <see langword="abstract"/>player exits.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerCount = Mathf.Max(0, playerCount - 1);
            firstPlayer = null;
        }
    }

    /// <summary>
    /// Lerps the light intensity of lights.
    /// </summary>
    /// <returns></returns>
    private IEnumerator ChangeLights()
    {
        if(oldLights.Count > 1) 
        { 
            lightStrength = oldLights[0].intensity;
        }
        foreach (Light light in newLights)
        {
           
            light.gameObject.SetActive(true);
            
        }
        while (t < 1.0f)
        {
            foreach(Light light in newLights)
            {
                light.intensity = Mathf.Lerp(0, litStrength, t);
                yield return null;
            }
            t += Time.deltaTime;
        }
        t = 0.0f;
        while(t < 1.0f)
        {
            foreach(Light light in oldLights)
            {
                
                light.intensity = Mathf.Lerp(lightStrength, 0, t);
                yield return null;
                if(light.intensity < 0.1f)
                {
                    light.gameObject.SetActive(false);
                }
            }
            t += Time.deltaTime/duration;
        }
        foreach(Light light in oldLights)
        {
            //Debug.Log(light.gameObject.name);
        }

        t = 0.0f;
        Destroy(this);
    }
}
