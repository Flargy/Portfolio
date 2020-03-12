using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Main Author: Marcus Lundqvist

public class MultipleTargetCamera : MonoBehaviour
{
    [SerializeField] private List<Transform> players = null;
    [SerializeField] private Vector3 offset = Vector3.zero;
    [SerializeField] private float maxZoom = 10;
    [SerializeField] private float minZoom = 30;
    [SerializeField] private float zoomLimiter = 50;

    private Bounds bound;
    private Vector3 centerPoint;
    private Vector3 newPosition;
    private Vector3 velocity;
    private float smoothTime = 0.5f;
    private Camera cam;

    /// <summary>
    /// Sets values on startup.
    /// </summary>
    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    /// <summary>
    /// Activates the functions <see cref="Move"/> and <see cref="Zoom"/>.
    /// </summary>
    private void LateUpdate()
    {
        Move();
        Zoom();

    }

    /// <summary>
    /// 
    /// </summary>
    private void Zoom()
    {
        float newZoom = Mathf.Lerp(maxZoom, minZoom, GreatestDistance() / zoomLimiter);
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, newZoom, Time.deltaTime);
    }

    private void Move()
    {
        centerPoint = GetCenterPoint();

        newPosition = centerPoint + offset;

        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
    }

    private float GreatestDistance()
    {
        Bounds bound = new Bounds(players[0].position, Vector3.zero);
        for (int i = 1; i < players.Count; i++)
        {
            bound.Encapsulate(players[i].position);
        }
        return bound.size.x;
    }

    private Vector3 GetCenterPoint()
    {
        
        Bounds bound = new Bounds(players[0].position, Vector3.zero);
        for(int i = 0; i < players.Count; i++)
        {
            bound.Encapsulate(players[i].position);
        }
        
        

        return bound.center;
    }
}
