using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Main Author: Marcus Lundqvist

public class SplashTimer : MonoBehaviour
{
    [SerializeField] float timeDelayed = 0;
    [SerializeField] bool goesToMain = false;

    /// <summary>
    /// Starts the coroutine.
    /// </summary>
    void Start()
    {
        StartCoroutine(SceneDelay());
    }

    /// <summary>
    /// Creates a delay before changing scene.
    /// </summary>
    /// <returns></returns>
    private IEnumerator SceneDelay()
    {
        yield return new WaitForSeconds(timeDelayed);
        if(goesToMain == false) 
        { 
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
        }
        else
        {
            SceneManager.LoadScene(1);
        }
    }

}
