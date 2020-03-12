using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour {

    
    public AudioClip pistol;
    public AudioClip rifleBlast;
    public AudioClip shotgunBlast;
    // public Sprite handGun;
    // public Sprite rifle;
    // public Sprite shotgun;
    public Sprite[] selectedGun;

    public static float bulletDamage;
    public static int shotgunAmmo;
    public static int rifleAmmo;
    public static int currentAmmo = 1337;

   

    public UnityEngine.UI.Button weaponBtn;

    public Transform bulletTrailPrefab;
    public Transform shotgunBullets;

    float timeToFire = 0;
    Transform firePoint;
    Transform firePointShotgun1;
    Transform firePointShotgun2;
    Transform firePointShotgun3;
    Transform firePointShotgun4;
    Transform firePointPistol;

    private float fireBuff = 0;

    private AudioSource source;
    private float volLowRange = 0.5f;
    private float volHighRange = 1.0f;
    private int weaponChoice = 1;
    private SpriteRenderer sprite;
    private float fireRate = 1.4f;
    private Animator anim;
    private bool firstShotTimer = true;
    private bool delayTimeOnShot = true;
  



    void Awake () {

        currentAmmo = 1337;
        shotgunAmmo = 10;
        rifleAmmo = 100;
        bulletDamage = 10;
        source = GetComponent<AudioSource>();
        sprite = GetComponent<SpriteRenderer>(); 
        firePoint = transform.Find("FirePoint");
        firePointPistol = transform.Find("FirePointPistol");
        firePointShotgun1 = transform.Find("FirePointShotgun1");
        firePointShotgun2 = transform.Find("FirePointShotgun2");
        firePointShotgun3 = transform.Find("FirePointShotgun3");
        firePointShotgun4 = transform.Find("FirePointShotgun4");
        anim = GetComponentInParent<Animator>();
        if (firePoint == null)
        {
            Debug.LogError("No firepoint");
        }
        anim.SetInteger("weaponChoice", weaponChoice);
        weaponBtn.image.sprite = selectedGun[0];

    }

	
	void Update () {
#if UNITY_STANDALONE_WIN
        if (fireRate == 0)
        {
            if (Input.GetButtonDown("Fire1")){
                Shoot();
            }
        }
        else
        {
            if (Input.GetButton("Fire1") && Time.time > timeToFire)
            {
                timeToFire = Time.time + 1 / (fireRate + fireBuff);
                Shoot();
            }
        }
#endif

#if UNITY_ANDROID

        
            if (PlayerController.primaryShooting)
            {
            anim.SetBool("shooting", true);
            if (Time.time > timeToFire)
            {
                timeToFire = Time.time + 1 / (fireRate + fireBuff);
                if(delayTimeOnShot)
                    StartCoroutine(FirstShotDelay());
                
            }
        }
        else
        {
            firstShotTimer = true;
            delayTimeOnShot = true;
            anim.SetBool("shooting", false);
        }
        
#endif
    }

    private IEnumerator FirstShotDelay()
    {
        delayTimeOnShot = false;
        if (firstShotTimer)
        {
            firstShotTimer = false;
            yield return new WaitForSeconds(0.2f);
        }

        Shoot();
        delayTimeOnShot = true;
    }

    void Shoot()
    {
        float vol = Random.Range(volLowRange, volHighRange);
        //Vector2 fireTargetPosition = new Vector2(firePointDirection.position.x, firePointDirection.position.y);
        //Vector2 firePointPosition = new Vector2(firePoint.position.x, firePoint.position.y);
        //RaycastHit2D hit = Physics2D.Raycast(firePointPosition, fireTargetPosition, 100, whatToHit);
        if (weaponChoice == 3)
        {
            if (shotgunAmmo != 0)
            {
                source.PlayOneShot(shotgunBlast, vol);
                Instantiate(shotgunBullets, firePoint.position, firePoint.rotation);
                Instantiate(shotgunBullets, firePointShotgun1.position, firePointShotgun1.rotation);
                Instantiate(shotgunBullets, firePointShotgun2.position, firePointShotgun2.rotation);
                Instantiate(shotgunBullets, firePointShotgun3.position, firePointShotgun3.rotation);
                Instantiate(shotgunBullets, firePointShotgun4.position, firePointShotgun4.rotation);
                shotgunAmmo--;
                currentAmmo = shotgunAmmo;
            }
            if(shotgunAmmo <= 0)
            {
                weaponChoice = 1;
                bulletDamage = PlayerPrefs.GetFloat("pistolDamage", 10f);
                //sprite.sprite = handGun;
                weaponBtn.image.sprite = selectedGun[0];
                fireRate = 1.4f;
                currentAmmo = 1337;
                anim.SetInteger("weaponChoice", weaponChoice);
            }
        }
        
        else if (weaponChoice == 2)
        {
            if (rifleAmmo != 0)
            {
                source.PlayOneShot(rifleBlast, vol);
                Instantiate(bulletTrailPrefab, firePoint.position, firePoint.rotation);
                rifleAmmo--;
                currentAmmo = rifleAmmo;
            }
            if (rifleAmmo <= 0)
            {
                weaponChoice = 1;
                bulletDamage = PlayerPrefs.GetFloat("pistolDamage", 10f);
                //sprite.sprite = handGun;
                weaponBtn.image.sprite = selectedGun[0];
                fireRate = 1.4f;
                currentAmmo = 1337;
                anim.SetInteger("weaponChoice", weaponChoice);
            }
        }
        else if (weaponChoice == 1)
        {
            source.PlayOneShot(pistol, vol);
            Instantiate(bulletTrailPrefab, firePointPistol.position, firePointPistol.rotation);
            currentAmmo = 1337;
        }
       
    }

    public void selectWeapon()
    {
        weaponChoice++;
        if (weaponChoice > 3)
        {
            weaponChoice = 1;
        }

        if (weaponChoice == 1)
        {
            
            bulletDamage = 10f;
            weaponBtn.image.sprite = selectedGun[0];
            fireRate = 1.4f;
            currentAmmo = 1337;
            anim.SetInteger("weaponChoice", weaponChoice);
            
        }
            
        else if (weaponChoice == 2)
        {
            bulletDamage = 5f;
            weaponBtn.image.sprite = selectedGun[1];
            fireRate = 8f;
            currentAmmo = rifleAmmo;
            anim.SetInteger("weaponChoice", weaponChoice);
         
        }
            
        else if (weaponChoice == 3)
        {
            bulletDamage = 12f;
            weaponBtn.image.sprite = selectedGun[2];
            fireRate = 0.7f;
            currentAmmo = shotgunAmmo;
            anim.SetInteger("weaponChoice", weaponChoice);
            
        }
       

    }

    public void firePowerUp(float fireRateBuff)
    {
        StartCoroutine(firePUHelpCode(fireRateBuff));
    }
    public IEnumerator firePUHelpCode(float fireRateBuff) {
        fireBuff += fireRateBuff;
        yield return new WaitForSeconds(6f);
        fireBuff -= fireRateBuff;
    }

    public void ammoPickup()
    {
        if (weaponChoice == 2)
            currentAmmo = rifleAmmo;
        else if (weaponChoice == 3)
            currentAmmo = shotgunAmmo;
    }

    
}
