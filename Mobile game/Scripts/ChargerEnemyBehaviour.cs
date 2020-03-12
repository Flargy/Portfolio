using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;


[RequireComponent (typeof (Rigidbody2D))]
[RequireComponent (typeof (Seeker))]
public class ChargerEnemyBehaviour : MonoBehaviour {

    public float speed, updateRate = 2.0f, nextWaypointDistance = 0.1f;
    //public int health;
    public Path path;
    public ForceMode2D fMode;
    public float chargeDistance = 6;
    public float chargeRate = 2;

    [HideInInspector]
    public bool pathIsEnded = false;

    private Seeker seeker;
    private Rigidbody2D rb;
    private float startSpeed, distance, updateRateStart;
    private int currentWaypoint = 0;
    private Transform playerTarget;
    private GameObject player;
    private Animator anim;
    private bool charging = false;
    private Vector3 dir, chargeDir;
    private bool chargeActivated = false;


    void Start () {

        playerTarget = GameObject.Find("hitman1_gun").transform;
        startSpeed = speed;
        MonsterSpawn.numberOfMonsters++;
        updateRateStart = updateRate;
        
        player = GameObject.Find("hitman1_gun");

        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        if (playerTarget == null)
        {
            Debug.LogError("No Player found!");
        }

        seeker.StartPath(transform.position, playerTarget.position, OnPathComplete);

        StartCoroutine(UpdatePath());

        anim = GetComponent<Animator>();
    }


    public void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    IEnumerator UpdatePath()
    {

        seeker.StartPath(transform.position, playerTarget.position, OnPathComplete);

        yield return new WaitForSeconds(1.0f / updateRate);
        StartCoroutine(UpdatePath());
    }

 
    private void FixedUpdate()
    {
        

        distance = Vector3.Distance(transform.position, playerTarget.position);

        
            speed = startSpeed;
            updateRate = updateRateStart;
        

        var lookDir = playerTarget.position - transform.position;
        var angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        if (!anim.GetBool("death"))
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

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

            dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;
            dir *= (speed * Time.fixedDeltaTime);

            if (distance > chargeDistance && !anim.GetBool("death") && !charging)
            {
                rb.AddForce(dir, fMode);
            }
            else if (charging && !anim.GetBool("death"))
            {
                rb.AddForce(chargeDir, fMode);
                //rb.AddForce(chargeDir, ForceMode2D.Impulse);
            }

            if (distance <= chargeDistance && !charging && !chargeActivated && !anim.GetBool("death"))
            {
                StartCoroutine(charge());
            }

            float dist = Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]);

            if (dist < nextWaypointDistance)
            {
                currentWaypoint++;
                return;
            }
        }
    }

    IEnumerator charge()
    {
        chargeActivated = true;

        anim.SetBool("charge", true);
        chargeDistance = 10;

        yield return new WaitForSeconds(chargeRate);
        //rb.AddForce(dir*3f, ForceMode2D.Impulse);
        charging = true;
        chargeDir = new Vector2(playerTarget.position.x, playerTarget.position.y);
        chargeDir = (chargeDir - transform.position).normalized * 50;
        yield return new WaitForSeconds(chargeRate);
        charging = false;
        anim.SetBool("charge", false);
        chargeDistance = 6;
        chargeActivated = false;
    }

    
}
