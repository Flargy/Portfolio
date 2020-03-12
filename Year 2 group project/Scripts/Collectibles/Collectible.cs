using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Main Author: Marcus Lundqvist

public class Collectible : MonoBehaviour
{
    [SerializeField] private string itemType;

    /// <summary>
    /// Activates <see cref="PlayerStateMashine.AddItem(string, GameObject)"/> if the player enters the trigger zone
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ProgressQuestEventInfo pqei = new ProgressQuestEventInfo { type = GoalType.Find };
            EventHandeler.Current.FireEvent(EventHandeler.EVENT_TYPE.ProgressQuest, pqei);
            other.GetComponent<PlayerStateMashine>().AddItem(itemType, gameObject);
        }
            
    }
}
