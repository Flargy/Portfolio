using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Data 
{
    public int Health { get; set; }
    public float[] Position { get; set; }

    public Data(PlayerStateMashine player)
    {
        Health = player.Health;

        Position = new float[3];
        Position[0] = player.transform.position.x;
        Position[1] = player.transform.position.y;
        Position[2] = player.transform.position.z;
    }
}
