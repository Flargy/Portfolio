using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour {

    public int health = 10;

    private GameObject player;

    private void Start()
    {
        player = GameObject.Find("hitman1_gun");
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Debug.Log(player.GetComponent<PlayerVariables>().health);
            player.GetComponent<PlayerVariables>().healthPack(health);
            Debug.Log(player.GetComponent<PlayerVariables>().health);
            DestroyObject(this.gameObject);
        }
    }

}
