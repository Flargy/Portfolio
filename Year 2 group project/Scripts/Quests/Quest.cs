using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest
{
    [SerializeField] private int questID;
    public int QuestID {get { return questID; } }

    [SerializeField] private int rewardNumber;
    public int RewardID { get { return rewardNumber; } }

    [SerializeField] private string questDescription;
    public string QuestDescription { get { return questDescription; } }

    [SerializeField] private string questCompleteDescription;
    public string QuestCompleteDescription { get { return questCompleteDescription; } }

    [SerializeField] private string questActiveDescription;
    public string QuestActiveDescription { get { return questActiveDescription; } }

    [SerializeField] private string questName;
    public string QuestName { get { return questName; } }

    [SerializeField] private bool activeStatus;

    [SerializeField] private QuestGoal goal;
    public QuestGoal QuestType { get { return goal; } }

    /// <summary>
    /// Creates a new Quest;
    /// </summary>
    /// <param name="ID">ID for the quest;</param>
    /// <param name="reward">Reward ID for the quest</param>
    /// <param name="Dtext">Description for the quest;</param>
    /// <param name="Ntext">Name for the quest</param>
    /// <param name="newGoal">Goal that needs to be reached to complete the quest;</param>
    public Quest(int ID, int reward, string description, string complete, string active, string name, QuestGoal newGoal)
    {
        questID = ID;
        rewardNumber = reward;
        questDescription = description;
        questCompleteDescription = complete;
        questActiveDescription = active;
        questName = name;
        goal = newGoal;
    }

    /// <summary>
    /// Sets the quest to active;
    /// </summary>
    public void SetActiveQuest()
    {

        activeStatus = true;
    }


    /// <summary>
    /// Increases the quests progress by calling the quests goal and increasing its progress;
    /// if the goal is meet with the increase then the quest will send GoalReachedEventInfo callback;
    /// </summary>
    public void IncreaseQuestProgress()
    {
        if (activeStatus == true)
            goal.increaseProgress();
        if (goal.IsGoalReached())
        {
            GoalReachedEventInfo grei = new GoalReachedEventInfo { completedQuestID = questID };
            //GoalReachedEventInfo grei = new GoalReachedEventInfo { completedQuestID = questID};
            EventHandeler.Current.FireEvent(EventHandeler.EVENT_TYPE.QuestGoalReached, grei);

           // Debug.Log("Quest " + questID + " is completed");

        }
    }
}
