using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoDisplay : MonoBehaviour {

    private Text ammoText;

    void Start () {

        ammoText = this.gameObject.GetComponent<Text>();
		
	}
	
	// Update is called once per frame
	void Update () {
        if ( Weapon.currentAmmo == 1337)
        {
            ammoText.text = "";
        }
        else
        {
            ammoText.text = Weapon.currentAmmo + "";

        }
    }
}
