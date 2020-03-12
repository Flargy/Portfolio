using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Main Author: Marcus Lundqvist

public class InteractionLever : Interactable
{
    [SerializeField] private List<AffectedObject> affected = null;
    [SerializeField] private GameObject rendererHolder = null;
    [SerializeField] private GameObject clockHand = null;
    [SerializeField] private List<Sprite> sprites = null;
    [SerializeField] private GameObject sceneCamera = null;
    [SerializeField] private GameObject leverAxis = null;
    [SerializeField] private float QTETimer = 3.0f;
    [SerializeField] private float cutoffTime = 0.15f;
    [SerializeField] private List<Interactable> connectedSwitches = null;
    [SerializeField] private AudioSource correct;
    [SerializeField] private AudioSource incorrect;

    private PlayerQTE interactingPlayer = null;
    private int correctAnswer = 0;
    private int playerAnswer = 0;
    //private bool interacting = false;
    private bool abortQTE = false;
    private float originalQTETimer = 0.0f;
    private bool takeInput = false;
    private bool playerHasAnswered = false;
    private SpriteRenderer renderQTE = null;
    private Coroutine activateQTE = null;
    private Coroutine leverRotation = null;
    private float timeAdd = 0.0f;
    float t = 0;
    float leverPullDownTime = 0.0f;

    private AudioSource audioSource;
    public AudioClip pushDownLever;
    public AudioClip releaseLever;

