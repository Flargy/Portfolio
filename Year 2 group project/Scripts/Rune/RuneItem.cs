using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Author: Marcus Lundqvist

public class RuneItem : MonoBehaviour
{
    [SerializeField] private int runeNumber;

    /// <summary>
    /// Performes a reward callback to the player so a rune is added with the number the object holds;
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            RewardQuestInfo rqei = new RewardQuestInfo { rewardNumber = runeNumber };
            EventHandeler.Current.FireEvent(EventHandeler.EVENT_TYPE.QuestReward, rqei);
            Destroy(gameObject);
        }
    }
}
