using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//Main Author: Marcus Lundqvist

public class InteractionPlayer : Interactable
{
    [SerializeField] private GameObject otherPlayer = null;
    [SerializeField] private float horizontalYeetForce = 300.0f;
    [SerializeField] private float verticalYeetForce = 40.0f;
    [SerializeField] private Vector3 offsetVector = Vector3.zero;
    [SerializeField] private float inputPickupDelay = 3.0f;
    [SerializeField] private float animationDuration = 1.0f;

    private NewPlayerScript thisPlayer;
    
    private Rigidbody rb = null;
    private bool isLifted = false;
    private PlayerInput playerInput = null;
    private Coroutine breakFree = null;
    private bool noMovementAllowed = true;
    private RenderPath rp = null;
    private Collider[] colliders;//Eku

    /// <summary>
    /// Sets starting values to variables.
    /// </summary>
    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody>();
        colliders = GetComponents<Collider>();//Eku
        thisPlayer = GetComponent<NewPlayerScript>();
        rp = GetComponent<RenderPath>();
    }

    /// <summary>
    /// Moves the <see cref="Rigidbody"/> affected object towards the desired position.
    /// </summary>
    private void Update()
    {
        if (isLifted)
        {
            rb.MovePosition(otherPlayer.transform.position + otherPlayer.transform.localRotation * offsetVector);
            transform.rotation = otherPlayer.transform.rotation;
        }
    }

    /// <summary>
    /// Starts the player pickup interaction if <see cref="NewPlayerScript.CanBeLifted"/> function returns true.
    /// </summary>
    /// <param name="player"></param>
    public override void Interact(GameObject player)
    {
         if(thisPlayer.CanBeLifted() == true && isLifted == false)
        {
            breakFree = StartCoroutine(BreakFreeDelay());
            thisPlayer.BecomeLifted();
            rb.velocity = Vector3.zero;
            isLifted = true;
            rb.useGravity = false;
            rp.ShowPath();
            otherPlayer.GetComponent<NewPlayerScript>().PickUpObject(gameObject, animationDuration);
            playerInput.SwitchCurrentActionMap("BreakingFree");
        }
         else if(isLifted == true)
        {
            GetPutDown();
        }
    }

    /// <summary>
    /// Resets variable values and applies a force similar to that of a throw to the <see cref="Rigidbody"/> affected object.
    /// </summary>
    public override void Toss()
    {
        isLifted = false;
        rb.useGravity = true;
        thisPlayer.Released();
        rp.HidePath();
        otherPlayer.GetComponent<NewPlayerScript>().DropObject();
        rb.AddForce((otherPlayer.transform.rotation * Vector3.forward * horizontalYeetForce) + (Vector3.up * verticalYeetForce));
        //foreach (Collider col in colliders)
        //{
        //    col.enabled = true;//Eku
        //}
        playerInput.SwitchCurrentActionMap("Gameplay");
        noMovementAllowed = true;
    }

    /// <summary>
    /// Resets the values and releases the <see cref="Rigidbody"/> affected object.
    /// </summary>
    public void GetPutDown()
    {
        thisPlayer.Released();
        rp.HidePath();
        isLifted = false;
        rb.useGravity = true;
        otherPlayer.GetComponent<NewPlayerScript>().DropObject();
        //foreach (Collider col in colliders)
        //{
        //    col.enabled = true;//Eku
        //}
        playerInput.SwitchCurrentActionMap("Gameplay");
        noMovementAllowed = true;

    }

    /// <summary>
    /// Releases the <see cref="Rigidbody"/> affected object object if <see cref="noMovementAllowed"/> is equal to false.
    /// </summary>
    public void OnBreakFree()
    {
        if (noMovementAllowed == false)
        {
            GetPutDown();
            noMovementAllowed = true;
        }
    }

    /// <summary>
    /// Applies a delay before the player can attempt to release themselves from the hold.
    /// </summary>
    /// <returns></returns>
    private IEnumerator BreakFreeDelay()
    {
        yield return new WaitForSeconds(inputPickupDelay);
        noMovementAllowed = false;
    }
}
