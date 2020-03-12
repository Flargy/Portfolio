using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//Main Author: Marcus Lundqvist

public class PlayerQTE : MonoBehaviour
{
    private InteractionLever currentLever = null;
    private PlayerInput playerInput = null;
    private Vector3 moveToPosition = Vector3.zero;
    private Vector3 moveFromPosition = Vector3.zero;
    private float t = 0;
    private float lerpTime = 0;
    private Rigidbody rb = null;
    private Vector3 targetRotation = Vector3.zero;
    private Quaternion fromRotation = Quaternion.identity;
    private Quaternion toRotation = Quaternion.identity;

    /// <summary>
    /// Sets values of variables on start.
    /// </summary>
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// Swaps the players <see cref="PlayerInput.currentActionMap"/> to QTE and sets values of variables.
    /// </summary>
    /// <param name="lever">The script attached to the lever.</param>
    /// <param name="leverPosition">The gameobject which the script is attached to.</param>
    public void SwapToQTE(InteractionLever lever, GameObject leverPosition)
    {
        currentLever = lever;
        playerInput.SwitchCurrentActionMap("QTE");


        Vector3 placement = new Vector3(leverPosition.transform.position.x, transform.position.y, leverPosition.transform.position.z);
        moveToPosition = placement + leverPosition.transform.forward * 0.4f + -leverPosition.transform.right * 0.5f;
        targetRotation = (placement + leverPosition.transform.forward * 0.4f) - moveToPosition;
        fromRotation = transform.rotation;
        moveFromPosition = rb.transform.position;
        toRotation = Quaternion.LookRotation(targetRotation, Vector3.up);
        StartCoroutine(PlaceAndRotate());
    }

    /// <summary>
    /// Changes the players <see cref="PlayerInput.currentActionMap"/> back to Gameplay.
    /// </summary>
    public void SwapToMovement()
    {
        playerInput.SwitchCurrentActionMap("Gameplay");
    }

    /// <summary>
    /// Sends value to <see cref="InteractionLever.ReceiveAnswer(int)"/>
    /// </summary>
    public void OnDown()
    {
        currentLever.ReceiveAnswer(0);
    }

    /// <summary>
    /// Sends value to <see cref="InteractionLever.ReceiveAnswer(int)"/>
    /// </summary>
    public void OnLeft()
    {
        currentLever.ReceiveAnswer(1);
    }

    /// <summary>
    /// Sends value to <see cref="InteractionLever.ReceiveAnswer(int)"/>
    /// </summary>
    public void OnUp()
    {
        currentLever.ReceiveAnswer(2);
    }

    /// <summary>
    /// Sends value to <see cref="InteractionLever.ReceiveAnswer(int)"/>
    /// </summary>
    public void OnRight()
    {
        currentLever.ReceiveAnswer(3);
    }

    /// <summary>
    /// Rotates the player towards the lever and places them in the correct position to match animations.
    /// </summary>
    /// <returns></returns>
    private IEnumerator PlaceAndRotate()
    {
        while (lerpTime < 0.7f)
        {
            t += Time.deltaTime / 0.7f;
            rb.MovePosition(Vector3.Lerp(moveFromPosition, moveToPosition, t));
            rb.MoveRotation(Quaternion.Lerp(fromRotation, toRotation, t));
            lerpTime += Time.deltaTime;
            yield return null;
        }
        t = 0;
        lerpTime = 0;
    }
}
