using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnemy : MonoBehaviour {

    Animator anim;


    private void Start()
    {

        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update () {
/*
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");


        anim.SetFloat("Speed", v);
        anim.SetFloat("Speed", h);
*/

    }
}
