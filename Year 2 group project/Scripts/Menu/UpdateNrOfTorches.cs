using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Main Author: Marcus Lundqvist

public class UpdateNrOfTorches : MonoBehaviour
{
    [SerializeField] private Text textObject;

    private int nrOfTorches;

    void Start()
    {
        textObject.text = nrOfTorches.ToString();
        EventHandeler.Current.RegisterListener(EventHandeler.EVENT_TYPE.TorchPickup, AddTorch);
        EventHandeler.Current.RegisterListener(EventHandeler.EVENT_TYPE.TorchDepleted, RemoveTorch);

    }

    private void AddTorch(EventInfo torchInfo)
    {
        nrOfTorches++;
        textObject.text = nrOfTorches.ToString();
    }

    private void RemoveTorch(EventInfo torchInfo)
    {
        nrOfTorches--;
        textObject.text = nrOfTorches.ToString();
    }

}
