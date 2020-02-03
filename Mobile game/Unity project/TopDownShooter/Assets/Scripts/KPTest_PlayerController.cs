using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KPTest_PlayerController : MonoBehaviour {

    public float moveForce = 5;
    public float speed = 1.0f;
    public KeyCode pressUp;
    public KeyCode pressDown;
    public KeyCode pressLeft;
    public KeyCode pressRight;
    Animator anim;

    Rigidbody2D rgbd;

    void Start()
    {

        rgbd = this.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();

    }

    //test
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        var move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
       transform.position += move * speed * Time.deltaTime;
        if (Input.GetKeyDown(pressUp) || Input.GetKey(pressUp))
        {
            GetComponent<Transform>().eulerAngles = new Vector3(0, 0, 180);
            anim.SetFloat("Speed", 1);
        }
        else if (Input.GetKeyDown(pressDown) || Input.GetKey(pressDown)) 
        {
            GetComponent<Transform>().eulerAngles = new Vector3(0, 0, 0);
            anim.SetFloat("Speed", 1);
        }
        else if (Input.GetKeyDown(pressLeft) || Input.GetKey(pressLeft))
        {
            GetComponent<Transform>().eulerAngles = new Vector3(0, 0, -90);
            anim.SetFloat("Speed", 1);
        }
        else if (Input.GetKeyDown(pressRight) || Input.GetKey(pressRight))
        {
            GetComponent<Transform>().eulerAngles = new Vector3(0, 0, 90);
            anim.SetFloat("Speed", 1);
        }
        else
        {
            anim.SetFloat("Speed", 0);
        }



    }



    /*
    void FixedUpdate () {
    #if UNITY_STANDALONE_WIN

            float h = Input.GetAxisRaw("Horizontal") * moveForce;
            float v = Input.GetAxisRaw("Vertical") * moveForce;

            rgbd.AddForce(new Vector2(h, v));
    #endif
    #if UNITY_ANDROID

            Vector2 moveVec = new Vector2(CrossPlatformInputManager.GetAxis("Horizontal"),
                 CrossPlatformInputManager.GetAxis("Vertical")) * moveForce;

             Vector3 lookVec = new Vector3(CrossPlatformInputManager.GetAxis("Horizontal_2"),
                 CrossPlatformInputManager.GetAxis("Vertical_2"), 4000);

             if(lookVec.x != 0 && lookVec.y != 0)
                 transform.rotation = Quaternion.LookRotation(lookVec, Vector3.back);

             rgbd.AddForce(moveVec);
    #endif


        }

    */
}