using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;


public class PlayerController : MonoBehaviour
{
    private static PlayerController control;
    private float moveForce = 45;
    Rigidbody2D rgbd;
    private Animator anim;
    private float speedPU = 0;

    private float currencyValueMultiplier;
    public static bool primaryShooting;
    public bool secondaryShooting;


    private void Awake()
    {
        /*if (control == null)
        {
            DontDestroyOnLoad(gameObject);
            control = this;
        }
        else if(control != null)
        {
            Destroy(gameObject);
        }*/

    }

    void Start()
    {

        rgbd = this.GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

    }

    public void increaseMovement()
    {
        StartCoroutine(movementPowerUp());
    }
    public IEnumerator movementPowerUp()
    {
        speedPU += 20f;
        Debug.Log(speedPU);
        yield return new WaitForSeconds(6f);
        speedPU -= 20;
        Debug.Log(speedPU);
    }

    void FixedUpdate()
    {
#if UNITY_STANDALONE_WIN

        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 dir = Input.mousePosition - pos;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        angle = angle - 90;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);


        float h = Input.GetAxisRaw("Horizontal") * (moveForce + speedPU);
        float v = Input.GetAxisRaw("Vertical") * (moveForce + speedPU);
        rgbd.AddForce(new Vector2(h, v));
    }
}
#endif
#if UNITY_ANDROID

        Vector2 moveVec = new Vector2(CrossPlatformInputManager.GetAxis("Horizontal"),
             CrossPlatformInputManager.GetAxis("Vertical")) * (moveForce + speedPU);

        Vector3 primaryLookVec = new Vector3(CrossPlatformInputManager.GetAxis("Horizontal_2"),
            CrossPlatformInputManager.GetAxis("Vertical_2"), 4000);

        Vector3 secondaryLookVec = new Vector3(CrossPlatformInputManager.GetAxis("Horizontal"),
             CrossPlatformInputManager.GetAxis("Vertical"), 4000);


        primaryShooting = primaryLookVec.x != 0 && primaryLookVec.y != 0;
        secondaryShooting = secondaryLookVec.x != 0 && secondaryLookVec.y != 0;

        if (primaryShooting)
        {
            transform.rotation = Quaternion.LookRotation(primaryLookVec, Vector3.back);
            moveForce = 25;
        }
        else if (secondaryShooting)
        {
            transform.rotation = Quaternion.LookRotation(secondaryLookVec, Vector3.back);
            moveForce = 45;
        }

        float speed = CrossPlatformInputManager.GetAxis("Horizontal") + CrossPlatformInputManager.GetAxis("Vertical");
        if (speed < 0)
            speed *= -1;
        anim.SetFloat("speed", speed);


        rgbd.AddForce(moveVec);

    }
}
#endif


