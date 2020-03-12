using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Author: Marcus Lundqvist

public class CelestialBody : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    [SerializeField] private GameObject holder;
    [SerializeField] private float intecityLevel;

    [SerializeField] private Light light;
    [SerializeField] private Material skyMaterial;
    private Material currentSky;
    private float timer = 0;
    private bool nightChange = false;
    private bool dayChange = false;

    private void Start()
    {
        light = GetComponent<Light>();
    }
    /// <summary>
    /// Rotates the object along the Z axis of the world around the assigned object <see cref="holder"/>.
    /// Makes the rotating object always face the <see cref="holder"/>.
    /// </summary>
    void Update()
    {
        if (transform.localPosition.z > 0 && transform.localPosition.y < 300 && transform.localPosition.y > 100) { 
            timer += Time.deltaTime;
        }
        else if(transform.localPosition.z < 0 && transform.localPosition.y >-300 && transform.localPosition.y < -100)
        {
            timer += Time.deltaTime;
        }
        if (transform.localPosition.z > 0 && transform.localPosition.y < 100 && nightChange == false) { 
            StartCoroutine(TurnOffLight());
        }
        else if (transform.localPosition.z < 0 && transform.localPosition.y > -100  && dayChange == false) { 
            StartCoroutine(TurnOnLight());
        }


        transform.RotateAround(holder.transform.position, Vector3.right, rotationSpeed * Time.deltaTime);
        transform.LookAt(holder.transform.position);
        
    }

    private IEnumerator TurnOffLight()
    {
        nightChange = true;
        float check = 0f;
        while (check < timer)
        {
            float t = check / timer;
            light.intensity = Mathf.Lerp(intecityLevel, 0, t);
            check += Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        light.intensity = 0; 
        dayChange = false;
        timer = 0;
    }

    private IEnumerator TurnOnLight()
    {
        currentSky = RenderSettings.skybox;
        dayChange = true;
        float check = 0f;
        while (check < timer)
        {
            check += Time.deltaTime;
            float t = check / timer;
            light.intensity = Mathf.Lerp(0, intecityLevel, t);
            RenderSettings.skybox.Lerp(currentSky, skyMaterial, t);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        light.intensity = intecityLevel;
        nightChange = false;
        timer = 0;
    }
}
