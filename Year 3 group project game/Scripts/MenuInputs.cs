using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuInputs : MonoBehaviour
{
    public Toggle trueNorthToggle;
    [SerializeField] private NewPlayerScript player1 = null;
    [SerializeField] private NewPlayerScript player2 = null;
    [SerializeField] private GameObject pauseMenuUI;
    private Vector2 moveInput;
    private PlayerInput playerInput = null;
    private EventSystem es = null;
    private Button btn = null;
    private Scene scene;
    

    public static bool isGamePaused = false;
    private static NewPlayerScript currentPlayerPaused;

    public static MenuInputs Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        scene = SceneManager.GetActiveScene();

        //pauseMenuUI.SetActive(false);
    }


    //public void OnNavigate(InputValue input)
    //{
    //    moveInput = input.Get<Vector2>();
    //}


    //public void OnStart()
    //{
    //    pauseMenuUI.SetActive(true);
    //    btn = GameObject.Find("ResumeButton").GetComponent<Button>();
    //    es = GameObject.Find("PauseMenuEventSystem").GetComponent<EventSystem>();
    //    es.SetSelectedGameObject(btn.gameObject);
    //    playerInput.SwitchCurrentActionMap("Menu");
    //    Time.timeScale = 0f;
    //    isGamePaused = true;

    //}

    public void OnStartFromNewPlayerScript(NewPlayerScript player)
    {
        pauseMenuUI.SetActive(true);
        btn = GameObject.Find("ResumeButton").GetComponent<Button>();
        es = GameObject.Find("PauseMenuEventSystem").GetComponent<EventSystem>();
        es.SetSelectedGameObject(btn.gameObject);
        //playerInput.SwitchCurrentActionMap("Menu");
        Time.timeScale = 0f;
        isGamePaused = true;

        currentPlayerPaused = player;
        if (trueNorthToggle != null)
            trueNorthToggle.isOn = currentPlayerPaused.UsingScreenNorth;

        if(player == player1)
        {
            player2.SwapToPaused();
        }
        else
        {
            player1.SwapToPaused();
        }
    }

    public void PauseResume()
    {
        pauseMenuUI.SetActive(false);
        es.SetSelectedGameObject(null);
        Time.timeScale = 1f;
        //var allPlayerÏnputComponents = FindObjectsOfType<PlayerInput>();
        //foreach (var p in allPlayerÏnputComponents)
        //    p.SwitchCurrentActionMap("Gameplay");

        //playerInput.SwitchCurrentActionMap("Gameplay");
        player1.SwapToGameplayAM();
        player2.SwapToGameplayAM();
        isGamePaused = false;

    }

    public void OptionsButtonBackSelection()
    {
        btn = GameObject.Find("BackButton").GetComponent<Button>();
        es = GameObject.Find("PauseMenuEventSystem").GetComponent<EventSystem>();
        es.SetSelectedGameObject(btn.gameObject);
    }
    
    public void MainMenuOptionsBackSelection()
    {
        btn = GameObject.Find("BackButton").GetComponent<Button>();
        es = GameObject.Find("MainMenuEventSystem").GetComponent<EventSystem>();
        es.SetSelectedGameObject(btn.gameObject);
    }

    public void ResumeButtonSelection()
    {
        btn = GameObject.Find("ResumeButton").GetComponent<Button>();
        es = GameObject.Find("PauseMenuEventSystem").GetComponent<EventSystem>();
        es.SetSelectedGameObject(btn.gameObject);
    }

    public void MainMenuPlayButtonSelection()
    {
        btn = GameObject.Find("PlayButton").GetComponent<Button>();
        es = GameObject.Find("MainMenuEventSystem").GetComponent<EventSystem>();
        es.SetSelectedGameObject(btn.gameObject);
    }

    public void ToggleHandler(bool value)
    {
        currentPlayerPaused.SetTrueNorth(value);
        Debug.Log("toggle for " + currentPlayerPaused.name + " value: " + value);
    }


    public void RestartCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    /*
     * -------------------
     * MAIN MENU FUNCTIONS 
     * --------------------
     * --------------------
     */

    public void PlayButton()
    {
        Debug.Log("Level loaded");
        //SceneManager.LoadScene("Level_1_1");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);//Eku

    }


    public void QuitBUtton()
    {
        Debug.Log("Closing game");
        Application.Quit();
    }

    public void ControllerBackButton()
    {

    }

    public void SwapOutline()
    {
        SwapOutlineEventInfo soei = new SwapOutlineEventInfo();
        EventHandeler.Current.FireEvent(EventHandeler.EVENT_TYPE.SwapOutlineEvent, soei);
    }
}
