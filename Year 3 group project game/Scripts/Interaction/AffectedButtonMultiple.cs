using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Main Author: Marcus Lundqvist

public class AffectedButtonMultiple : Interactable
{
    [SerializeField] private AffectedObject[] affectedObjectList = null;
    [SerializeField] private GameObject button = null;
    [SerializeField] private float durationToClose = 2.0f;
    [SerializeField] private float animationDuration = 0.5f;
    [SerializeField] private float actionDelay = 0.0f;
    [SerializeField] private float timeDelayBetweenObjects = 0.5f;

    private float lerpTime = 0;
    private float t = 0;
    private Vector3 pressedPosition = Vector3.zero;
    private Vector3 notPressedPosition = Vector3.zero;
    private AudioSource audioSource;
    public AudioClip buttonSoundIn;
    public AudioClip buttonSoundOut;


    /// <summary>
    /// Activates the affected objects connected and plays animation on the interacting player.
    /// </summary>
    /// <param name="player"></param>
    public override void Interact(GameObject player)
    {
        if (interacting == false)
        {

            StartCoroutine(ActivateObjectsWithTimeDelay());
            
            interacting = true;
            StartCoroutine(InteractionCooldown());
            if (button != null)
            {
                StartCoroutine(ButtonMovement());
            }
            
            StartCoroutine(OnATimer());
            
            player.GetComponent<NewPlayerScript>().Freeze(animationDuration);
            player.GetComponent<NewPlayerScript>().StartAnimation("Push Button");
            StartCoroutine(ActionDelay());
        }
        
    }

    /// <summary>
    /// Sets starting values.
    /// </summary>
    private void Start()
    {

        audioSource = GetComponent<AudioSource>();
        if (button != null)
        {
            pressedPosition = button.transform.position - ((button.transform.up * 0.1f));
            notPressedPosition = button.transform.position;
        }
    }

    /// <summary>
    /// Lerps the movement of the button in and out.
    /// </summary>
    /// <returns></returns>
    private IEnumerator ButtonMovement()
    {
        audioSource.PlayOneShot(buttonSoundIn);

        while (lerpTime < 1)
        {
            t += Time.deltaTime;
            button.transform.position = Vector3.Lerp(notPressedPosition, pressedPosition, t);
            lerpTime += Time.deltaTime;
            yield return null;
        }

        t = 0.0f;
        lerpTime = 0.0f;

        while (lerpTime < 1)
        {
            t += Time.deltaTime;
            button.transform.position = Vector3.Lerp(pressedPosition, notPressedPosition, t);
            lerpTime += Time.deltaTime;
            yield return null;
        }
        audioSource.PlayOneShot(buttonSoundOut);
        t = 0.0f;
        lerpTime = 0.0f;
    }

    /// <summary>
    /// Reactivates the affected object after a set amount of time.
    /// </summary>
    /// <returns></returns>
    private IEnumerator OnATimer()
    {
        yield return new WaitForSeconds(durationToClose);

        StartCoroutine(ActivateObjectsWithTimeDelay());
        
    }

    /// <summary>
    /// Creates a delay before activation.
    /// </summary>
    /// <returns></returns>
    private IEnumerator ActionDelay()
    {
        yield return new WaitForSeconds(actionDelay);

        if (interacting == false)
        {
            
            foreach (AffectedObject obj in affectedObjectList)
            {
                obj.ExecuteAction();
            }
            
            interacting = true;
            StartCoroutine(InteractionCooldown());
            if (button != null)
            {
                StartCoroutine(ButtonMovement());
            }
            
             StartCoroutine(OnATimer());
            
        }
    }

    private IEnumerator ActivateObjectsWithTimeDelay()
    {
        foreach(AffectedObject obj in affectedObjectList)
        {
            obj.ExecuteAction();
            yield return new WaitForSeconds(timeDelayBetweenObjects);
        }
    }
}
