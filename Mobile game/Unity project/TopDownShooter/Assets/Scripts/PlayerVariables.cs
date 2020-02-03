using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class PlayerVariables : MonoBehaviour
{
    public static PlayerVariables control;
    public int health = 100;
    public int shield = 4;
    public GameObject shieldSprite;
    public Slider healthSlider;
    public Slider shieldSlider;
    public float pushbackForce = 10;
    //public static Transform playerTarget;
    public AudioClip damageSound;

    private AudioSource source;
    private float volLowRange = 0.5f;
    private float volHighRange = 1.0f;
    private GameObject player;

    private void Awake()
    {

        player = GameObject.Find("hitman1_gun");
        
    }

    void OnGUI()
    {
        healthSlider = GameObject.Find("Slider").GetComponent<Slider>();
        shieldSlider = GameObject.Find("ShieldSlider").GetComponent<Slider>();

    }
    void Start()
    {
        
        source = GetComponent<AudioSource>();
        //playerTarget = transform;
        shieldSprite.SetActive(true);


    }


    void Update()
    {
        if (healthSlider != null)
            healthSlider.value = health;
        if(shieldSlider != null)
            shieldSlider.value = shield;

        if (health <= 0)
            GameController.missionFailed = true;

        if (shield <= 0)
            shieldSprite.SetActive(false);

    }
    public int getHealth()
    {
        return health;
    }
    public int getShield()
    {
        return shield;
    }
    public int setHealth(int h)
    {
        health = h;
        return health;
    }
    public int setShield(int s)
    {
        shield = s;
        return shield;
    }
    public void healthPack(int x)
    {
        if (health <= 90)
            health += x;
    }

    public void shieldPack(int x)
    {
        if (shield < 3)
        {
            shield++;
            shieldSprite.SetActive(true);
        }
    }

    public void takeDamage(int amount, Transform enemy)
    {
        if (shield >= 1)
        {
            Debug.Log("Shield" + shield);
            shield--;
        }
        else
        {
            float vol = UnityEngine.Random.Range(volLowRange, volHighRange);
            source.PlayOneShot(damageSound, vol);
            health -= amount;
        }
        Vector3 dir = enemy.position - transform.position;
        dir = -dir.normalized;
        this.gameObject.GetComponent<Rigidbody2D>().AddForce(dir * pushbackForce);




    }

    public GameObject getPlayer()
    {
        return this.gameObject;
    }
}
    