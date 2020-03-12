using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClutterRandom : MonoBehaviour {

    public GameObject[] objects;
    private float randXPos, randYPos, randRot, spawnChance = 35;

    void Start()
    {
        float spawnCheck = Random.Range(0, 100);
        int objectSpawn = (int)Random.Range(0, (objects.Length - 0.1f));
        randXPos = Random.Range(0.0f, 2.0f) - 1.0f;
        randYPos = Random.Range(0.0f, 2.0f) - 1.0f;
        randRot = Random.Range(0, 359);
        Vector3 position = new Vector3(this.transform.position.x + randXPos, this.transform.position.y + randYPos, this.transform.position.z);

        if (spawnCheck <= spawnChance)
        {
            Instantiate(objects[objectSpawn], position, Quaternion.Euler(0.0f, 0.0f, randRot));

        }

    }
}
