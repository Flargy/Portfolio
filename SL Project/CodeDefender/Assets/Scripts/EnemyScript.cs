using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    public bool playerLocated = false;
    public GameObject player;
    public GameObject fightTrigger;
    [SerializeField] private float timeToDestroy = 0;
    [SerializeField] private bool isRanged = false;
    [SerializeField] private GameObject bullet;

    private NavMeshAgent agent;
    private float followDelay = 0;
    private float attackDelay;
    private bool canAttack = true;
    private bool isDead = false;

    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (playerLocated && isDead == false)
        {
            if(canAttack == false)
            {
                attackDelay += Time.deltaTime;
                if(attackDelay >= 1.5f && isRanged == false)
                {
                    canAttack = true;
                }
                else if (attackDelay >= 4.0f && isRanged == true)
                {
                    canAttack = true;
                }
            }

            followDelay += Time.deltaTime;
            if (followDelay > 0.3f)
            {
                agent.SetDestination(player.transform.position);
                followDelay = 0.0f;
            }
            
            if (CheckRemainingDistance(player.transform.position, 1.0f) && isRanged == false)
            {
                if (canAttack == true)
                {
                    player.GetComponent<PlayerController>().TakeDamage();
                    canAttack = false;
                    attackDelay = 0.0f;
                }

            }

            else if(CheckRemainingDistance(player.transform.position, 15f) && isRanged == true)
            {
                if (canAttack == true)
                {
                    transform.LookAt(new Vector3(player.transform.position.x, transform.position.y,  player.transform.position.z));
                    ShootPlayer();
                    canAttack = false;
                    attackDelay = 0.0f;
                }
            }
        }
        
    }

    public void Die()
    {
        isDead = true;
        agent.isStopped = true;
        fightTrigger.GetComponent<FightTrigger>().RemoveEnemy(gameObject);
        Destroy(gameObject);
    }

    private bool CheckRemainingDistance(Vector3 destination, float acceptableRange)
    {
        return (destination - transform.position).sqrMagnitude < acceptableRange * acceptableRange;
    }

    private void ShootPlayer()
    {
        Instantiate(bullet, transform.position +(transform.forward * 2), transform.rotation);
    }
}
