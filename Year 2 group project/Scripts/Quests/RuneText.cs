using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Author: Alishia Nossborn


public class RuneText : MonoBehaviour
{
    public GameObject runeInfo;
    public GameObject interactKeyText;


    private void Update()
    {
//        if (Input.GetKeyDown(KeyCode.E))
//        {
//            runeInfo.SetActive(true);
//            interactKeyText.SetActive(false);
//       }
    }

    /// <summary>
    /// Displays the <see cref="runeInfo"/> text if the player enters the trigger zone.
    /// </summary>
    /// <param name="col"></param>
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player") )
        {
//           interactKeyText.SetActive(true);
            runeInfo.SetActive(true);
        }

    }
    
    /// <summary>
    /// Disables the text if the player exits the trigger zone.
    /// </summary>
    /// <param name="col"></param>
    private void OnTriggerExit(Collider col)
    {
        runeInfo.SetActive(false);
//        interactKeyText.SetActive(false);
    }
}
