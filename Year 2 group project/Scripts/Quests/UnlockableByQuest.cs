using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockableByQuest : MonoBehaviour
{
    [SerializeField] private int requiredQuestID;
    // Start is called before the first frame update
    void Start()
    {
        if (QuestComponent.IsCompleted(requiredQuestID))
            gameObject.SetActive(false);
        else
        {
            EventHandeler.Current.RegisterListener(EventHandeler.EVENT_TYPE.CompleteQuest, RemoveDoor);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void RemoveDoor(EventInfo eventInfo)
    {
        CompleteQuestEventInfo cqei = (CompleteQuestEventInfo)eventInfo;

        if(cqei.eventQuestID == requiredQuestID)
        {
            gameObject.SetActive(false);
        }
    }
}
