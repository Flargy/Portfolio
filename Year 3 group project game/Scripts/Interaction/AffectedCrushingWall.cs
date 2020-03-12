using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Main Author: Marcus Lundqvist

public class AffectedCrushingWall : AffectedObject
{
    [SerializeField] private Vector3 startPosition = Vector3.zero;
    [SerializeField] private Transform endPosition = null;
    [SerializeField] private GameObject deathColliderHolder = null;
    [SerializeField] [Range(0.5f, 0.9f)] private float deathColliderActivationPercentage = 0.8f;

    private float lerpTime = 0;
    private float t = 0;
    private Vector3 goToPosition = Vector3.zero;
    private Vector3 goFromPosition = Vector3.zero;
    private Rigidbody rb = null;
    private Coroutine movement = null;
    private bool coroutineIsRunning = false;
    private BoxCollider[] boxes;


    /// <summary>
    /// Is triggered by an interactable object and starts the coroutine for moving objects
    /// </summary>
    public override void ExecuteAction()
    {
        if (coroutineIsRunning == true)
        {
            StopCoroutine(movement);
            SwapLocationValues();
            coroutineIsRunning = false;
        }
        movement = StartCoroutine(ChangePosition());
    }

    /// <summary>
    /// Sets starting values to variables
    /// </summary>
    private void Start()
    {
        rb = GetComponentInChildren<Rigidbody>();
        startPosition = rb.transform.position;
        goToPosition = endPosition.position;
        goFromPosition = startPosition;
        boxes = rb.gameObject.GetComponents<BoxCollider>();
        deathColliderHolder.SetActive(false);

    }

    /// <summary>
    /// Moves the rigidbody object to a specific location over a set amount of time and activates a killbox depending on position
    /// </summary>
    /// <returns> Returns a pause using the same time as the current frame</returns>
    private IEnumerator ChangePosition()
    {
       
        coroutineIsRunning = true;
        while (lerpTime < actionDuration)
        {
            t += Time.deltaTime / actionDuration;
            rb.MovePosition(Vector3.Lerp(goFromPosition, goToPosition, t));
            lerpTime += Time.deltaTime;
            if(goToPosition == startPosition && t >= deathColliderActivationPercentage && deathColliderHolder.activeSelf == false)
            {
                deathColliderHolder.SetActive(true);
            }
            yield return null;
        }

        SwapLocationValues();
       

        coroutineIsRunning = false;

    }

    /// <summary>
    /// Changes the values of from and to so that the next activation will send the object back to it's former position
    /// </summary>
    private void SwapLocationValues()
    {
        t = 0.0f;
        lerpTime = 0.0f;
        goFromPosition = rb.transform.position;
        if (goToPosition == endPosition.position)
        {
            goToPosition = startPosition;
        }
        else
        {
            goToPosition = endPosition.position;
            deathColliderHolder.SetActive(false);
        }
    }
}
