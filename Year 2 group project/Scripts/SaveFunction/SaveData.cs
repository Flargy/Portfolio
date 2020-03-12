using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData : MonoBehaviour
{

    //This class will hold all of the data that is going to be saved whenthe player decides to save the game.
    //Game components that will have their values scanned at the save
    [SerializeField] private GameObject celestialHolder;

    //Wold variables
    private Vector3 sunPosition = Vector3.zero;
    private Vector3 moonPosition = Vector3.zero;
    private bool day;

    //Player Variables
    private Vector3 playerPosition = Vector3.zero;
    private float playerHealth = 100;
    private List<int> completedQuests = new List<int>();
    private float capturedCows = 0;
    private float torches = 0;
    //index int, första vector3 är pos spawner, spawner gameobject pos = andra vector3
    private Dictionary<int, Dictionary<Vector3, Vector3>> SpawnerObject = new Dictionary<int, Dictionary<Vector3, Vector3>>();


    /// <summary>
    /// This method is called when the player wants to save their game.
    /// It will register all the data that the save file requires and then write that down to a save file that later can be read.-
    /// </summary>
    public void MakeNewSave()
    {
        //Hämtar hem de värden den behöver från spelet och sedan skriver ner dem på en txt fil så man kan läsa in dem senare
        playerPosition = GetComponent<PlayerStateMashine>().transform.position;
        playerHealth = GetComponent<PlayerStateMashine>().Health;
        completedQuests = QuestComponent.GetCompletedQuestList();
        sunPosition = celestialHolder.GetComponent<DayAndNightSM>().Sun.transform.position;
        moonPosition = celestialHolder.GetComponent<DayAndNightSM>().Moon.transform.position;
        //idex 1, (Spawner.transform.position, spawner.object.transform.position).

    //spara ner värden i en fil

    }
    /// <summary>
    /// This will read all the data that the saved file has and will then apply it to the current active scene.
    /// </summary>
    public SaveData LoadSave()
    {
        //Tar fram sparade värden och sedan appliserar dem till spelaren efter att allt har laddats in och innan spelaren börjar spela sitt spel. fär göra så att detta scripts kör först ifall spelaren väljer att köra en sparad fil.
        //return those
        return null;
    }

    public void LoadData()
    {
       // this = SaveData();
    }

    public void ApplyValues()
    {
       // if(!day)
      //  foreach{ boll in gianlist}
      //  boll.transTO<NightState>();
    }

    public void Start()
    {
     //registrering av lyssnare ifall det nu ska användas för att spelare automatiskt ska spara olika variabler.   
    }
}
