using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{

    [SerializeField] private int questID;
    [SerializeField] private int questIDRecuirement;
    [SerializeField] private int alternativeIDRequired;
    [SerializeField] private Material avalibleQuest;
    [SerializeField] private Material activeQuest;
    [SerializeField] private Material completedQuest;

    private bool started = false;
    private bool turnedIn = false;

    [SerializeField] private QuestGiver nextQuestInLine;

    [SerializeField] private GameObject lamp;
    private Renderer lampRenderer;
    private Material standardMaterial;
    
    private Quest myQuest;

    private void Awake()
    {
        if (enabled == false)
            return;
        EventHandeler.Current.RegisterListener(EventHandeler.EVENT_TYPE.QuestGoalReached, QuestCompleted);
        EventHandeler.Current.RegisterListener(EventHandeler.EVENT_TYPE.AvailableQuest, MakeQuestAvalible);
        //EventHandeler.Current.RegisterListener(EventHandeler.EVENT_TYPE.ActivateNextQuest, TriggerNextQuest);
        lampRenderer = lamp.GetComponent<Renderer>();
        standardMaterial = lampRenderer.material;

        if(QuestComponent.IsCompleted(questIDRecuirement) && QuestComponent.IsCompleted(alternativeIDRequired))
        {
            lampRenderer.material = avalibleQuest;
        }
        if (nextQuestInLine != null)
            nextQuestInLine.enabled = false;
    }

    /// <summary>
    /// starts the quest by calling an ActivateQuestEventInfo;
    /// </summary>
    public void StartTheQuest(){
        if (lampRenderer == null)
            lampRenderer = lamp.GetComponent<Renderer>();
        ActivateQuestEventInfo aqei = new ActivateQuestEventInfo { eventQuestID = questID};
        EventHandeler.Current.FireEvent(EventHandeler.EVENT_TYPE.ActivateQuest, aqei);
        started = true;
        lampRenderer.material = activeQuest;
    }

    /// <summary>
    /// While the player stays in contact with the quest Giver and presses E:
    /// if the quest is complete nothing happens;
    /// if the quest is not yet avalible to the player nothing happens;
    /// if the requirements arent met for the quest then nothing hapens;
    /// otherwise the quest starts by calling startTheQuest();
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerStay(Collider other)
    {
        if (enabled == false)
            return;
        if (Input.GetButtonDown("Interact"))
        {
            ControllInput();
        }
    }

    private void TriggerNextQuest(EventInfo eventInfo)
    {
        if(QuestComponent.IsCompleted(questIDRecuirement) == true || QuestComponent.IsCompleted(alternativeIDRequired) == false && started == false)
        {
            lampRenderer = lamp.GetComponent<Renderer>();
            lampRenderer.material = avalibleQuest;
            Debug.Log("");//skriv ut ID på den questen som lyssnar på denna

        }

    }

    public void ControllInput()
    {
        if (enabled == true)
        {
            QuestDialogEventInfo questDialogEvent = new QuestDialogEventInfo { questText = CurrentStateDialog(), show = true };
            EventHandeler.Current.FireEvent(EventHandeler.EVENT_TYPE.QuestDialog, questDialogEvent);
        }

        if (QuestComponent.IsCompleted(questID))
        {
            TurnInQuest();
        }
        else if (QuestComponent.IsCompleted(questIDRecuirement) == false || QuestComponent.IsCompleted(alternativeIDRequired) == false && this.enabled == true)
        {
            return;
        }
        else if (started == false )
        {
            StartTheQuest();
        }

    }

    /// <summary>
    /// If the player enters contact with the quest giver then a InteractTriggerEvent is called;
    /// </summary>
    /// <param name="other">Gameobjects Collider</param>
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Quest giver with questID " + myQuest.QuestID + " Has been triggered enter");
        if (enabled == false)
            return;
        InteractTriggerEventInfo itei = new InteractTriggerEventInfo{ isInteractable = true };
        EventHandeler.Current.FireEvent(EventHandeler.EVENT_TYPE.Interact, itei);
    }

    /// <summary>
    /// If the player exit contact with the quest giver then a InteractTriggerEvent is called;
    /// </summary>
    /// <param name="other">Gameobjects Collider</param>
    private void OnTriggerExit(Collider other)
    {
        if (enabled == false)
            return;
        InteractTriggerEventInfo itei = new InteractTriggerEventInfo { isInteractable = false };
        EventHandeler.Current.FireEvent(EventHandeler.EVENT_TYPE.Interact, itei);

        QuestDialogEventInfo questDialogEvent = new QuestDialogEventInfo { questText = CurrentStateDialog(), show = false };
        EventHandeler.Current.FireEvent(EventHandeler.EVENT_TYPE.QuestDialog, questDialogEvent);
    }

    private void TurnInQuest()
    {
        lampRenderer.material = standardMaterial;
        if (turnedIn != true)
        {
            CompleteQuestEventInfo cqei = new CompleteQuestEventInfo { eventQuestID = questID };
            EventHandeler.Current.FireEvent(EventHandeler.EVENT_TYPE.CompleteQuest, cqei);

            RewardQuestInfo rei = new RewardQuestInfo { rewardNumber = myQuest.RewardID };
            EventHandeler.Current.FireEvent(EventHandeler.EVENT_TYPE.QuestReward, rei);

            EventHandeler.Current.UnregisterListener(EventHandeler.EVENT_TYPE.QuestGoalReached, QuestCompleted);
            EventHandeler.Current.UnregisterListener(EventHandeler.EVENT_TYPE.AvailableQuest, MakeQuestAvalible);
        }
        else if (nextQuestInLine != null && turnedIn == true)
        {
            nextQuestInLine.enabled = true;
            //Debug.Log("Im do the nexInLine DoMe()");

            nextQuestInLine.ControllInput();
        }
        turnedIn = true;
    }

    /// <summary>
    /// Reacts to a callback event CompleteQuestEventInfo;
    /// This then changes material for the indicator and proceeds to set the indicator to inactive;
    /// </summary>
    /// <param name="eventInfo">CompleteEventInfo;</param>
    private void QuestCompleted(EventInfo eventInfo)
    {
        GoalReachedEventInfo grei = (GoalReachedEventInfo)eventInfo;
        //CompleteQuestEventInfo grei = (CompleteQuestEventInfo)eventInfo;
        if (grei.completedQuestID == questID && enabled == true)
        {
            lampRenderer.material = completedQuest;
        }
    }

    private void MakeQuestAvalible(EventInfo eventinfo)
    {

        //Debug.Log("This is the current active status: " + enabled);
        if (QuestComponent.IsCompleted(alternativeIDRequired) && QuestComponent.IsCompleted(questIDRecuirement) && started != true)
        {
            lampRenderer.material = avalibleQuest;
        }
    }


    private string CurrentStateDialog()
    {
        myQuest = QuestComponent.GetQuestFromID(questID);
        if (myQuest == null)
            return "There is no quest";

        if (QuestComponent.IsCompleted(questID))
        {
            return myQuest.QuestCompleteDescription;
        }
        else if (QuestComponent.IsCompleted(questIDRecuirement) == false && QuestComponent.IsCompleted(alternativeIDRequired))
        {
            return "You don't meet the requirements for the quest";
        }
        else if (started != true)
        {
            return myQuest.QuestDescription;
        }
        else if(started == true)
        {
            return myQuest.QuestActiveDescription;
        }
        return "";
    }


}
