using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Author: Marcus Lundqvist

public class DayAndNightSM : StateMachine
{
    public float NightTime { get { return nightTime; } }
    public GameObject Sun { get { return sun; } }
    public GameObject GameComponent { get { return gameComponent; } }
    public GameObject Moon { get { return moon; } }


    [SerializeField] private GameObject moon;
    [SerializeField] private float nightTime;
    [SerializeField] private GameObject sun;
    [SerializeField] private GameObject gameComponent;

    protected override void Awake()
    {
        base.Awake();
    }

    


}
