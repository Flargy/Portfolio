using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameController : MonoBehaviour {
    public Text amountOfEnemies, levelEnd, waveAnnouncement, gameOver;
    public GameObject spawner, levelEndPanel, completedLevelPanel, pauseScreen;
    public Button play;
    public Sprite pausedImage, unpausedImage;
    //public GameObject spawn;
    private static GameController control;
    [HideInInspector]
    public static bool missionFailed = false;
    private int wave;
    private GameObject player;
    private int nextLevel;

    private void Start()
    {
        
        player = GameObject.Find("hitman1_gun");

        //player.transform.position = spawn.transform.position;
        nextLevel = PlayerPrefs.GetInt("sceneToLoad", 1) + 1;
        Time.timeScale = 1;
        levelEnd.gameObject.SetActive(false);
        waveAnnouncement.gameObject.SetActive(false);
        gameOver.gameObject.SetActive(false);
        levelEndPanel.gameObject.SetActive(false);
        pauseScreen.gameObject.SetActive(false);
        completedLevelPanel.gameObject.SetActive(false);
        wave = 0;
        StartCoroutine(waveControl());
        missionFailed = false;
    }

    private void FixedUpdate()
    {
        
        if (MonsterSpawn.monstersSpawned && MonsterSpawn.numberOfMonsters == 0)
        {
            completedLevelPanel.gameObject.SetActive(true);
            levelEnd.gameObject.SetActive(true);
            PlayerPrefs.SetInt("sceneToLoad", nextLevel);
            levelEnded();

            if (PlayerPrefs.GetInt("sceneToLoad", 1) > 3)
            {
                PlayerPrefs.SetInt("sceneToLoad", 0);
                if (PlayerPrefs.GetInt("Rank", 1) < 6)
                {
                    PlayerPrefs.SetInt("Rank", PlayerPrefs.GetInt("Rank", 1) + 1);
                }
            }



        }

        amountOfEnemies.text = "" + MonsterSpawn.numberOfMonsters;

        if (wave < spawner.GetComponent<MonsterSpawn>().getCurrentWave())
        {
            StartCoroutine(waveControl());
            wave = spawner.GetComponent<MonsterSpawn>().getCurrentWave();
        }

        if (missionFailed)
        {
            gameOver.gameObject.SetActive(true);
            levelEndPanel.gameObject.SetActive(true);
            levelEnded();
        }

    }

    public void levelEnded()
    {
        
        Time.timeScale = 0f;
       
    }

    public void changeLevel()
    {
        if (player.GetComponent<PlayerVariables>().health < 20)
        {
            player.GetComponent<PlayerVariables>().health = 100;
        }
        SceneManager.LoadScene("LoadingScreen", LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }

    public void pauseContinueGame()
    {
        if (pauseScreen.activeSelf)
        {
            if (Time.timeScale == 0.0f)
                Time.timeScale = 1.0f;
            play.image.sprite = unpausedImage;
        }
        else
        {
            Time.timeScale = 0.0f;
            play.image.sprite = pausedImage;
        }
        pauseScreen.SetActive(!pauseScreen.activeSelf);

    }

    public void abandonMission()
    {
        SceneManager.LoadScene(0);
    }


    IEnumerator waveControl()
    {
        waveAnnouncement.gameObject.SetActive(true);
        waveAnnouncement.text = "Wave " + (spawner.GetComponent<MonsterSpawn>().getCurrentWave() + 1) + " incoming!";
        yield return new WaitForSeconds(2.0f);
        waveAnnouncement.gameObject.SetActive(false);
    }

    void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus && !pauseScreen.activeSelf)
        {   

            pauseContinueGame();
        }
    }
   
}
