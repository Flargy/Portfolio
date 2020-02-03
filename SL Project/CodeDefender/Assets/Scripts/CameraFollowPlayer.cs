using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    public GameObject player;

    [SerializeField] private float zDistanceFromPlayer = 5f;
    [SerializeField] private float heightOffset;
    [SerializeField] private float xDistanceFromPlayer = 0;
    [SerializeField] private float xRotation;
    

    private bool lerping = false;
    private Quaternion oldRotation;
    private Quaternion newRotation;
    private float newXDistance;
    private float newZDistance;
    private float lerpedXDistance;
    private float lerpedZDistance;
    private Vector3 lerpVector;
    private float timeAddition;

    void Update()
    {
        if (lerping)
        {
            timeAddition += Time.deltaTime;
            transform.rotation = Quaternion.Lerp(oldRotation, newRotation, 0.5f * timeAddition);
            lerpedXDistance = Mathf.Lerp(xDistanceFromPlayer, newXDistance, 0.5f * timeAddition);
            lerpedZDistance = Mathf.Lerp(zDistanceFromPlayer, newZDistance, 0.5f * timeAddition);
            lerpVector = new Vector3(player.transform.position.x - lerpedXDistance, (player.transform.position.y + heightOffset), player.transform.position.z - lerpedZDistance);
            transform.position = lerpVector;

            if (transform.rotation == newRotation)
            {
              
                zDistanceFromPlayer = newZDistance;
                xDistanceFromPlayer = newXDistance;
                lerping = false;

            }
        }
        else if(lerping == false)
        {
            transform.position = new Vector3((player.transform.position.x - xDistanceFromPlayer), (player.transform.position.y + heightOffset), (player.transform.position.z - zDistanceFromPlayer));

        }
    }

    public void ChangeCamera(float rotation, float xDistance, float zDistance)
    {
        timeAddition = 0.0f;
        lerping = true;
        newRotation = Quaternion.Euler(xRotation, rotation, 0);
        oldRotation = transform.rotation;
        newZDistance = zDistance;
        newXDistance = xDistance;
        lerpedXDistance = xDistanceFromPlayer;
        lerpedZDistance = zDistanceFromPlayer;
        heightOffset = 7.0f;
    }

    public void ChangeCamera(float rotation, float xDistance, float zDistance, float xRotation, float offset)
    {
        timeAddition = 0.0f;
        lerping = true;
        newRotation = Quaternion.Euler(xRotation, rotation, 0);
        oldRotation = transform.rotation;
        newZDistance = zDistance;
        newXDistance = xDistance;
        lerpedXDistance = xDistanceFromPlayer;
        lerpedZDistance = zDistanceFromPlayer;
        heightOffset = offset;
    }



}
