
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GameDataController : MonoBehaviour
{
    public static SaveData2 SaveInformation;

    private void Awake()
    {
        LoadData();
    }

    [ContextMenu("Save Data")]
    public void SaveGame()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/save.info";
        FileStream stream = File.Create(path);
        //FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, SaveInformation);
        stream.Close();
        Debug.Log("Saved");
        //    var data = JsonUtility.ToJson(SaveInformation);
        //    PlayerPrefs.SetString("GameData", data);
    }

    [ContextMenu("Load Data")]
    public void LoadData()
    {
        string path = Application.persistentDataPath + "/save.info";
        if (File.Exists(path))
        {
            Debug.Log("successful load");
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SaveInformation = (SaveData2)formatter.Deserialize(stream);
            stream.Close();

        }

        //var data = PlayerPrefs.GetString("GameData");
        //SaveInformation = JsonUtility.FromJson<SaveData2>(data);
    }

    private void OnDisable()
    {
        SaveGame();
    }

    public static CowsData GetCowState(CowSM cow)
    {
        CowsData cowData = new CowsData() { Id = null };

        if (SaveInformation.CowInfoList == null)
            return cowData;

        if (SaveInformation.CowInfoList.Any(t => t.Id == cow.name))
        {
            return SaveInformation.CowInfoList.FirstOrDefault(t => t.Id == cow.name);
        }



        return cowData ;
    }

    public static void SetCowState(CowSM cowInfo)
    {
        if (SaveInformation.CowInfoList == null)
            SaveInformation.CowInfoList = new List<CowsData>();

        CowsData cowData = new CowsData() { Id = cowInfo.name, Position = new float[3], StateIndex = cowInfo.CurrentState.Index};
        Debug.Log(cowInfo.CurrentState.Index);
        cowData.Position[0] = cowInfo.transform.position.x;
        cowData.Position[1] = cowInfo.transform.position.y;
        cowData.Position[2] = cowInfo.transform.position.z;

        SaveInformation.CowInfoList.RemoveAll(t => t.Id == cowData.Id);
        SaveInformation.CowInfoList.Add(cowData);
    }

    public static void SetPlayerState(PlayerStateMashine playerInfo)
    {
        SaveInformation.CurrentHealth = playerInfo.Health;

        SaveInformation.Position = new float[3];

        SaveInformation.Position[0] = playerInfo.transform.position.x;
        SaveInformation.Position[1] = playerInfo.transform.position.y;
        SaveInformation.Position[2] = playerInfo.transform.position.z;

    }

    public static SaveData2 GetPlayerState(PlayerStateMashine playerInfo)
    {
      
        return SaveInformation;
        
    }

    public static WolvesData GetWolfState(AlphaStateMachine wolf)
    {
        WolvesData wolfData = new WolvesData() { Id = null };

        if (SaveInformation.WolfInfoList == null)
            return wolfData;

        if (SaveInformation.WolfInfoList.Any(t => t.Id == wolf.name))
        {
            return SaveInformation.WolfInfoList.FirstOrDefault(t => t.Id == wolf.name);
        }



        return wolfData;
    }

    public static void SetWolfState(AlphaStateMachine wolfInfo)
    {
        if (SaveInformation.WolfInfoList == null)
            SaveInformation.WolfInfoList = new List<WolvesData>();

        WolvesData wolfData = new WolvesData() { Id = wolfInfo.name, Position = new float[3]};
        wolfData.Position[0] = wolfInfo.transform.position.x;
        wolfData.Position[1] = wolfInfo.transform.position.y;
        wolfData.Position[2] = wolfInfo.transform.position.z;

        SaveInformation.WolfInfoList.RemoveAll(t => t.Id == wolfData.Id);
        SaveInformation.WolfInfoList.Add(wolfData);
    }

    public static GiantsData GetGiantState(GiantSM giant)
    {
        GiantsData giantData = new GiantsData() { Id = null };

        if (SaveInformation.GiantInfoList == null)
            return giantData;

        if (SaveInformation.GiantInfoList.Any(t => t.Id == giant.name))
        {
            return SaveInformation.GiantInfoList.FirstOrDefault(t => t.Id == giant.name);
        }
        
        return giantData;
    }

    public static void SetGiantState(GiantSM giant)
    {
        if (SaveInformation.WolfInfoList == null)
            SaveInformation.WolfInfoList = new List<WolvesData>();

        GiantsData giantData = new GiantsData() { Id = giant.name, Position = new float[3]};
        giantData.Position[0] = giant.transform.position.x;
        giantData.Position[1] = giant.transform.position.y;
        giantData.Position[2] = giant.transform.position.z;

        SaveInformation.GiantInfoList.RemoveAll(t => t.Id == giantData.Id);
        SaveInformation.GiantInfoList.Add(giantData);
    }
}