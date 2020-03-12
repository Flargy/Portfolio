using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{

    private Transform camHead = null;

    private bool startNextRotation = true;
    [SerializeField] private bool rotateRight;

    [SerializeField] private float yaw = 0;
    [SerializeField] private float pitch = 0;
    [SerializeField] private float secondsToRotate = 0;
    [SerializeField] private float rotateSwitchTime;

    private void Start()
    {
        
        camHead = transform.GetChild(0);
        camHead.localRotation = Quaternion.AngleAxis(pitch, Vector3.forward);
        SetUpStartRotation();
    }

    private void Update()
    {
        if(startNextRotation && rotateRight)
        {
            StartCoroutine(Rotate(yaw, secondsToRotate));
        }else if (startNextRotation && !rotateRight)
        {
            StartCoroutine(Rotate(-yaw, secondsToRotate));

        }
    }

    IEnumerator Rotate(float yaw, float duration)
    {

        startNextRotation = false;
        Quaternion initialRotation = transform.rotation;

        float timer = 0f;

        while(timer < duration)
        {
            timer += Time.deltaTime;
            transform.rotation = initialRotation * Quaternion.AngleAxis(timer / duration * yaw, Vector3.up);
            yield return null;
        }

        yield return new WaitForSeconds(rotateSwitchTime);

        startNextRotation = true;
        rotateRight = !rotateRight;
    }

    void SetUpStartRotation()
    {
        if (rotateRight)
        {
            transform.localRotation = Quaternion.AngleAxis(-yaw / 2, Vector3.up);
        }
        else
        {
            transform.localRotation = Quaternion.AngleAxis(yaw / 2, Vector3.up);
        }
    }
}
