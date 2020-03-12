using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//Main Author: Hjalmar Andersson
//Secondary Author: Alishia Nossborn

public class GameController : MonoBehaviour
{
    public static GameController gameControllerInstance;
    public Slider playerHealthBar;
    public Slider torchFuseBar;
    public Slider shoutCD;

    public List<Sprite> runes = new List<Sprite>();
    private Rune[] activeRunes = new Rune[3];

    public Transform playerPosition;

    public GameObject pausePanel;

    [SerializeField] private GameObject questPanel;
    [SerializeField] private GameObject book;

    public Text torchAmount;
    public Text heathAmount;

    public Image thunderRune;
    public Image calmRune;
    public Image locateRune;
    public Image runeSprite;
    public Image runeBackground;
    public Rune currentRune;

    private float cdTimer1;
    private float cdTimer2;
    private float cdTimer3;
    public float torchTimer;

    private bool activeTorch = false;
    private int torches;
    float temp = 0;

    // Start is called before the first frame update
    void Start()
    {
        pausePanel.SetActive(false);
        torchFuseBar.maxValue = torchTimer;
        shoutCD.gameObject.SetActive(false);
        runeSprite.enabled = false;
        runeBackground.enabled = false;
        gameControllerInstance = this;
        DisEnableAllRunes();
        torchFuseBar.gameObject.SetActive(false);
        EventHandeler.Current.RegisterListener(EventHandeler.EVENT_TYPE.ChangeRune, ChangeRune);
        EventHandeler.Current.RegisterListener(EventHandeler.EVENT_TYPE.TorchPickup, IncreaseTorch);
        EventHandeler.Current.RegisterListener(EventHandeler.EVENT_TYPE.TorchActivation, DecreaseTorch);
        EventHandeler.Current.RegisterListener(EventHandeler.EVENT_TYPE.Damage, TakeDamage);
        EventHandeler.Current.RegisterListener(EventHandeler.EVENT_TYPE.Song, ShoutHasBeenMade);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Pause"))
        {
            if (questPanel.active) { 
                questPanel.SetActive(false);
            }else if (book.active)
            {
                book.SetActive(false);
            }
            else
            { 
                if (pausePanel.active)
                    UnPauseGame();
                else
                {
                    pausePanel.SetActive(true);
                    PauseTheGame();
                }
            }
        }
        if (activeTorch == true)
        {
            torchFuseBar.value += Time.deltaTime;
            if (torchFuseBar.value >= torchFuseBar.maxValue)
            {
                torchFuseBar.gameObject.SetActive(false);
                activeTorch = false;
            }
        }
        if(currentRune != null) {
            if (currentRune.ReadyToUse())
            {
                switch (currentRune.GetRuneValue())
                {
                    case 1:
                        thunderRune.fillAmount = 1;
                        break;
                    case 2:
                        calmRune.fillAmount = 1;
                        break;
                    case 3:
                        locateRune.fillAmount = 1;
                        break;
                }
            }
            else
            {
                switch (currentRune.GetRuneValue())
                {
                    case 1:
                        thunderRune.fillAmount = cdTimer1;
                        break;
                    case 2:
                        calmRune.fillAmount = cdTimer2;
                        break;
                    case 3:
                        locateRune.fillAmount = cdTimer3;
                        break;
                }
            }

        }
    }

    private void FixedUpdate()
    {

        if (currentRune != null)
        {
            switch (currentRune.GetRuneValue())
            {
                case 1:
                    thunderRune.fillAmount = cdTimer1;
                    break;
                case 2:
                    calmRune.fillAmount = cdTimer2;
                    break;
                case 3:
                    locateRune.fillAmount = cdTimer3;
                    break;
            }
        }
        CalculateCoolDownForRunes();
        
    }

