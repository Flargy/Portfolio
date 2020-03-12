using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Main Author: Marcus Lundqvist

public class CinematicCamera : MonoBehaviour
{
    [SerializeField] private Transform endPosition = null;
    [SerializeField] private float movementDuration = 5.0f;

    private float lerpTime = 0;
    private float t = 0;
    private Vector3 goToPosition = Vector3.zero;
    private Vector3 goFromPosition = Vector3.zero;

    /// <summary>
    /// Sets values to variables on startup
    /// </summary>
    void Start()
    {
        goToPosition = endPosition.position;
        goFromPosition = transform.position;
        StartCoroutine(ChangePosition());
    }

    /// <summary>
    /// Lerps the position of the <see cref="GameObject"/> towards <see cref="goToPosition"/> over the time <see cref="movementDuration"/>
    /// </summary>
    /// <returns></returns>
    private IEnumerator ChangePosition()
    {
        while (lerpTime < movementDuration)
        {
            t += Time.deltaTime / movementDuration;
            transform.position = Vector3.Lerp(goFromPosition, goToPosition, t);
            lerpTime += Time.deltaTime;
            yield return null;
        }
    }
}
