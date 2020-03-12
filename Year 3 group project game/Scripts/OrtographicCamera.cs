using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Main Author: Marcus Lundqvist

public class OrtographicCamera : MonoBehaviour
{
    [SerializeField] private float delayTime = 0.2f;
    [SerializeField] private float screenOffset = 4.0f;
    [SerializeField] private float minimumSize = 5.0f;
    [SerializeField] private Transform[] players = null;
    [SerializeField] private Vector3 cameraOffset = Vector3.zero;

    private Camera cameraReference = null;
    private float zoomSpeed = 0.0f;
    private Vector3 movementvelocity = Vector3.zero;
    private Vector3 desiredPosition = Vector3.zero;

    /// <summary>
    /// Sets values on start.
    /// </summary>
    private void Awake()
    {
        cameraReference = GetComponentInChildren<Camera>();
    }

    /// <summary>
    /// Activates the functions <see cref="Move"/> and <see cref="Zoom"/> in a late update.
    /// </summary>
    private void LateUpdate()
    {
        Move();
        Zoom();
    }

    /// <summary>
    /// Activates <see cref="Move"/> and updates the objects <see cref="Transform.position"/> using a <see cref="Vector3.SmoothDamp(Vector3, Vector3, ref Vector3, float)"/>
    /// </summary>
    private void Move()
    {
        FindAveragePosition();

        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition + cameraOffset, ref movementvelocity, delayTime);
    }

    /// <summary>
    /// Calculates the average position of the objects in the list <see cref="players"/>
    /// </summary>
    private void FindAveragePosition()
    {
        Vector3 averagePosition = new Vector3();
        int numberOfTargets = 0;

        foreach(Transform target in players)
        {
            if (!target.gameObject.activeSelf)
            {
                continue;
            }
            averagePosition += target.position;
            numberOfTargets++;
        }

        if(numberOfTargets > 0)
        {
            averagePosition /= numberOfTargets;
        }

        /*
         if player jumping == true
            averagepos = transform.pos
         */

        //averagePosition.y = transform.position.y; //this needs to change
        desiredPosition = averagePosition;
    }
    /// <summary>
    /// Sets the value of requiredSize variable to the return value from <see cref="FindRequiredSize"/> and then changes the camera size through a <see cref="Mathf.SmoothDamp(float, float, ref float, float)"/>.
    /// </summary>
    private void Zoom()
    {
        float requiredSize = FindRequiredSize();
        cameraReference.orthographicSize = Mathf.SmoothDamp(cameraReference.orthographicSize, requiredSize, ref zoomSpeed, delayTime);
    }

    /// <summary>
    /// Sets the value of size depending on <see cref="desiredPosition"/> relative to the screen.
    /// </summary>
    /// <returns>The desired size of the camera</returns>
    private float FindRequiredSize()
    {
        Vector3 desiredLocalPosition = transform.InverseTransformPoint(desiredPosition);

        float size = 0.0f;

        foreach(Transform target in players)
        {
            if (!target.gameObject.activeSelf)
            {
                continue;
            }
            Vector3 targetLocalPosition = transform.InverseTransformPoint(target.position);

            Vector3 desiredPositionToTarget = targetLocalPosition - desiredLocalPosition;

            size = Mathf.Max(size, Mathf.Abs(desiredPositionToTarget.y));
            size = Mathf.Max(size, Mathf.Abs(desiredPositionToTarget.x) / cameraReference.aspect);
        }

        size += screenOffset;

        size = Mathf.Max(size, minimumSize);


        return size;
    }

    
}
