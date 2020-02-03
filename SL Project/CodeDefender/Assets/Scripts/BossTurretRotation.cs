using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTurretRotation : MonoBehaviour
{
    [SerializeField] private GameObject orbitPoint;

    void Update()
    {
        gameObject.transform.RotateAround(orbitPoint.transform.position, Vector3.up,  30f * Time.deltaTime);
    }
}
