using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssignDoorScript : MonoBehaviour {

    public List<GameObject> allTriggers;
    public List<GameObject> allDoors;
    private int x;

    // Use this for initialization
    void Start () {
        foreach (GameObject trigger in allTriggers)
        {
            x = (int)Random.Range(0f, (float)allDoors.Count);
            trigger.GetComponent<OpenDoor>().assignDoor(allDoors[x]);
            allDoors.RemoveAt(x);            
        }
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
