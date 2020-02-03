using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Seeker))]
public class RangedEnemyBehaviour : MonoBehaviour
{

    
    public float speed, updateRate = 2.0f, nextWaypointDistance = 0.1f;
    //public int health;
    public Path path;
    public ForceMode2D fMode;
    public Transform bullets;
   
    Transform firePoint;

    [HideInInspector]
    public bool pathIsEnded = false;

    private Seeker seeker;
    private Rigidbody2D rb;
    private float startSpeed, distance, hitCooldown;
    private int currentWaypoint = 0;
    private bool fire = false;
    private Transform playerTarget;
    private Animator anim;

    void Start()
    {

        MonsterSpawn.numberOfMonsters++;
        playerTarget = GameObject.Find("hitman1_gun").transform;
        startSpeed = speed;
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        firePoint = transform.Find("FirePoint");

        if (playerTarget == null)
        {
            Debug.LogError("No Player found!");
        }

        seeker.StartPath(transform.position, playerTarget.position, OnPathComplete);

        anim = GetComponent<Animator>();

        StartCoroutine(UpdatePath());
        StartCoroutine(ShootPlayer());


    }

    void Update()
    {

        anim.SetFloat("speed", speed);

        if (hitCooldown > 0)
            hitCooldown -= Time.deltaTime;

        distance = Vector3.Distance(transform.position, playerTarget.position);

        if (distance < 6)
        {
            speed = 0f;
            fire = true;

        }
        else
        {
            speed = startSpeed;
            fire = false;
        }

        var dir = playerTarget.position - transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (!anim.GetBool("death"))
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);



        //transform.position = Vector2.MoveTowards(transform.position, playerTarget.position, speed * Time.fixedDeltaTime);


    }

    public void OnPathComplete(Path p)
    {
        //Debug.Log("Got path, error? " + p.error);
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    IEnumerator ShootPlayer()
    {
        if (!anim.GetBool("death"))
        {
            if (fire == true)
            {
                anim.SetBool("shooting", true);

                yield return new WaitForSeconds(0.5f);

                Instantiate(bullets, firePoint.position, firePoint.rotation);

                anim.SetBool("shooting", false);

                yield return new WaitForSeconds(1.0f);

                StartCoroutine(ShootPlayer());

            }
            else
            {
                yield return new WaitForSeconds(0.5f);


                StartCoroutine(ShootPlayer());
            }
        }

    }

    IEnumerator UpdatePath()
    {

        seeker.StartPath(transform.position, playerTarget.position, OnPathComplete);

        yield return new WaitForSeconds(1.0f / updateRate);
        StartCoroutine(UpdatePath());
    }

    /*public void takeDamage(int amount, Transform source)
    {
        health -= amount;
        Vector3 dir = source.position - transform.position;

        dir = -dir.normalized;
        this.gameObject.GetComponent<Rigidbody2D>().AddForce(dir * pushbackForce);

    }*/
    private void FixedUpdate()
    {
        if (path != null)
        {



            if (currentWaypoint >= path.vectorPath.Count)
            {
                if (pathIsEnded)
                    return;
                //Debug.Log("End of path reached.");
                pathIsEnded = true;
                return;
            }

            pathIsEnded = false;

            Vector3 dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;
            dir *= speed * Time.fixedDeltaTime;

            if (!anim.GetBool("death"))
                rb.AddForce(dir, fMode);

            float dist = Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]);

            if (dist < nextWaypointDistance)
            {
                currentWaypoint++;
                return;
            }

        }


    }

    
}
