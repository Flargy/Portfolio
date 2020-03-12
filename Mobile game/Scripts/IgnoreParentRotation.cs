using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreParentRotation : MonoBehaviour {

    public GameObject shadow;


	void Start () {

        Vector3 position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);

        Instantiate(shadow, position, Quaternion.identity);
	}
}
