using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;





public class SaveSystem : MonoBehaviour {
    private int health;
    private int shield;
    private int credits;
    private int currencyValueMultiplier;
    public GameObject player;


    public void SavePlayer()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/Stats.dat");
        Stats stats = new Stats(health, shield, credits, currencyValueMultiplier);

        stats.credits = Currency.credits;
        stats.currencyValueMultiplier = Currency.currencyValueMultiplier;
        stats.health = player.GetComponent<PlayerVariables>().health;
        stats.shield = player.GetComponent<PlayerVariables>().shield;
        bf.Serialize(file, stats);
        file.Close();
        Debug.Log("Saving");
    }
    public void LoadPlayer()
    {
        if (File.Exists(Application.persistentDataPath + "/Stats.dat"))
        {

            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/Stats.dat", FileMode.Open);
            Stats stats = (Stats)bf.Deserialize(file);
            file.Close();
            Currency.credits = stats.credits;
            Currency.currencyValueMultiplier = stats.currencyValueMultiplier;
            player.GetComponent<PlayerVariables>().health = stats.health;
            player.GetComponent<PlayerVariables>().shield = stats.shield;
            Debug.Log("Loading");
        }
    }
}

[Serializable]
class Stats
{
    public int health;
    public int shield;
    public int credits;
    public float currencyValueMultiplier;


    public Stats(int health, int shield, int credits, float currencyValueMultiplier)
    {
        this.health = health;
        this.shield = shield;
        this.credits = credits;
        this.currencyValueMultiplier = currencyValueMultiplier;
    }
    public int getHealth()
    {
        return health;
    }
    public int getShield()
    {
        return shield;
    }

}
