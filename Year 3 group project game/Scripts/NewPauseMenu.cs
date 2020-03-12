using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class NewPauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject parent = null;
    [SerializeField] private GameObject pauseMenuHolder = null;
    [SerializeField] private GameObject optionsMenuHolder = null;
    [SerializeField] private GameObject screenTint = null;
    [SerializeField] private GameObject controls = null;
    [SerializeField] private GameObject controlBack = null;
    [SerializeField] private GameObject resumeButton = null;
    [SerializeField] private GameObject backButton = null;
    [SerializeField] private GameObject devHolder = null;
    [SerializeField] private GameObject devBack = null;
    [SerializeField] private Toggle outlineState = null;
    [SerializeField] private EventSystem eventSys = null;
    [SerializeField] private Text movementText = null;
    [SerializeField] private Text QTENumber = null;

    private NewPlayerScript player1= null;
    private NewPlayerScript player2 = null;
    private NewPlayerScript currentPlayer = null;
    private string trueNorth = "True North";
    private string screenNorth= "Screen North";
    private bool player1North = false;
    private bool player2North = false;
    private float QTETimeAdd = 0.0f;

    public static NewPauseMenu Instance;

    private void Awake()
    {
         
        Instance = this;
        DontDestroyOnLoad(parent);
        SceneManager.sceneLoaded += OnLevelLoad;
        current = this;

    }

    static private NewPauseMenu current;
    static public NewPauseMenu Current {
        get {
            if (current == null)
            {
                current = GameObject.FindObjectOfType<NewPauseMenu>();
            }
            return current;
        }
    }

    private void OnLevelLoad(Scene scene, LoadSceneMode sceneMode)
    {
        if(gameObject != null) { 
            pauseMenuHolder.SetActive(false);
            optionsMenuHolder.SetActive(false);
            screenTint.SetActive(false);
            controls.SetActive(false);
            player1 = GameObject.Find("Player 1").GetComponent<NewPlayerScript>();
            player2 = GameObject.Find("Player 2").GetComponent<NewPlayerScript>();
            player1.SetTrueNorth(player1North);
            player2.SetTrueNorth(player2North);
            if (outlineState.isOn)
            {
                SwapOutline();
            }
            if(SceneManager.GetActiveScene().buildIndex == 1 || SceneManager.GetActiveScene().buildIndex == 9 || SceneManager.GetActiveScene().buildIndex == 10)
            {
                Destroy(parent);
            }
            FireTimeEvent();
                
        }


    }

    private void Start()
    {
        player1 = GameObject.Find("Player 1").GetComponent<NewPlayerScript>();
        player2 = GameObject.Find("Player 2").GetComponent<NewPlayerScript>();
    }

    public void OnStartFromNewPlayerScript(NewPlayerScript player)
    {
        pauseMenuHolder.SetActive(true);
        screenTint.SetActive(true);

        eventSys.SetSelectedGameObject(resumeButton);
        Time.timeScale = 0f;

        currentPlayer = player;

        if (currentPlayer.GetNorth() == true)
        {
            movementText.text = screenNorth;
        }
        else
        {
            movementText.text = trueNorth;
        }

        if (player == player1)
        {
            player2.SwapToPaused();
        }
        else
        {
            player1.SwapToPaused();
        }
    }

    public void ResumeGame()
    {

        pauseMenuHolder.SetActive(false);
        screenTint.SetActive(false);
        Time.timeScale = 1f;
        player1.SwapToGameplayAM();
        player2.SwapToGameplayAM();


    }

    public void RestartLevel()
    {
        pauseMenuHolder.SetActive(false);
        screenTint.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    public void OpenOptions()
    {
        eventSys.SetSelectedGameObject(backButton);
    }

    public void CloseOptions()
    {

        eventSys.SetSelectedGameObject(resumeButton);
    }

    public void SelectMoveType()
    {
        if(movementText.text != screenNorth)
        {
            movementText.text = screenNorth;
            
        }
        else
        {
            movementText.text = trueNorth;
        }

        currentPlayer.SwapTrueNorth();

        if(currentPlayer.gameObject.name == "Player 1")
        {
            player1North = currentPlayer.GetNorth();
        }
        else
        {
            player2North = currentPlayer.GetNorth();
        }

    }

    public void SwapOutline()
    {
        SwapOutlineEventInfo soei = new SwapOutlineEventInfo();
        EventHandeler.Current.FireEvent(EventHandeler.EVENT_TYPE.SwapOutlineEvent, soei);
    }

        public void ShowControls()
    {
        eventSys.SetSelectedGameObject(controlBack.gameObject);
    }

    public void HideControls()
    {
        eventSys.SetSelectedGameObject(backButton.gameObject);
    }

    public void GoToMain()
    {
        SceneManager.LoadScene(1);
        Destroy(parent);
    }

    public void OpenDev()
    {

        eventSys.SetSelectedGameObject(devBack.gameObject);

    }

    public void HideDev()
    {
        eventSys.SetSelectedGameObject(backButton.gameObject);
    }

    public void IncreaseTime()
    {
        QTETimeAdd = Mathf.Min(2, QTETimeAdd + 0.1f);
        QTENumber.text = QTETimeAdd.ToString("f1");
        FireTimeEvent();
    }

    public void DecreaseTime()
    {
        QTETimeAdd = Mathf.Max(-1, QTETimeAdd - 0.1f);
        QTENumber.text = QTETimeAdd.ToString("f1");
        FireTimeEvent();
    }

    private void FireTimeEvent()
    {
        ChangeQTEEventInfo cqei = new ChangeQTEEventInfo() {time = QTETimeAdd };
        EventHandeler.Current.FireEvent(EventHandeler.EVENT_TYPE.ChangeQTEEvent, cqei);
    }


}
