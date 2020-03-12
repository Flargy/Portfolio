using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTrail : MonoBehaviour {

    public int moveSpeed = 230;
    public Transform target;
    public float timeToLive;

    private Rigidbody2D rgbd;

    private void Start()
    {
        rgbd = this.gameObject.GetComponent<Rigidbody2D>();
        Shoot();
    }

    
    private void Shoot()
    {
        rgbd.velocity = (target.transform.position - transform.position).normalized * moveSpeed;
        DestroyObject(this.gameObject, timeToLive);
    }
}
