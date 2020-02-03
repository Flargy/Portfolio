using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    [SerializeField] private int levelToLoad;
    public void ReloadGame()
    {
        SceneManager.LoadScene(levelToLoad);
    }
}
