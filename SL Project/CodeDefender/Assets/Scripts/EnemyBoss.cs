using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBoss : MonoBehaviour
{
    public bool playerLocated = false;
    public GameObject player;
    [SerializeField] private float timeToDestroy = 0;
    [SerializeField] private bool isRanged = false;
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject firePointFront;
    [SerializeField] private GameObject firePoint1;
    [SerializeField] private GameObject firePoint2;
    [SerializeField] private GameObject firePoint3;
    [SerializeField] private GameObject firePoint4;
    [SerializeField] private GameObject firePoint5;
    [SerializeField] private GameObject postFightQuiz;

    private NavMeshAgent agent;
    private bool isDead = false;
    private float followDelay;
    private float turnDelay;
    private float attackDuration = 0;
    private float timeAttacked = 1.0f;
    private float attackCooldown = 0.0f;
    private int attackNumber = 3;
    private int health = 40;
    private float shotDelay = 0;
    private bool secondPhase = false;

    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    { if (isDead == false)
        {
            if (timeAttacked > attackDuration && attackCooldown >= 3.0f)
            {
                attackCooldown = 0;
                timeAttacked = 0.0f;
                attackNumber = Random.Range(attackNumber + 1, 3) % 3;

                switch (attackNumber)
                {
                    case 2:
                        attackDuration = 3.0f;
                        break;
                    default:
                        attackDuration = 2.0f;
                        break;
                }
            }

            if (timeAttacked < attackDuration)
            {
                timeAttacked += Time.deltaTime;

                if (shotDelay >= 0.1f)
                {
                    shotDelay = 0.0f;
                    switch (attackNumber)
                    {
                        case 0:
                            ShootForward();
                            break;
                        case 1:
                            SpreadShot();
                            break;
                        case 2:
                            ShootAround();
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    shotDelay += Time.deltaTime;
                }
            }
            else
            {
                attackCooldown += Time.deltaTime;
            }

            if (secondPhase == true)
            {
                followDelay += Time.deltaTime;
                if (followDelay > 0.3f)
                {
                    agent.SetDestination(player.transform.position);
                    followDelay = 0.0f;
                }
            }

            turnDelay += Time.deltaTime;
            if(turnDelay >= 0.1f)
            {
                turnDelay = 0.0f;
                transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));
            }


            if (Input.GetKeyDown(KeyCode.J))
            {
                ShootForward();
            }
            if (Input.GetKeyDown(KeyCode.K))
            {
                ShootAround();
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                SpreadShot();
            }
        }

    }

    public void Damage()
    {
        health--;
        if(health <= 20 && secondPhase == false)
        {
            secondPhase = true;
        }
        else if(health <= 0 && isDead == false)
        {
            isDead = true;
            postFightQuiz.SetActive(true);
            firePoint1.SetActive(false);
            firePoint2.SetActive(false);
            firePoint3.SetActive(false);
            firePoint4.SetActive(false);
            firePoint5.SetActive(false);
            agent.isStopped = true;
        }
    }

    private bool CheckRemainingDistance(Vector3 destination, float acceptableRange)
    {
        return (destination - transform.position).sqrMagnitude < acceptableRange * acceptableRange;
    }

    private void ShootForward()
    {
        Instantiate(bullet, firePointFront.transform.position, firePointFront.transform.rotation);
    }

    private void SpreadShot()
    {
        Instantiate(bullet, firePointFront.transform.position, firePointFront.transform.rotation);
        Instantiate(bullet, firePointFront.transform.position, firePointFront.transform.rotation * Quaternion.Euler(0f, 20f, 0f));
        Instantiate(bullet, firePointFront.transform.position, firePointFront.transform.rotation * Quaternion.Euler(0f, -20f, 0f));

    }

    private void ShootAround()
    {
        Instantiate(bullet, firePoint1.transform.position, firePoint1.transform.rotation);
        Instantiate(bullet, firePoint2.transform.position, firePoint2.transform.rotation);
        Instantiate(bullet, firePoint3.transform.position, firePoint3.transform.rotation);
        Instantiate(bullet, firePoint4.transform.position, firePoint4.transform.rotation);
        Instantiate(bullet, firePoint5.transform.position, firePoint5.transform.rotation);

    }


}
