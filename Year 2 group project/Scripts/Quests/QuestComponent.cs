using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestComponent : MonoBehaviour
{

    [SerializeField] static private Dictionary<int, Quest> questLog = new Dictionary<int, Quest>();

    [SerializeField] static private List<int> completedQuests = new List<int>();

    [SerializeField] static private Dictionary<int, Quest> quests = new Dictionary<int, Quest>();

    [SerializeField] static private int collectedCows = 0;

    [SerializeField] static private Text dialogText;

    // Start is called before the first frame update
    void Start()
    {
        //Activate Quest Listerner
        EventHandeler.Current.RegisterListener(EventHandeler.EVENT_TYPE.ActivateQuest, ActivateQuest);
        //Complete Quest Listerner
        EventHandeler.Current.RegisterListener(EventHandeler.EVENT_TYPE.CompleteQuest, CompleteQuest);
        //Goal Reached Quest Listerner
        EventHandeler.Current.RegisterListener(EventHandeler.EVENT_TYPE.QuestGoalReached, ChangeStatusForQuests);
        //Progress Quest Listerner
        EventHandeler.Current.RegisterListener(EventHandeler.EVENT_TYPE.ProgressQuest, QuestProgress);
        //Collected Cows Counter
        EventHandeler.Current.RegisterListener(EventHandeler.EVENT_TYPE.CollectCow, CollectCow);

        //quests.Add(ID, New Quest(ID, RewardID, Start text, Klar text, Active text, Namn, new QuestGoal(GoalType, slut antal, start progress)))
        quests.Add(1, new Quest(1, 4, 
            "Please gather an Item by pressing 'M'.", 
            "Well done, you are done with the quest! go see if there are others who need help", 
            "I told you to press the 'M' key, are you stupid?.", 
            "Press 'M'",
            new QuestGoal(GoalType.Gather, 1, 0)));

        quests.Add(2, new Quest(2, 3, "Ah this time you need to escort a cow to the pen, do so by song(1) and when the cow follows song(3).", "Well done, you almost make me proud. Go back to the first dude.", "Oh my, you must truly be bad at singing if no cow wants to follow you.", "Escort a cow", new QuestGoal(GoalType.Escort, 1, 0)));
        quests.Add(3, new Quest(3, 4, "ok, lets see what you have learned. Try pressig the 'M' key, i dunno like 3 times? yea three times is good. go on", "Marvolus, brings a tear to my eye!", "Yea you still need to press that key...three times. Not Four, not two. Five is out of the question. You shall only count two while going towards three.", "The holy 'M'", new QuestGoal(GoalType.Gather, 3, 0)));
        quests.Add(4, new Quest(4, 1, "ok, Press the 'M' key, i dunno like 3 times?", "Marvolus", "Yea you still need to press that ke", "mmm Marabou", new QuestGoal(GoalType.Gather, 3, 0)));
        quests.Add(5, new Quest(5, 2, "ok, Press the 'M' key, i dunno like 3 times?", "Marvolus", "Yea you still need to press that ke", "mmm Marabou nu med Daim", new QuestGoal(GoalType.Gather, 3, 0)));

    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M)){
            ProgressQuestEventInfo pqei = new ProgressQuestEventInfo { type = GoalType.Gather };
            EventHandeler.Current.FireEvent(EventHandeler.EVENT_TYPE.ProgressQuest, pqei);
        }
    }

    /// <summary>
    /// This method is called when the QuestComponent reacts to a callback event with the type CollectCow.
    /// This method will as a response create a callback that increases progress for a escort quest
    /// </summary>
    /// <param name="eventInfo">CollectCowEventInfo;</param>
    private static void CollectCow(EventInfo eventInfo)
    {
        collectedCows++;
        //Debug.Log("Ko är lämnad");
        ProgressQuestEventInfo pqei = new ProgressQuestEventInfo { type = GoalType.Escort };
        EventHandeler.Current.FireEvent(EventHandeler.EVENT_TYPE.ProgressQuest, pqei);
    }

    /// <summary>
    /// Called when the QuestComponent is reacting to a callback event that activates a quest; 
    /// If the activated quest is an escort quest, its progress will be filled with cows already captured; 
    /// </summary>
    /// <param name="eventInfo">ActivateQuestEventInfo;</param>
    private static void ActivateQuest(EventInfo eventInfo)
    {
        ActivateQuestEventInfo activate = (ActivateQuestEventInfo)eventInfo;

        if (quests.ContainsKey(activate.eventQuestID)) {
            quests[activate.eventQuestID].SetActiveQuest();
            questLog.Add(activate.eventQuestID,quests[activate.eventQuestID]);
            if(questLog[activate.eventQuestID].QuestType.TypeOfGoal.Equals(GoalType.Escort))
            {
                questLog[activate.eventQuestID].QuestType.AddCollectedCows(collectedCows -1);
                questLog[activate.eventQuestID].IncreaseQuestProgress();
            }
        }
    }

    /// <summary>
    /// Called when the QuestComponent is reacting to a callback event that completes a quest;
    /// Removes the quest from the questLog and adds the questID to completedQuests;
    /// </summary>
    /// <param name="eventInfo">CompleteQuestEventInfo;</param>
    private static void CompleteQuest(EventInfo eventInfo)
    {
        CompleteQuestEventInfo completed = (CompleteQuestEventInfo)eventInfo;

        AvailableQuestEventInfo aqei = new AvailableQuestEventInfo { };
        EventHandeler.Current.FireEvent(EventHandeler.EVENT_TYPE.AvailableQuest, aqei);
    }

    /// <summary>
    /// Reacts to a callback event caused by goalReachedEventInfo
    /// This changes the staus of the quest, marking it as complete if the goal is reached. This will make the quest turn in-able at the questgiver
    /// </summary>
    /// <param name="eventInfo"></param>
    private void ChangeStatusForQuests(EventInfo eventInfo)
    {
        GoalReachedEventInfo completed = (GoalReachedEventInfo)eventInfo;
        Quest completedQuest = null;
        if (questLog.ContainsKey(completed.completedQuestID))
        {
            completedQuest = questLog[completed.completedQuestID];
            completedQuests.Add(completed.completedQuestID);
        }
        if (completedQuest != null)
            questLog.Remove(completed.completedQuestID);
    }

    /// <summary>
    /// Called when the QuestComponent is reacting to a callback event that grats progress to a questType;
    /// It creates a new temporary list with all the quests that has the same questType;
    /// Reason for that is if a quest is completed during the process it will remove itself from the questlog list. Removal during runtime is not wanted so a new temporary list it is.
    /// Then it loops through the new list and calling its progress;
    /// </summary>
    /// <param name="eventInfo">ProgressQuestEventInfo;</param>
    private static void QuestProgress(EventInfo eventInfo)
    {
        ProgressQuestEventInfo pqei = (ProgressQuestEventInfo)eventInfo;
        List<Quest> tempQuestList = new List<Quest>();

        foreach(Quest q in questLog.Values)
        {
            if(q.QuestType.TypeOfGoal == pqei.type)
            {
                tempQuestList.Add(q);
            }
        }
        foreach(Quest qst in tempQuestList)
        {
            qst.IncreaseQuestProgress();
        }
    }

    /// <summary>
    /// Returns if a quest with the entered ID has been completed or not
    /// </summary>
    /// <param name="id">ID for the quest;</param>
    /// <returns>true if the quest exists in completedQuests;</returns>
    static public bool IsCompleted(int id)
    {
        if (id == 0)
        {
            return true;
        }
        else if (completedQuests.Count <= 0)
            return false;
        return completedQuests.Contains(id);
    }

    /// <summary>
    /// Retuns the name of the quest that has the same ID as the one entered;
    /// </summary>
    /// <param name="id">ID for the quest;</param>
    /// <returns>name of the Quest;</returns>
    static public string GetQuestName(int id)
    {
        return quests[id].QuestName;
    }

    /// <summary>
    /// Returns the description text for the quest with the same ID as the one entered;
    /// </summary>
    /// <param name="id">ID for the quest;</param>
    /// <returns>Description for a quest;</returns>
    static public string GetQuestDescription(int id)
    {
        return quests[id].QuestDescription;
    }

    /// <summary>
    /// Returns a list with all the completed quests index.
    /// Used by the save function to determin what quests the player has completed at the time of save.
    /// </summary>
    /// <returns></returns>
    static public List<int> GetCompletedQuestList()
    {
        return completedQuests;
    }

    /// <summary>
    /// Returns the name for all the questst that are active in the questlog;
    /// </summary>
    /// <returns>The names for all the quests in questLog;</returns>
    static public string GetQuestLog()
    {
        string log = "";
        foreach(Quest q in questLog.Values)
        {
            log += q.QuestName + "\n";
        }
        return log;
    }

    /// <summary>
    /// Returns the Quest with the quest ID entered.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    static public Quest GetQuestFromID(int id)
    {
        if (quests.ContainsKey(id))
            return quests[id];
        else
            return null;
    }

    static public void ChangeFontOfDialog(string name){
        dialogText.font = Resources.Load<Font>("Fonts/" + name);
    }
}
