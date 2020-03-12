using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Main Author: Marcus Lundqvist

public class SeeThroughWallRaycast : MonoBehaviour
{
    [SerializeField] private GameObject seeThrough = null;
    [SerializeField] private LayerMask wallLayer = 0;
    [SerializeField] private GameObject cam = null;
    [SerializeField] private float radius = 0.5f;

    private List<Vector3> castPoints;

    /// <summary>
    /// Fills the list <see cref="castPoints"/> with 5 different points
    /// </summary>
    private void Awake()
    {
        castPoints = new List<Vector3>();
        castPoints.Add(Vector3.up * 0.6f);
        castPoints.Add(Vector3.up * 0.6f + Vector3.right * radius);
        castPoints.Add(Vector3.up * 0.6f + Vector3.left * radius);
        castPoints.Add(Vector3.up * 0.6f + Vector3.up * radius);
        castPoints.Add(Vector3.up * 0.6f + Vector3.down * radius);
    }

    /// <summary>
    /// Sends raycasts towards the position of <see cref="cam"/> from each point in <see cref="castPoints"/>.
    /// If any of the raycasts hits the desired layer it activates the <see cref="seeThrough"/> object.
    /// </summary>
    private void FixedUpdate()
    {
        
        RaycastHit hit;
        
        for ( int i = 0; i < 5; i++)
        {
            if(Physics.Raycast(transform.position + castPoints[i], cam.transform.position - transform.position, out hit, 20.0f, wallLayer))
            {
                seeThrough.transform.LookAt(cam.transform.position, Vector3.up);
                if (seeThrough.activeSelf == false)
                {
                    seeThrough.SetActive(true);
                    break;
                }
               
            }
            else
            {
                if (seeThrough.activeSelf == true)
                {
                    seeThrough.SetActive(false);
                }
            }
        }
        
    }

}
