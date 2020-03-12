using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour {

    private GameObject player;
    private GameObject door;

    private void Start()
    {
        player = GameObject.Find("hitman1_gun");
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
           door.GetComponent<LevelDoorScript>().openTheDoor();
            DestroyObject(this.gameObject);
        }
    }

    public void assignDoor(GameObject door)
    {
        this.door = door;
    }
}