    /// <summary>
    /// Sets starting values.
    /// </summary>
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        renderQTE = rendererHolder.GetComponent<SpriteRenderer>();
        rendererHolder.SetActive(false);
        originalQTETimer = QTETimer;
    }

    private void Awake()
    {
        EventHandeler.Current.RegisterListener(EventHandeler.EVENT_TYPE.ChangeQTEEvent, ChangeTimeAdd);
    }

    private void ChangeTimeAdd(EventInfo info)
    {
        ChangeQTEEventInfo cqei = (ChangeQTEEventInfo)info;
        timeAdd = cqei.time;
    }

    /// <summary>
    /// Initiates the startup for the quick time event and activates the affected object.
    /// </summary>
    /// <param name="player"></param>
    public override void Interact(GameObject player)
    {
        if (interacting == false)
        {
            foreach (Interactable otherSwitch in connectedSwitches)
            {
                if (otherSwitch.interacting == false)
                {
                    otherSwitch.StopInteraction();
                }
            }
            
            interacting = true;
            interactingPlayer = player.GetComponent<PlayerQTE>();
            interactingPlayer.GetComponent<NewPlayerScript>().SwapLiftingState();
            interactingPlayer.SwapToQTE(this, gameObject);
            
            StartCoroutine(AnimationDelay());
        }
    }

    /// <summary>
    /// Receives input from the interacting player and activates <see cref="CheckAnswer"/>.
    /// </summary>
    /// <param name="answerID"></param>
    public void ReceiveAnswer(int answerID)
    {
        if(takeInput == true)
        {
            takeInput = false;
            playerHasAnswered = true;
            playerAnswer = answerID;
            CheckAnswer();

        }
    }

    /// <summary>
    /// Checks if the players input matches the variable <see cref="correctAnswer"/>.
    /// Stops the QTE if <see cref="playerAnswer"/> doens't match <see cref="correctAnswer"/>.
    /// </summary>
    private void CheckAnswer()
    {
        if(playerAnswer == correctAnswer)
        {
            abortQTE = false;
            renderQTE.sprite = null;
            correct.Play();
        }
        else
        {
            abortQTE = true;
            StopCoroutine(activateQTE);
            StopCoroutine(leverRotation);
            StopQTE();
            incorrect.Play();
        }
    }

    /// <summary>
    /// Stops the QTE and resets values.
    /// </summary>
    private void StopQTE()
    {
        if (abortQTE == true && interactingPlayer != null)
        {
            takeInput = false;
            rendererHolder.SetActive(false);
            interactingPlayer.GetComponent<NewPlayerScript>().SwapLiftingState();
            interactingPlayer.GetComponent<NewPlayerScript>().StartAnimation("FailQTE");
            interactingPlayer.SwapToMovement();
            QTETimer = originalQTETimer;
            playerAnswer = 0;
            abortQTE = false;
            correctAnswer = 0;
            interactingPlayer = null;
            renderQTE.sprite = null;
            playerHasAnswered = false;
            StartCoroutine(InteractionCooldown());
            leverRotation = StartCoroutine(RotateLever());
            foreach (AffectedObject affectedObject in affected)
            {
                affectedObject.ExecuteAction();
            }
            foreach (Interactable otherSwitch in connectedSwitches)
            {
                if (otherSwitch.interacting == true)
                {
                    otherSwitch.EnableInteraction();
                }
            }

            if (clockHand != null)
            {
                clockHand.SetActive(false);
            }

        }
    }

    /// <summary>
    /// Displays the correct input.
    /// </summary>
    private void DisplayWantedInput()
    {
        renderQTE.sprite = sprites[correctAnswer];
        if(clockHand != null)
        {

            clockHand.SetActive(true);
            StartCoroutine(TurnClock());
        }
    }

    /// <summary>
    /// Starts the QTE functionality on a loop.
    /// </summary>
    /// <returns></returns>
    private IEnumerator StartQTE()
    {
        int newNumber = correctAnswer;
        while(abortQTE == false)
        {
            playerHasAnswered = false;
            while (newNumber == correctAnswer)
            {
                newNumber = Random.Range(0, 4);
            }
            correctAnswer = newNumber;
            //correctAnswer = Random.Range(correctAnswer + 1, 4) % 4;
            DisplayWantedInput();
            takeInput = true;
            yield return new WaitForSeconds(QTETimer + timeAdd);
            QTETimer -= cutoffTime;
            if(playerHasAnswered == false)
            {
                abortQTE = true;
            }
            

        }
        StopQTE();
    }

    /// <summary>
    /// Rotates the clock's hand 360 degrees.
    /// </summary>
    /// <returns></returns>
    private IEnumerator TurnClock()
    {
        clockHand.transform.localRotation = Quaternion.Euler(Vector3.zero);
        while (t <  0.99f)
        {
            t += Time.deltaTime / QTETimer + timeAdd;
            clockHand.transform.localRotation = Quaternion.Euler(Vector3.Lerp(Vector3.zero, new Vector3(0, 0, 358), t));

            yield return null;
        }

        t = 0;
    }

    /// <summary>
    /// Rotates the lever to pull it down and release it back up
    /// </summary>
    /// <returns></returns>
    private IEnumerator RotateLever()
    {
        leverPullDownTime = 0.0f;
        Vector3 currentRotation = leverAxis.transform.localRotation.eulerAngles;
        Vector3 endRotation = Vector3.zero;
        if (currentRotation.x == 0)
        {
            endRotation = new Vector3(60, 0, 0);
            audioSource.PlayOneShot(pushDownLever);

        }
        else 
        {
            audioSource.PlayOneShot(releaseLever);
        }

        while (leverPullDownTime < 1.0f)
        {
            leverPullDownTime += Time.deltaTime / 1.5f;
            leverAxis.transform.localRotation = Quaternion.Euler(Vector3.Lerp(currentRotation, endRotation, leverPullDownTime));
            yield return null;
        }
    }

    /// <summary>
    /// Sets a delay before the animation is played while the player is placed in the right position.
    /// </summary>
    /// <returns></returns>
    private IEnumerator AnimationDelay()
    {
        leverRotation = StartCoroutine(RotateLever());
        interactingPlayer.GetComponent<NewPlayerScript>().StartAnimation("Pull Lever");
        yield return new WaitForSeconds(0.7f);
        rendererHolder.SetActive(true);
        rendererHolder.transform.LookAt(sceneCamera.transform.position, Vector3.up);
        takeInput = true;
        activateQTE = StartCoroutine(StartQTE());
        
        foreach (AffectedObject affectedObject in affected)
        {
            affectedObject.ExecuteAction();
        }
    }

    
}
