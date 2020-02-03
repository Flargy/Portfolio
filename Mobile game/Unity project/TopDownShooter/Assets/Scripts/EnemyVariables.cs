using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVariables : MonoBehaviour {

    public float health;
    public float pushbackForce, hitCooldownTime = 1.0f;
    public int damage;
    public AudioClip damageSound;
    public AudioClip deathSound;
    public GameObject[] powerUps;
    public float range = 1.5f;
    
    private float volLowRange = 0.5f;
    private float volHighRange = 1.0f;
    private float vol;
    private float hitCooldown;
    private float distance;
    private AudioSource aSource;
    private Animator anim;
    private GameObject player;
    private bool takingDamage = true;

    void Start ()
    {
        hitCooldown = hitCooldownTime;

        aSource = GetComponent<AudioSource>();

        player = GameObject.Find("hitman1_gun");

        anim = GetComponent<Animator>();
    }


    void FixedUpdate () {
        if (hitCooldown > 0)
            hitCooldown -= Time.deltaTime;

        distance = Vector3.Distance(transform.position, player.transform.position);
        
        if  (distance < range && hitCooldown <= 0 && !anim.GetBool("death"))
        {
            player.GetComponent<PlayerVariables>().takeDamage(damage, transform);
            hitCooldown = hitCooldownTime;
        }

    }

    public float getRange()
    {
        return range;
    }

    public void takeDamage(float amount, Transform source)
    {
        if (!anim.GetBool("death"))
        {
            vol = Random.Range(volLowRange, volHighRange);

            if (takingDamage)
                aSource.PlayOneShot(damageSound, 0.5f);

            if (takingDamage)
                StartCoroutine(damageNoiseCooldown());

            health -= amount;
            Vector3 dir = source.position - transform.position;

            if (health > 0)
            {
                dir = -dir.normalized;
                this.gameObject.GetComponent<Rigidbody2D>().AddForce(dir * pushbackForce);
            }

            if (health <= 0)
            {
                StartCoroutine(death());
            }

        }

    }

    IEnumerator damageNoiseCooldown()
    {
        takingDamage = false;
        yield return new WaitForSeconds(1f);
        takingDamage = true;
    }

    IEnumerator death()
    {
        aSource.PlayOneShot(deathSound, 1);
        int i = (int)(Random.Range(0.0f, 1.0f) * 10);
        if (i >= 9)
        {
            Instantiate(powerUps[0], transform.position, Quaternion.identity);
        }
        else if (i >= 8 && i < 9)
        {
            Instantiate(powerUps[1], transform.position, Quaternion.identity);

        }
        else if (i >= 7 && i < 8)
        {
            Instantiate(powerUps[2], transform.position, Quaternion.identity);
        }
        else if (i >= 6 && i < 7)
        {
            Instantiate(powerUps[3], transform.position, Quaternion.identity);
        }

        Debug.Log(transform.position);
        MonsterSpawn.numberOfMonsters--;
        GetComponent<CircleCollider2D>().enabled = false;
        this.gameObject.layer = 2;
        anim.SetBool("death", true);
        yield return new WaitForSeconds(5f);
        DestroyObject(this.gameObject);
    }
   
}
