using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Main Author: Marcus Lundqvist

public class RenderPath : MonoBehaviour
{
    [SerializeField] private GameObject boxDisplay = null;
    [SerializeField] private float initialVelocity = 6.0f;
    [SerializeField] private float timeResolution = 0.1f;
    [SerializeField] private float maxTime = 1.6f;
    [SerializeField] private LayerMask hitLayer = -1;

    private LineRenderer lr;
    private GameObject boxDisplayInstanceFar = null;
    //private GameObject boxDisplayInstanceNear = null;
    private bool isLifted = false;

    /// <summary>
    /// Sets start values of variables.
    /// </summary>
    private void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.enabled = false;
    }

    /// <summary>
    /// Changes the value of <see cref="isLifted"/> and enables the <see cref="LineRenderer"/>.
    /// </summary>
    public void SwapLifted()
    {
        isLifted = !isLifted;
        lr.enabled = isLifted;
        if(boxDisplayInstanceFar != null)
        {
            Destroy(boxDisplayInstanceFar);
            //Destroy(boxDisplayInstanceNear);
        }
    }

    /// <summary>
    /// Changes value of <see cref="isLifted"/> to true and enables the <see cref="LineRenderer"/>.
    /// </summary>
    public void ShowPath()
    {
        isLifted = true;
        lr.enabled = true;
    }

    /// <summary>
    /// Changes the value of <see cref="isLifted"/> to false and disables the <see cref="LineRenderer"/> and destroys the <see cref="boxDisplayInstanceFar"/> if active.
    /// </summary>
    public void HidePath()
    {
        isLifted = false;
        lr.enabled = false;
        if (boxDisplayInstanceFar != null)
        {
            Destroy(boxDisplayInstanceFar);
            //Destroy(boxDisplayInstanceNear);
        }
    }

    /// <summary>
    /// Fills the <see cref="LineRenderer"/> with positions and calculates where the box should hit.
    /// </summary>
    private void Update()
    {
        if(isLifted == true) { 
        Vector3 velocityVector = transform.forward * initialVelocity;


        int index = 0;

        Vector3 currentPosition = transform.position;
        RaycastHit hit;

        //if(boxDisplay != null)
        //    {
        //        if(boxDisplayInstanceNear != null)
        //        {
        //            boxDisplayInstanceNear.transform.position = transform.position + transform.forward * 0.5f + transform.up * -1.35f;
        //            boxDisplayInstanceNear.transform.rotation = transform.rotation;
        //        }
        //        else
        //        {
        //            boxDisplayInstanceNear = Instantiate(boxDisplay, transform.position + transform.forward * 0.5f + transform.up * -1.5f, Quaternion.identity) as GameObject;
        //        }
        //    }

            for (float t = 0.0f; t <= maxTime; t += timeResolution)
            {

                lr.positionCount = index + 1;
                lr.SetPosition(index, currentPosition);
                if (Physics.Raycast(currentPosition, velocityVector, out hit, 1.0f, hitLayer))
                {
                    lr.positionCount = index + 2;
                    lr.SetPosition(index + 1, hit.point);

                    if (boxDisplay != null)
                    {
                        if (boxDisplayInstanceFar != null)
                        {
                            boxDisplayInstanceFar.transform.position = hit.point + Vector3.up * 0.2f;
                            boxDisplayInstanceFar.transform.rotation = transform.rotation;
                        }
                        else
                        {
                            boxDisplayInstanceFar = Instantiate(boxDisplay, hit.point + Vector3.up * 0.2f, Quaternion.identity) as GameObject;
                        }
                    }
                    break;
                }
                
                currentPosition += velocityVector * timeResolution;
                velocityVector += Physics.gravity * timeResolution;
                index++;
            }
            if(Physics.Raycast(currentPosition, velocityVector, 1.0f, hitLayer) == false && boxDisplayInstanceFar != null)
            {
                Destroy(boxDisplayInstanceFar);
            }
        }
    }
}
