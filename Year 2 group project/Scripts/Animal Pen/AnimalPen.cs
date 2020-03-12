using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Main Author: Marcus Lundqvist

public class AnimalPen : MonoBehaviour
{
    public float PenArea { get { return penArea; } }
    public float CowCollectionDistance { get { return cowCollectionDistance; } }

    [SerializeField] private float penArea;
    [SerializeField] private float cowCollectionDistance;
    [SerializeField] private List<GameObject> cowsInPen;

    /// <summary>
    /// Adds the <see cref="GameObject"/> to the <see cref="GameComponents.AnimalPens"/> list.
    /// </summary>
    void Start()
    {
        GameComponents.AnimalPens.Add(gameObject);
    }

    
}
