using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Main Author: Marcus Lundqvist

public class AffectedLibraryPlatforms : AffectedObject
{
    [SerializeField] private Vector3 startPosition = Vector3.zero;
    [SerializeField] private Transform endPosition = null;
    [SerializeField] private PhysicMaterial startMaterial, movementMaterial = null;
    [SerializeField] private bool willUpdateStartPosition = false;

    private float lerpTime = 0;
    private float t = 0;
    private Vector3 goToPosition = Vector3.zero;
    private Vector3 goFromPosition = Vector3.zero;
    private Vector3 endPos = Vector3.zero;
    private Rigidbody rb = null;
    private Coroutine movement = null;
    private bool coroutineIsRunning = false;
    private bool activatedFirstTime = false;
    private BoxCollider[] boxes;

    /// <summary>
    /// Activates coroutine to move objects.
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
    /// Sets starting values
    /// </summary>
    private void Start()
    {
        rb = GetComponentInChildren<Rigidbody>();
        startPosition = rb.transform.position;
        endPos = endPosition.position;
        goToPosition = endPos;
        goFromPosition = startPosition;
        boxes = rb.gameObject.GetComponents<BoxCollider>();

    }

    /// <summary>
    /// Changes the position of the <see cref="Rigidbody"/> affected object
    /// </summary>
    /// <returns></returns>
    private IEnumerator ChangePosition()
    {
        if(activatedFirstTime == false && willUpdateStartPosition == true && startPosition != rb.transform.position)
        {
            startPosition = rb.transform.position;
            activatedFirstTime = true;
        }
        goFromPosition = transform.position;
        foreach (BoxCollider box in boxes)
        {
            box.material = movementMaterial;
        }
        coroutineIsRunning = true;
        while (lerpTime < actionDuration)
        {
            t += Time.deltaTime / actionDuration;
            //transform.position = Vector3.Lerp(goFromPosition, goToPosition, t);
            rb.MovePosition(Vector3.Lerp(goFromPosition, goToPosition, t));
            lerpTime += Time.deltaTime;
            yield return null;
        }

        SwapLocationValues();
        foreach (BoxCollider box in boxes)
        {
            box.material = startMaterial;
        }

        coroutineIsRunning = false;

    }

    /// <summary>
    /// Changes the <see cref="goFromPosition"/> and <see cref="goToPosition"/>
    /// </summary>
    private void SwapLocationValues()
    {
        t = 0.0f;
        lerpTime = 0.0f;
        goFromPosition = rb.transform.position;
        if (goToPosition == endPos)
        {
            goToPosition = startPosition;
        }
        else
        {
            goToPosition = endPos;
        }
    }
}