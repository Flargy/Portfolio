using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeadDoorOpener : MonoBehaviour {

    public GameObject[] doors;

    private bool helpIter = false;


	void Start () {
        int amountOfDoors = (int)(doors.Length / 1.40f);
        int rand;
        int[] nums = new int[amountOfDoors];

        foreach (int i in nums)
        {
            helpIter = false;
            while (!helpIter)
            {
                rand = Random.Range(0, doors.Length - 1);
                if (!doors[rand].GetComponent<LevelDoorScript>().getOpen())
                {
                    doors[rand].GetComponent<LevelDoorScript>().openTheDoor();
                    
                    helpIter = true;
                }
            }
        }
	}
}
