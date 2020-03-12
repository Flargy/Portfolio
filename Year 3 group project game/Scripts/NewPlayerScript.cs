using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

//Main Author: Marcus Lundqvist

public class NewPlayerScript : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayer = 1;
    [SerializeField] private LayerMask interactionLayer = 1;
    [SerializeField] private LayerMask wallLayer = 1;
    [SerializeField] private float moveSpeed = 2.5f;
    [SerializeField] private float jumpPower = 5.0f;
    [SerializeField] private GameObject mainCam = null;
    [SerializeField] private GameObject dropShadow = null;
    [SerializeField] private float jumpVelocityClampValue = 7.0f;
    [SerializeField] Collider[] colliders;
    [SerializeField] private FollowPlayer followPoint = null;

    private Rigidbody rb = null;
    private Vector2 movementVector = Vector2.zero;
    private bool interacting = false;
    private bool CarryingAObject = false;
    private bool airBorne = false;
    private bool crouching = false;
    private bool usingScreenNorth = false;
    private bool canPickUp = true;
    private GameObject carriedObject;
    private float jumpGroundCheckDelay = 0.0f;
    private bool isLifted = false;
    private bool canBeLifted = true;
    private Interactable interactScript = null;
    private CapsuleCollider capsule = null;
    private Vector3 capsuleTop = Vector3.zero;
    private Vector3 capsuleBottom = Vector3.zero;
    private Vector3 lookDirection = Vector3.zero;
    private Vector3 faceDirection = Vector3.zero;
    private Vector3 rotationVector = Vector3.zero;
    private Vector3 respawnPoint = Vector3.zero;
    private Animator anim = null;
    private float groundCheckDelay = 0.0f;
    private GameObject dropShadowInstance = null;
    private float latestDropshadowDepth = 0.0f;

    private PlayerInput playerInput = null;
   // private MenuInputs menuInputs = null;
    private PlayerSounds playerSounds;

    void Start()
    {
        capsule = GetComponent<CapsuleCollider>();
        rb = GetComponent<Rigidbody>();
        interactScript = GetComponent<Interactable>();
        anim = GetComponent<Animator>();
        playerSounds = GetComponent<PlayerSounds>();
        respawnPoint = transform.position;
        playerInput = GetComponent<PlayerInput>();
        //menuInputs = GetComponent<MenuInputs>();

    }


    public bool UsingScreenNorth => usingScreenNorth;
    void Update()
    {
        DrawDropShadow();

        if (airBorne == false && interacting == false && isLifted == false && crouching == false)
        {
            ApplyGroundVelocity();
        }
        else if (airBorne == true && isLifted == false)
        {
            jumpGroundCheckDelay += Time.deltaTime;
            ApplyAirVelocity();
            if (GroundCheck() == true && jumpGroundCheckDelay >= 0.3f)
            {
                airBorne = false;
                if(followPoint != null)
                {
                    followPoint.StartFollowingPlayer();
                }
                //Destroy(dropShadowInstance);
                //dropShadowInstance = null;
                jumpGroundCheckDelay = 0.0f;
            }
        }
        else if (isLifted == true)
        {
            groundCheckDelay += Time.deltaTime;
            if (groundCheckDelay >= 0.5f)
            {
                if (GroundCheck() == true)
                {
                    isLifted = false;
                    groundCheckDelay = 0.0f;
                }
            }
        }

        FaceTowardsDirection();

    }

    private void DrawDropShadow()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, Vector3.down, out hit, 8.0f, groundLayer))
        {
            if(dropShadowInstance != null)
            {
                dropShadowInstance.transform.position = hit.point + Vector3.up * 0.005f;
            }
            else if(dropShadowInstance == null)
            {
                dropShadowInstance = Instantiate(dropShadow, hit.point + Vector3.up * 0.005f, Quaternion.Euler(90f,0f,0f));

            }
            latestDropshadowDepth = hit.point.y;
        }
        else
        {
            if(dropShadowInstance != null)
            {
                if(Mathf.Abs(latestDropshadowDepth - transform.position.y) < 0.1f)
                {
                    latestDropshadowDepth = transform.position.y - 0.2f;
                }
                dropShadowInstance.transform.position = new Vector3(transform.position.x, latestDropshadowDepth, transform.position.z) + Vector3.up * 0.05f;
            }
        }
    }

    private void FaceTowardsDirection()
    {
        if (movementVector.magnitude >= 0.01f && airBorne == false)
        {
            if (usingScreenNorth == false)
            {
                lookDirection = new Vector3(movementVector.x, 0, movementVector.y);
            }
            else
            {
                lookDirection = Quaternion.Euler(0, mainCam.transform.rotation.eulerAngles.y, 0) * new Vector3(movementVector.x, 0, movementVector.y);
            }
            faceDirection += lookDirection.normalized * Time.deltaTime * 10;
            if (faceDirection.magnitude > 1)
            {
                faceDirection = faceDirection.normalized;
            }
            transform.LookAt(transform.position + faceDirection);


        }
        //else if(rotationVector.magnitude >= 0.01f && airBorne == false)
        //{
        //    lookDirection = new Vector3(rotationVector.x, 0, rotationVector.y);
        //    faceDirection += lookDirection.normalized * Time.deltaTime * 6;
        //    if (faceDirection.magnitude > 1)
        //    {
        //        faceDirection = faceDirection.normalized;
        //    }
        //    transform.LookAt(transform.position + faceDirection);
        //}

        else if (rotationVector.magnitude >= 0.8f && airBorne == false)
        {
            float xValue = rotationVector.x;
            if (usingScreenNorth == false)
            {
                lookDirection = new Vector3(rotationVector.x, 0, rotationVector.y);
            }
            else
            {
                lookDirection = Quaternion.Euler(0, mainCam.transform.rotation.eulerAngles.y, 0) * new Vector3(rotationVector.x, 0, rotationVector.y);
            }
            faceDirection += lookDirection.normalized * Time.deltaTime * 6;
            if (faceDirection.magnitude > 1)
            {
                faceDirection = faceDirection.normalized;
            }
            transform.LookAt(transform.position + faceDirection);

            //if (xValue > 0.01)
            //{
            //    transform.rotation = transform.rotation * Quaternion.Euler(0, 3 * xValue, 0);
            //}
            //else if (xValue < -0.01)
            //{
            //    transform.rotation = transform.rotation * Quaternion.Euler(0, 2.3f * xValue, 0);
            //}
        }

        else if (movementVector.magnitude >= 0.01f && airBorne == true)
        {
            if (usingScreenNorth == false)
            {
                lookDirection = new Vector3(movementVector.x, 0, movementVector.y);
            }
            else
            {
                lookDirection = Quaternion.Euler(0, mainCam.transform.rotation.eulerAngles.y, 0) * new Vector3(movementVector.x, 0, movementVector.y);
            }
            faceDirection += lookDirection.normalized * Time.deltaTime * 6;
            if (faceDirection.magnitude > 1)
            {
                faceDirection = faceDirection.normalized;
            }
            transform.LookAt(transform.position + faceDirection);
        }




    }

    public void OnInteract()
    {
        if (isLifted == false && crouching == false && airBorne == false && canPickUp)
        {
            RaycastHit ray;
            if (CarryingAObject == false)
            {
                capsuleTop = transform.position - (transform.forward * 0.1f) + (capsule.center + Vector3.up * (capsule.height / 2 - capsule.radius));
                capsuleBottom = transform.position - (transform.forward * 0.1f) + (capsule.center + Vector3.down * (capsule.height / 2 - capsule.radius));
                if (Physics.CapsuleCast(capsuleTop, capsuleBottom, capsule.radius, transform.forward, out ray, 2, interactionLayer))
                {

                    Interactable interactionObject = ray.collider.GetComponent<Interactable>();
                    interactionObject.DistanceCheck(gameObject);
                    StartCoroutine(PickupDelay());

                }
            }

            else if (CarryingAObject == true && carriedObject != null == airBorne == false)
            {
                if (Physics.Raycast(transform.position, transform.forward, 0.8f, wallLayer) == false)
                {
                    carriedObject.GetComponent<Interactable>().Interact(gameObject);
                }
            }
        }

    }

    public void PickUpObject(GameObject carried, float freezeTime)
    {
        if(carried.tag == "Player")//Eku
        {
            anim.SetTrigger("LiftPlayer");
            anim.SetBool("isLiftingObject", true);

        }
        else 
        {
            anim.SetTrigger("LiftObject");//Eku
            anim.SetBool("isLiftingObject", true); 
        }//Eku

        playerSounds.PlayPickup();

        var crateSound = carried.GetComponent<CrateSound>();
        if(crateSound != null && crateSound.howToPlaySound == CrateSound.HowToPlaySound.Pickup)
            crateSound.shouldPlaySound = true;

        canBeLifted = false;
        carriedObject = carried;
        CarryingAObject = true;
        StartCoroutine(FreezePlayer(freezeTime));
    }

    public void DropObject()
    {
        anim.SetBool("isLiftingObject", false);//Eku
        canBeLifted = true;
        carriedObject = null;
        CarryingAObject = false;
        StartCoroutine(PickupDelay());
    }

    public void ApplyGroundVelocity()
    {
        if (movementVector.magnitude > 0.05f)
        {
            if (usingScreenNorth == false)
            {
                rb.velocity = (new Vector3(movementVector.x, 0, movementVector.y) * moveSpeed) + new Vector3(0, rb.velocity.y, 0);
            }
            else
            {
                //rb.velocity = Vector3.ProjectOnPlane(mainCam.transform.rotation * (new Vector3(movementVector.x, 0, movementVector.y) * moveSpeed) + new Vector3(0, rb.velocity.y, 0), Vector3.up);
                rb.velocity = Quaternion.Euler(0, mainCam.transform.rotation.eulerAngles.y, 0) * (new Vector3(movementVector.x, 0, movementVector.y) * moveSpeed) + new Vector3(0, rb.velocity.y, 0);
            }
            if (new Vector3(rb.velocity.x, 0, rb.velocity.z).magnitude >= moveSpeed)
            {
                float yVelocity = rb.velocity.y;
                rb.velocity = Vector3.ClampMagnitude(rb.velocity, moveSpeed);
                rb.velocity = new Vector3(rb.velocity.x, yVelocity, rb.velocity.z);
            }

        }
    }

    public bool GroundCheck()
    {
        capsuleTop = transform.position + capsule.center + Vector3.up * (capsule.height / 2.1f - capsule.radius);
        capsuleBottom = transform.position + capsule.center + Vector3.down * (capsule.height / 2.1f - capsule.radius);
        if (Physics.CapsuleCast(capsuleTop, capsuleBottom, capsule.radius, Vector3.down, 0.15f, groundLayer))
        {
            return true;
        }
        return false;
    }

    public void ApplyAirVelocity()
    {
        //Vector2 currentVelocity = new Vector2(rb.velocity.x, rb.velocity.z);
        //float turndot = Vector2.Dot(currentVelocity.normalized, movementVector.normalized);
        //Debug.Log(turndot);
        //if (turndot < 0.9 && movementVector.magnitude > 0.2f)
        //{
        ////    //rb.velocity += (new Vector3(movementVector.x, 0, movementVector.y) * Time.deltaTime);
        //    float yVelocity = rb.velocity.y;
        //    Vector2 horizontal = new Vector2(rb.velocity.x, rb.velocity.z);
        //    horizontal = Vector2.ClampMagnitude(horizontal, 2.5f) * 0.9f;
        //    rb.velocity = new Vector3(horizontal.x, yVelocity, horizontal.y);

        //}
        float yVelocity = rb.velocity.y;
        Vector2 noGravityVector = new Vector2(rb.velocity.x, rb.velocity.z);

        if (movementVector.magnitude >= 0.1f)
        {
            Vector3 noY = (new Vector3(movementVector.x, 0, movementVector.y) * moveSpeed);
            rb.velocity = Vector3.ClampMagnitude(noY, moveSpeed) + new Vector3(0, rb.velocity.y, 0);
            if (usingScreenNorth == false)
            {
                rb.velocity = new Vector3(rb.velocity.x, yVelocity, rb.velocity.z);
            }
            else
            {
                rb.velocity = Quaternion.Euler(0, mainCam.transform.rotation.eulerAngles.y, 0) * new Vector3(rb.velocity.x, yVelocity, rb.velocity.z);
            }
        }
        else if (movementVector.magnitude < 0.1f && noGravityVector.magnitude > 0.0f)
        {
            //noGravityVector += -noGravityVector * Time.deltaTime;
            rb.velocity = new Vector3(noGravityVector.x, yVelocity, noGravityVector.y);
        }
        //if (rb.velocity.y < 0)
        //{
        //    rb.velocity += Vector3.up * Physics.gravity.y * (0.5f) * Time.deltaTime;
        //}


    }

    public void OnMove(InputValue value)
    {
        if (isLifted == false && interacting == false && crouching == false)
        {
            movementVector = value.Get<Vector2>();
            anim.SetFloat("Moving", movementVector.normalized.magnitude);
        }
        else
        {
            anim.SetFloat("Moving", 0f);
        }
    }

    public void OnJump()
    {
        if (GroundCheck() == true && interacting == false && isLifted == false && CarryingAObject == false && crouching == false && airBorne == false)
        {
            Vector3 clampedY = new Vector3(0, jumpPower, 1.0f);
            clampedY = Vector3.ClampMagnitude(clampedY, jumpVelocityClampValue);
            rb.velocity = new Vector3(rb.velocity.x, clampedY.y, rb.velocity.z);
            rb.velocity += transform.forward * 0.5f;

            airBorne = true;
            if(followPoint != null)
            {
                followPoint.StopFollowingPlayer();
            }
            anim.SetTrigger("isJumping");
        }
    }

    public void OnToss()
    {
        if (CarryingAObject == true && carriedObject != null)
        {
            anim.SetTrigger("Tossing");//Eku
            carriedObject.GetComponent<Interactable>().Toss();
            anim.SetBool("isLiftingObject", false);//Eku
        }
    }

    public void OnCrouch()
    {
        Crouch();
        RaycastHit plateHit;
        capsuleTop = transform.position + capsule.center + Vector3.up * (capsule.height / 2.1f - capsule.radius);
        capsuleBottom = transform.position + capsule.center + Vector3.down * (capsule.height / 2.5f - capsule.radius);
        if (Physics.CapsuleCast(capsuleTop, capsuleBottom, capsule.radius, Vector3.down, out plateHit, 0.15f, LayerMask.GetMask("PressurePlate")))
        {
            plateHit.collider.GetComponent<PressurePlate>().LowerCounter();
        }
    }

    private void Crouch()
    {
        if (CarryingAObject == false && canBeLifted == true && airBorne == false && interacting == false && crouching == false && isLifted == false && airBorne == false)
        {
            capsule.enabled = false;
            foreach (Collider col in colliders)
            {
                col.enabled = !col.enabled;
            }
            crouching = true;
            movementVector = Vector3.zero;
            anim.SetBool("isCrouching", true);
        }
        else if (crouching == true && isLifted == false)
        {
            capsule.enabled = true;
            foreach (Collider col in colliders)
            {
                col.enabled = !col.enabled;
            }
            crouching = false;
            anim.SetBool("isCrouching", false);
            //if(Physics.Raycast(transform.position + Vector3.up * 0.5f, Vector3.down, out plateHit, 1f, LayerMask.GetMask("PressurePlate")))
            //{
            //    plateHit.collider.GetComponent<PressurePlate>().LowerCounter();
            //}

            
        }
    }

    public void OnRotate(InputValue value)
    {
        rotationVector = value.Get<Vector2>();
    }


    //Player to player interactions

    public bool CanBeLifted()
    {
        return canBeLifted;
    }

    public void SwapLiftingState()
    {
        canBeLifted = !canBeLifted;
    }

    public void BecomeLifted()
    {
        
        isLifted = true;
        anim.SetBool("isCarried", true);//Eku
        movementVector = Vector2.zero;
        anim.SetFloat("Moving", 0.0f);
    }

    public void Released()
    {
        //isLifted = false;
        anim.SetBool("isCarried", false);//Eku
        airBorne = true;
        if (crouching == true)
        {
            capsule.enabled = true;
            foreach (Collider col in colliders)
            {
                col.enabled = false;
            }
            crouching = false;
        }

    }


    public void ChangeSpawnPoint(Vector3 respawnposition)
    {
        respawnPoint = respawnposition;
    }

    public void Respawn()
    {
        if(CarryingAObject == true && carriedObject != null)
        {
            carriedObject.GetComponent<Interactable>().Interact(gameObject);
        }
        rb.velocity = Vector3.zero;
        transform.position = respawnPoint;
    }

    public void StartAnimation(string triggerName)
    {
        anim.SetTrigger(triggerName);
    }

    public void Freeze(float freezeDuration)
    {
        StartCoroutine(FreezePlayer(freezeDuration));
    }

    public IEnumerator FreezePlayer(float freezeTime)
    {
        interacting = true;
        movementVector = Vector3.zero;
        yield return new WaitForSeconds(freezeTime);
        interacting = false;
    }

    private IEnumerator PickupDelay()
    {
        canPickUp = false;
        yield return new WaitForSeconds(1.5f);
        canPickUp = true;
    }

    public void OnPause()
    {

        playerInput.SwitchCurrentActionMap("Menu");
        NewPauseMenu.Instance.OnStartFromNewPlayerScript(this);
        //menuInputs.OnStart();
    }

    public void SwapToGameplayAM()
    {
         playerInput.SwitchCurrentActionMap("Gameplay");

    }

    public void SwapToPaused()
    {
        playerInput.SwitchCurrentActionMap("Pause");
    }

    public void SwapTrueNorth()
    {
        usingScreenNorth = !usingScreenNorth;   
    }

    public void SetTrueNorth(bool value)
    {
        usingScreenNorth = value;
    }

    public bool GetNorth()
    {
        return UsingScreenNorth;
    }
}
