using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MMButton : MonoBehaviour {

    public GameObject windowFrame;
    public UnityEngine.UI.Text htpText;
    public GameObject optionGrp;
    public GameObject htpGrp;
    public UnityEngine.UI.Button nextButton;
    public UnityEngine.UI.Button backButton;
    public GameObject ScreenPanel;
    public GameObject checkPanel;
    int next = 0;
    int sceneToLoad;
    int resetWhat;
    static bool screenTrigger = true;
    public bool soundStatus = true;

    private string[] textList = { "Controll your characters movement by using the joystick to the left. Aim your gun with the joystick to the right and your gun will fire as long as the joystick is held down. You cant lose health while you still have shield.",
                                    "Killed enemies have a chance to drop a power up. A red healthpack that restores health, a green suringe that inceases movement speed, a purple mark that increases firerate and a crate with more ammo.",
                                    "Your character have three kinds of weapons: a handgun with endless ammo and two weapons with limited ammo. A shotgun with high firepower and a riffle with high fire rate. Swap weapons with the weapon button." };

    public void Awake()
    {
        ScreenPanel.SetActive(screenTrigger);
        sceneToLoad = PlayerPrefs.GetInt("sceneToLoad", 1);
        if (sceneToLoad == 0)
        {
            PlayerPrefs.SetInt("sceneToLoad", 1);
            sceneToLoad = 1;
        }
        if (sceneToLoad > 3)
        {
            sceneToLoad = 1;
            PlayerPrefs.SetInt("sceneToLoad", 1);
        }
    }
   

    public void playGame()
    {
        //gör en animation som ska spelas innan spelaren kommer till nästa scene
        //Ladda in scenen som tar spelaren till spel scenen
        SceneManager.LoadScene("LoadingScreen", LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }

    public void optionPanel()
    {
        windowFrame.SetActive(true);
        optionGrp.SetActive(true);
        //Visa vad för optuions som spelaren kan ändra
    }

    public void exitGame()
    {
        //stänger av applikationen
        Application.Quit();
    }

    public void howToPlay()
    {
        //Visar upp ett fönster som säger hur man spelare spelet
        windowFrame.SetActive(true);
        htpGrp.SetActive(true);
        htpText.text = textList[next];
    }
    
    public void nextSlide()
    {
        if (next == 0)
        {
            next++;
            htpText.text = textList[next];
            backButton.gameObject.SetActive(true);
        }else if(next == 1)
        {
            next++;
            htpText.text = textList[next];
            nextButton.gameObject.SetActive(false);
        }

    }

    public void backSlide()
    {
        if(next == 1)
        {
            next--;
            htpText.text = textList[next];
            backButton.gameObject.SetActive(false);
        }
        if (next == 2)
        {
            next--;
            htpText.text = textList[next];
            nextButton.gameObject.SetActive(true);
        }
    }

    public void closeFrame()
    {
        checkPanel.SetActive(false);
        //Stänger fönstret som visar hhur man spelar
        windowFrame.SetActive(false);
        optionGrp.SetActive(false);
        nextButton.gameObject.SetActive(true);
        backButton.gameObject.SetActive(false);
        next = 0;
        htpGrp.SetActive(false);
    }

    public void continueTOMM()
    {
        screenTrigger = !screenTrigger;
        ScreenPanel.SetActive(screenTrigger);
    }

    public void resetLevels()
    {
        checkPanel.SetActive(true);
        resetWhat = 1;
    }

    public void resetAll()
    {
        checkPanel.SetActive(true);
        resetWhat = 2;
    }

    public void resetConfirmed()
    {
        if (resetWhat == 1)
        {
            PlayerPrefs.SetInt("sceneToLoad", 1);
            closeFrame();
        }
        else if (resetWhat == 2)
        {
            PlayerPrefs.SetInt("Rank", 1);
            PlayerPrefs.SetInt("sceneToLoad", 1);
            GetComponent<PlayerPrestige>().resetRank();
            closeFrame();
        }
    }

    public void soundSet()
    {
        soundStatus = !soundStatus;
    }


}
