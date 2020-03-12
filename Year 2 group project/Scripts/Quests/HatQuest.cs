using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//Main Author: Marcus Lundqvist
//Secondary Author: Alishia Nossborn


public class HatQuest : MonoBehaviour
{
    private AudioSource source;
    public string questItem;
    public AudioClip gotQuestClip;
    public AudioClip finishedQuestClip;
    public GameObject questInfo;
    public GameObject rewardInfo;
    public GameObject repeatedInfo;

    private bool isFinished = false;
   
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Displays information if the player does not yet have the item that the quest is looking for.
    /// If the player has the item in their inventory it is removed and new information is displayed.
    /// </summary>
    /// <param name="col"></param>
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player") && !isFinished)
        {
            //Spela upp quest, text eller något.

            if (col.gameObject.GetComponent<PlayerStateMashine>().Inventory.Contains(questItem))
            {
                col.gameObject.GetComponent<PlayerStateMashine>().RemoveItem(questItem);
                rewardInfo.SetActive(true);
                source.PlayOneShot(finishedQuestClip);
                isFinished = true;
            }
            else
            {
                questInfo.SetActive(true);

                if (!source.isPlaying)
                {
                    source.PlayOneShot(gotQuestClip);
                }
            }
        }
        else if (col.gameObject.CompareTag("Player") && isFinished)
        {
            repeatedInfo.SetActive(true);
        }

    }
 
    private void OnTriggerExit(Collider col)
    {
        questInfo.SetActive(false);
        rewardInfo.SetActive(false);
        repeatedInfo.SetActive(false);
    }
}
