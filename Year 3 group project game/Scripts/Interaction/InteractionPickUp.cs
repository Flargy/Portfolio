using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Main Author: Marcus Lundqvist

public class InteractionPickUp : Interactable
{
    [SerializeField] private float horizontalYeetForce = 300.0f;
    [SerializeField] private float verticalYeetForce = 40.0f;
    [SerializeField] private Vector3 offsetVector = Vector3.zero;
    [SerializeField] private Transform respawnPoint = null;
    [SerializeField] private bool showTrajectory = false;
    [SerializeField] private float animationDuration = 0.0f;
    [SerializeField] private float pickupAnimationDelay = 1.0f;

    private Rigidbody rb;
    private RenderPath rp;
    private bool isPickedUp;
    private GameObject currentHolder;
    private BoxCollider[] colliders;
    private float t = 0;
    private float lerpTime = 0;
    private Vector3 goToPosition = Vector3.zero;
    private Vector3 goFromPosition = Vector3.zero;

    /// <summary>
    /// Sets starting values to variables
    /// </summary>
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        colliders = GetComponentsInChildren<BoxCollider>();
        if(showTrajectory == true)
        {
            rp = GetComponent<RenderPath>();
        }
        
    }

    /// <summary>
    /// Moves the <see cref="Rigidbody"/> affected object towards the desired position.
    /// </summary>
    private void Update()
    {
        if (isPickedUp == true && interacting == true)
        {
            rb.MovePosition(currentHolder.transform.position + currentHolder.transform.localRotation * offsetVector);
            transform.rotation = currentHolder.transform.rotation;
            
        }
    }

    /// <summary>
    /// Sets values for being picked up and starts coroutines to change position.
    /// Sends a raycast downwards to lower the count of possible pressure plates under it.
    /// </summary>
    /// <param name="player"></param>
    public override void Interact(GameObject player)
    {
        if(interacting == false)
        {
            
            interacting = true;
            currentHolder = player;
            rb.velocity = Vector3.zero;
            rb.useGravity = false;
            rb.constraints = RigidbodyConstraints.FreezeRotation;
            //transform.rotation = Quaternion.Euler(Vector3.zero);
            //transform.position += Vector3.up;
            if (CompareTag("CarryBox"))
            {
                RaycastHit plateHit;
                //if (Physics.Raycast(transform.position + Vector3.up * 0.5f, Vector3.down, out plateHit, 1f, LayerMask.GetMask("PressurePlate")))
                if (Physics.BoxCast(transform.position + Vector3.up * 0.6f, colliders[0].size / 2 + new Vector3(0.15f, 0.15f, 0.15f), Vector3.down, out plateHit, Quaternion.identity, 0.45f, LayerMask.GetMask("PressurePlate")))
                {
                    plateHit.collider.GetComponent<PressurePlate>().LowerCounter();
                }
            }
            StartCoroutine(PickupDelay());

            //GetComponent<BoxCollider>().enabled = false;
            //GetComponentInChildren<BoxCollider>().enabled = false;
            foreach(BoxCollider box in colliders)
            {
                box.enabled = false;
            }
            player.GetComponent<NewPlayerScript>().PickUpObject(gameObject, animationDuration);
            

            

        }
        else if(player == currentHolder)
        {
            Drop();
            player.GetComponent<NewPlayerScript>().DropObject();
        }
    }

    /// <summary>
    /// Resets the values on the picked up object and places it in front of the player.
    /// </summary>
    public void Drop()
    {
        currentHolder.GetComponent<NewPlayerScript>().DropObject();
        transform.position = currentHolder.transform.position + currentHolder.transform.forward * 0.5f + currentHolder.transform.up * 0.6f;
        currentHolder = null;
        isPickedUp = false;
        interacting = false;
        rb.useGravity = true;
        rb.constraints = RigidbodyConstraints.None;
        //GetComponent<BoxCollider>().enabled = true;
        //GetComponentInChildren<BoxCollider>().enabled = true;
        foreach (BoxCollider box in colliders)
        {
            box.enabled = true;
        }
        HideLine();
    }


    /// <summary>
    /// Resets the values of the picked up object and applies a force similar to a throw to it.
    /// </summary>
    public override void Toss()
    {
        currentHolder.GetComponent<NewPlayerScript>().DropObject();
        rb.AddForce((currentHolder.transform.rotation * Vector3.forward * horizontalYeetForce) + (Vector3.up * verticalYeetForce));
        isPickedUp = false;
        interacting = false;
        rb.useGravity = true;
        currentHolder = null;
        rb.constraints = RigidbodyConstraints.None;
        //GetComponent<BoxCollider>().enabled = true;
        //GetComponentInChildren<BoxCollider>().enabled = true;
        foreach (BoxCollider box in colliders)
        {
            box.enabled = true;
        }
        HideLine();

    }

    /// <summary>
    /// Teleports the object to a specific position.
    /// </summary>
    public override void Teleport()
    {
        if(currentHolder != null)
        {
            currentHolder.GetComponent<NewPlayerScript>().DropObject();
            currentHolder = null;

        }
            isPickedUp = false;
        rb.useGravity = true;
        rb.constraints = RigidbodyConstraints.None;


    }

    /// <summary>
    /// Moves the box to it's designed respawn position.
    /// </summary>
    public void Respawn()
    {
        if(respawnPoint != null)
        { 
            rb.velocity = Vector3.zero;
            transform.position = respawnPoint.position;
            transform.rotation = respawnPoint.rotation * Quaternion.Euler(0, 90, 0);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ChangeRespawn(Transform newSpawnPos)
    {
        respawnPoint = newSpawnPos;
    }

    /// <summary>
    /// Deactivates the <see cref="LineRenderer"/> on the object if one is attached.
    /// </summary>
    private void HideLine()
    {
        if (showTrajectory == true)
        {
            rp.HidePath();
        }
    }

    /// <summary>
    /// Sets a delay before the pickup is activated to better match animations
    /// </summary>
    /// <returns></returns>
    private IEnumerator PickupDelay()
    {
        yield return new WaitForSeconds(pickupAnimationDelay);
        
        
        StartCoroutine(RaisePosition());
    }

    /// <summary>
    /// Lerps the position of the <see cref="Rigidbody"/> affected object to simulate pickup up the object
    /// </summary>
    /// <returns></returns>
    private IEnumerator RaisePosition()
    {
        HideInteraction();
        goFromPosition = rb.transform.position;
        goToPosition = currentHolder.transform.position + (currentHolder.transform.localRotation * offsetVector) + currentHolder.transform.forward * 0.3f;

        while (lerpTime < 0.8f)
        {
            t += Time.deltaTime / 0.8f;
            rb.MovePosition(Vector3.Lerp(goFromPosition, goToPosition, t));
            lerpTime += Time.deltaTime;
            yield return null;
        }
        t = 0;
        lerpTime = 0;
        goFromPosition = rb.transform.position;
        goToPosition = currentHolder.transform.position + (currentHolder.transform.localRotation * offsetVector);

        while (lerpTime < 0.3f)
        {
            t += Time.deltaTime / 0.3f;
            rb.MovePosition(Vector3.Lerp(goFromPosition, goToPosition, t));
            lerpTime += Time.deltaTime;
            yield return null;
        }
        t = 0;
        lerpTime = 0;
        isPickedUp = true;

        if (showTrajectory == true)
        {
            rp.ShowPath();
        }
    }
}