    private void CalculateCoolDownForRunes()
    {
        if (activeRunes[0] != null && !activeRunes[0].ReadyToUse())
        {
            cdTimer1 += 1 / activeRunes[0].GetCooldown() * Time.deltaTime;
            if (cdTimer1 >= 1)
            {
                activeRunes[0].CooldownFinish();
                cdTimer1 = 0f;
            }
        }
        else
            cdTimer1 = 0f;

        if (activeRunes[1] != null && !activeRunes[1].ReadyToUse())
        {
            cdTimer2 += 1 / activeRunes[1].GetCooldown() * Time.deltaTime;
            if (cdTimer2 >= 1)
            {
                activeRunes[1].CooldownFinish();
                cdTimer2 = 0f;
            }
        }
        else
            cdTimer2 = 0f;

        if (activeRunes[2] != null && !activeRunes[2].ReadyToUse())
        {
            cdTimer3 += 3 / activeRunes[2].GetCooldown() * Time.deltaTime;
            if (cdTimer3 >= 1)
            {
                activeRunes[2].CooldownFinish();
                cdTimer3 = 0f;
            }
        }
        else
            cdTimer3 = 0f;
    }

    private void ChangeRune(EventInfo eventInfo)
    {
        runeSprite.enabled = true;
        runeBackground.enabled = true;
        ChangeRuneEventInfo changeRuneInfo = (ChangeRuneEventInfo)eventInfo;
        currentRune = changeRuneInfo.newRune;
        Debug.Log("Changeing from currentrune value " + currentRune.GetRuneValue());
        DisEnableAllRunes();
        switch (currentRune.GetRuneValue())
        {
            case 1:
                ChangeRuneSprite(changeRuneInfo.newRune);
                thunderRune.enabled = true;
                break;
            case 2:
                ChangeRuneSprite(changeRuneInfo.newRune);
                calmRune.enabled = true;
                break;
            case 3:
                ChangeRuneSprite(changeRuneInfo.newRune);
                locateRune.enabled = true;
                break;
            default:
                runeSprite.sprite = null;
                break;
        }
    }

    private void ChangeRuneSprite(Rune newRune)
    {
        runeSprite.sprite = runes[newRune.GetRuneValue()-1];
        if(activeRunes[newRune.GetRuneValue()-1] == null)
            activeRunes[newRune.GetRuneValue()-1] = newRune;
    }
    private void DisEnableAllRunes()
    {
        thunderRune.enabled = false;
        calmRune.enabled = false;
        locateRune.enabled = false;
    }
    
    private void TakeDamage(EventInfo eventInfo)
    {
        Debug.Log("healthbar value before " + playerHealthBar.value);
        DamageEventInfo DamageInfo = (DamageEventInfo)eventInfo;
        playerHealthBar.value -= DamageInfo.damage * 10;
        heathAmount.text = "Health: " + playerHealthBar.value;
        Debug.Log("healthbar value after " + playerHealthBar.value);

    }

    private void IncreaseTorch(EventInfo eventInfor)
    {
        torches++;
        torchAmount.text = torches.ToString();
    }
    
    private void DecreaseTorch(EventInfo eventInfor)
    {
        if (activeTorch == false && torches != 0) {
            activeTorch = true;
            torches--;
            torchAmount.text = torches.ToString();
            torchFuseBar.value = 0;
            torchFuseBar.gameObject.SetActive(true);
        }
    }

    private void ShoutHasBeenMade(EventInfo eventInfo)
    {
        ShoutEventInfo shoutInfo = (ShoutEventInfo)eventInfo;
        shoutCD.gameObject.SetActive(true);
        shoutCD.maxValue = shoutInfo.shoutDuration;
        shoutCD.value = shoutInfo.shoutDuration;
        StartCoroutine(ShoutCDTimer());
    }

    private IEnumerator ShoutCDTimer()
    {
        while (shoutCD.value >= 0.5)
        {
            shoutCD.value -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        shoutCD.gameObject.SetActive(false);
    }


    private void PauseTheGame()
    {
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }
    public void UnPauseGame()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}


