using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDoorScript : MonoBehaviour {

    public GameObject openDoor;
    public GameObject lockedDoor;
    private bool open = false;
    private BoxCollider2D collider;


    
	void Awake () {
        collider = GetComponentInChildren<BoxCollider2D>();
        openDoor.SetActive(false);
        open = false;
	}
	

    public void openTheDoor()
    {
        if (!open)
        {
            lockedDoor.SetActive(false);
            openDoor.SetActive(true);
            //collider.enabled = !collider.enabled;
            open = true;
        }
    }

    public bool getOpen()
    {
        return open;
    }
}
