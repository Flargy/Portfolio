using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRatePU : MonoBehaviour {

    public float fireRateBuff = 1;

    private GameObject player;

    private void Start()
    {
        player = GameObject.Find("hitman1_gun");
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")) {
            player.transform.Find("Gun").GetComponent<Weapon>().firePowerUp(fireRateBuff);
            DestroyObject(this.gameObject);
        }
    }
}
