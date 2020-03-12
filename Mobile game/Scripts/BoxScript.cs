using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScript : MonoBehaviour {

    //private float force = 3;
    //public GameObject player;
    //Rigidbody2D rgb;
    
    // Use this for initialization
	void Start ()
    {
        //rgb = this.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    /*void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            if (player.transform.position.x > this.transform.position.x)
            {
                rgb.AddForce(new Vector2(-force, 0));
            }
            else if (player.transform.position.x < this.transform.position.x)
            {
                rgb.AddForce(new Vector2(force, 0));
            }
            else if (player.transform.position.y > this.transform.position.y)
            {
                rgb.AddForce(new Vector2(0, -force));
            }
            if (player.transform.position.y < this.transform.position.y)
            {
                rgb.AddForce(new Vector2(0, force));
            }
        }
       
    }*/
}
