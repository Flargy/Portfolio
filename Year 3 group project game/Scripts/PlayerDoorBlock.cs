using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Main Author: Marcus Lundqvist

public class PlayerDoorBlock : MonoBehaviour
{
    [SerializeField] private GameObject otherCollider = null;

    private bool done = false;

    /// <summary>
    /// Checks which player enters the trigger zone and activates <see cref="ChangeLayer(string)"/>.
    /// </summary>
    /// <param name="other">The object entering the trigger zone</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && done == false)
        {
            if(other.gameObject.layer == LayerMask.NameToLayer("Player1"))
            {
                ChangeLayer("AcceptPlayer1");
                otherCollider.GetComponent<PlayerDoorBlock>().ChangeLayer("AcceptPlayer2");
            }
            else if(other.gameObject.layer == LayerMask.NameToLayer("Player2"))
            {
                ChangeLayer("AcceptPlayer2");
                otherCollider.GetComponent<PlayerDoorBlock>().ChangeLayer("AcceptPlayer1");
            }
        }
    }

    /// <summary>
    /// Changes the layer of the trigger zone and makes it stop being a trigger.
    /// </summary>
    /// <param name="layerName"></param>
    public void ChangeLayer(string layerName)
    {
        gameObject.layer = LayerMask.NameToLayer(layerName);
        done = true;
        gameObject.GetComponent<BoxCollider>().isTrigger = false;
    }
}
