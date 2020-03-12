using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//Main Author: Marcus Lundqvist

public class AffectedMovement : AffectedObject
{
    [SerializeField] private Vector3 startPosition = Vector3.zero;
    [SerializeField] private Transform endPosition = null;
    [SerializeField] private PhysicMaterial startMaterial, movementMaterial = null;

    private float lerpTime = 0;
    private float t = 0;
    private Vector3 goToPosition = Vector3.zero;
    private Vector3 goFromPosition = Vector3.zero;
    private Rigidbody rb = null;
    private Coroutine movement = null;
    private bool coroutineIsRunning = false;
    private BoxCollider[] boxes;

    /// <summary>
    /// Starts the coroutines
    /// </summary>
    public override void ExecuteAction()
    {
        if(coroutineIsRunning == true)
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
        goToPosition = endPosition.position;
        goFromPosition = startPosition;        
        boxes = rb.gameObject.GetComponents<BoxCollider>();
        
    }

    /// <summary>
    /// Lerps position of <see cref="Rigidbody"/> affected object between <see cref="goFromPosition"/> to <see cref="goToPosition"/>
    /// </summary>
    /// <returns></returns>
    private IEnumerator ChangePosition()
    {
        foreach(BoxCollider box in boxes)
        {
            box.material = movementMaterial;
        }
        coroutineIsRunning = true;
        while(lerpTime < actionDuration)
        {
            t += Time.deltaTime / actionDuration;
            //transform.position = Vector3.Lerp(goFromPosition, goToPosition, t);
            rb.MovePosition(Vector3.Lerp(goFromPosition, goToPosition, t));
            lerpTime += Time.deltaTime;
            yield return null;
        }

        SwapLocationValues();
        foreach(BoxCollider box in boxes)
        {
            box.material = startMaterial;
        }
        
        coroutineIsRunning = false;

    }

    /// <summary>
    /// Changes the value of <see cref="goFromPosition"/> and <see cref="goToPosition"/>
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
        }
    }

    
}
