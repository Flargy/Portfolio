using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxInteractionPlacement : MonoBehaviour
{
    [SerializeField] private GameObject parent = null;

    void Update()
    {
        if(parent.activeSelf == true)
        {
            transform.position = parent.transform.position + Vector3.up;
            transform.rotation = Quaternion.Euler(0, 0, 0);
            
        }
    }
}
