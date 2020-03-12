using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Main Author: Hjalmar Andersson

public class SkillCooldown : MonoBehaviour
{
    bool used = false;
    public Image picture;
    public float coolDown;
    
    // Start is called before the first frame update
    void Start()
    {
        coolDown = 5;
        used = false;
        coolDown = 1 / coolDown;
        picture.fillAmount = 1f;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            used = true;
            picture.fillAmount = 0f;
        }
        if (used == true)
        {
            picture.fillAmount += coolDown * Time.deltaTime;
            if (picture.fillAmount == 1)
            {
                Debug.Log("Done!");
                used = false;
            }
        }
    }  
    
}
