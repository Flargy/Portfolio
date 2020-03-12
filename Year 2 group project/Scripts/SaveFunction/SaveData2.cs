using System;
using System.Collections.Generic;

[System.Serializable]
public struct SaveData2
{
    public float CurrentHealth;
    public float[] Position;

    public List<CowsData> CowInfoList;
    public List<WolvesData> WolfInfoList;
    public List<GiantsData> GiantInfoList;
}

[System.Serializable]
public struct CowsData
{
    public string Id;
    public float[] Position;
    public int StateIndex;
}

[System.Serializable]
public struct WolvesData
{
    public string Id;
    public float[] Position;
    //public int CurrentState;
}

[System.Serializable]
public struct GiantsData
{
    public string Id;
    public float[] Position;
    //public int CurrentState;
}