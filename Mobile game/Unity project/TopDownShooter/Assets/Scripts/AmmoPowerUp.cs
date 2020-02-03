using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPowerUp : MonoBehaviour {

    private GameObject player;
    public int ammoUpShotgun = 2;
    public int ammoUpRifle = 20;

    private void Start()
    {
        player = GameObject.Find("hitman1_gun");
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Weapon.shotgunAmmo += ammoUpShotgun;
            Weapon.rifleAmmo += ammoUpRifle;
            DestroyObject(this.gameObject);
            player.GetComponentInChildren<Weapon>().ammoPickup();
        }
    }
}
