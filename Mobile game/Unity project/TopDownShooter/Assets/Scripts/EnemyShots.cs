using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShots : MonoBehaviour {

    public int damage = 10;
    public LayerMask whatToHit;
    public int moveSpeed = 230;
    public float timeToLive;

    private Transform target;
    private Rigidbody2D rgbd;

    Transform firePoint;
    Transform firePointDirection;
    ContactPoint2D contact;


    private void Start()
    {
        target = GameObject.Find("hitman1_gun").transform;
        rgbd = this.gameObject.GetComponent<Rigidbody2D>();
        firePoint = transform.Find("FirePoint");
        firePointDirection = transform.Find("FirePointDirection");
        Shoot();

    }

    void FixedUpdate()
    {
        Vector2 fireTargetPosition = new Vector2(firePointDirection.position.x, firePointDirection.position.y);
        Vector2 firePointPosition = new Vector2(firePoint.position.x, firePoint.position.y);
        RaycastHit2D hit = Physics2D.Raycast(firePointPosition, fireTargetPosition, 0.3f, whatToHit);

        if (hit.collider != null)
        {
            //Debug.Log("enemy shot hit");
            if (hit.collider.gameObject.CompareTag("Walls"))
                DestroyObject(this.gameObject);
            else if (hit.collider.gameObject.CompareTag("Player"))
            {
                hit.collider.gameObject.GetComponent<PlayerVariables>().takeDamage(damage, transform);
                DestroyObject(this.gameObject);
            }
            
        }
    }


    private void Shoot()
    {
        rgbd.velocity = (target.position - transform.position).normalized * moveSpeed;
        Destroy(this.gameObject, timeToLive);
    }
}
