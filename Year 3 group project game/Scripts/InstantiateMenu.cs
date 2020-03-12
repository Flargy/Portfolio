using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu = null;
    void Start()
    {
        if(NewPauseMenu.Current == null)
        {
            Instantiate(pauseMenu);
        }
    }

   
}
