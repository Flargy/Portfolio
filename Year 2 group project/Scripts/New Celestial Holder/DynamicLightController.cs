using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicLightController : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    [SerializeField] private GameObject holder;
    [SerializeField] private float swapPosition;
    [SerializeField] private Vector3 startPosition;
    [SerializeField] private Color dayColor;
    [SerializeField] private Color nightColor;
    [SerializeField] private float dayIntensity;
    [SerializeField] private float nightIntensity;
    [SerializeField] private Material daySky;
    [SerializeField] private Material nightSky;


    private Light lighting;
    private bool isDay = true;
    private Material currentMaterial;

    //Lerp values
    private float temp = 0f;
    private float totalTime = 0;
    private bool lerpStarted = false;
    private float lerpR;
    private float lerpG;
    private float lerpB;


    void Start()
    {
        lighting = GetComponent<Light>();
        lighting.color = dayColor;
    }


    void Update()
    {
        transform.RotateAround(holder.transform.position, Vector3.right, rotationSpeed * Time.deltaTime);
        transform.LookAt(holder.transform.position);

        if (transform.localPosition.y < 100 && transform.localPosition.y > 50 && transform.localPosition.z > 0)
        {
            temp += Time.deltaTime;
        }
        
        if(lerpStarted == false && transform.localPosition.y < 50)
        {

            lerpStarted = true;
            if (isDay)
                StartCoroutine(StartLerpingToNight());
            else if (!isDay)
                StartCoroutine(StartLerpingToDay());
        }

        if (transform.localPosition.y <= 0)
        {
            gameObject.transform.localPosition = startPosition;
            isDay = !isDay;
            SwapValues();
        }


    }

    /// <summary>
    /// Triggers <see cref="DayEventInfo"/> and <see cref="NightEventInfo"/> depending on the value of <see cref="isDay"/>
    /// </summary>
    private void SwapValues()
    {
        if (isDay)
        {
            //lighting.intensity = dayIntensity;
            //lighting.color = dayColor;
            DayEventInfo dei = new DayEventInfo { };
            EventHandeler.Current.FireEvent(EventHandeler.EVENT_TYPE.SwapToDay, dei);
        }
        else
        {
            //lighting.intensity = nightIntensity;
            //lighting.color = nightColor;

            NightEventInfo nei = new NightEventInfo { };
            EventHandeler.Current.FireEvent(EventHandeler.EVENT_TYPE.SwapToNight, nei);

        }
    }

    /// <summary>
    /// Lerps the value of <see cref="lighting"/> between night and day values.
    /// </summary>
    /// <returns></returns>
    private IEnumerator StartLerpingToDay()
    {
        currentMaterial = RenderSettings.skybox;
        float check = temp * 2;
        while (totalTime < check)
        {
            float t = totalTime / check;
            //float p = totalTime / temp;
            lighting.intensity = Mathf.Lerp(nightIntensity, dayIntensity, t);

            lerpR = Mathf.Lerp(nightColor.r, dayColor.r, t);
            lerpG = Mathf.Lerp(nightColor.g, dayColor.g, t);
            lerpB = Mathf.Lerp(nightColor.b, dayColor.b, t);

            RenderSettings.skybox.Lerp(currentMaterial, daySky, t);
            //render.material.lerp(NightSky, DaySky, totaltime)

            lighting.color = new Color(lerpR, lerpG, lerpB);
            totalTime += Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        //float ko = 0;
        //while (totalTime < temp)
        //{
        //    float t = totalTime / check;
        //    float tp = ko / temp;
        //    lighting.intensity = Mathf.Lerp(0.2f, dayIntensity, tp);

        //    lerpR = Mathf.Lerp(nightColor.r, dayColor.r, t);
        //    lerpG = Mathf.Lerp(nightColor.g, dayColor.g, t);
        //    lerpB = Mathf.Lerp(nightColor.b, dayColor.b, t);

        //    RenderSettings.skybox.Lerp(currentMaterial, daySky, t);
        //    //render.material.lerp(NightSky, DaySky, totaltime)

        //    lighting.color = new Color(lerpR, lerpG, lerpB);
        //    totalTime += Time.deltaTime;
        //    ko += Time.deltaTime;
        //    yield return new WaitForSeconds(Time.deltaTime);
        //}
        temp = 0f;
        totalTime = 0f;
        lerpStarted = false;
    }

    /// <summary>
    /// Lerps the value of <see cref="lighting"/> between day and night.
    /// </summary>
    /// <returns></returns>
    private IEnumerator StartLerpingToNight()
    {
        
        currentMaterial = RenderSettings.skybox;
        float check = temp * 2;
        while (totalTime < check)
        {
            float t = totalTime / check;
            lighting.intensity = Mathf.Lerp(dayIntensity, nightIntensity , t);

            lerpR = Mathf.Lerp(dayColor.r, nightColor.r, t);
            lerpG = Mathf.Lerp(dayColor.g, nightColor.g, t);
            lerpB = Mathf.Lerp(dayColor.b, nightColor.b, t);

            RenderSettings.skybox.Lerp(currentMaterial, nightSky, t);

            lighting.color = new Color(lerpR, lerpG, lerpB);
            totalTime += Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }

        temp = 0f;
        totalTime = 0f;
        lerpStarted = false;
    }
}
