using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Main Author: Marcus Lundqvist

public class PressurePlate : MonoBehaviour
{
    [SerializeField] private List<AffectedObject> affectedObject = null;
    [SerializeField] private int desiredNrOfObjects = 1;
    [SerializeField] private Material activatedColor = null;
    [SerializeField] private MeshRenderer colorObject = null;
    [SerializeField] private DoorLightChange[] lights = null;
    private float itemsOnPad;
    private bool crouchedOn = false;
    private Material startingColor = null;
    private AudioSource audioSource;
    private GameObject playerOnPad = null;
    public AudioClip activatedSound;
    public AudioClip deActivatedSound;

    /// <summary>
    /// Sets starting values to variables.
    /// </summary>
    private void Start()
    {
        startingColor = colorObject.material;
        audioSource = GetComponent<AudioSource>();

    }
    
    /// <summary>
    /// Starts a coroutine to lower the counter <see cref="itemsOnPad"/> on the object.
    /// </summary>
    public void LowerCounter()
    {

        StartCoroutine(CounterDelay());
    }

    /// <summary>
    /// Increases the counter <see cref="itemsOnPad"/>.
    /// Changes color and starts <see cref="AffectedObject.ExecuteAction"/> on each connected object in the <see cref="affectedObject"/> list if <see cref="itemsOnPad"/> is equals to <see cref="desiredNrOfObjects"/>.
    /// Only reacts to objects tagged as "Player", "CarryBox" or "CrouchCollider"
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player") || other.CompareTag("CarryBox") || other.CompareTag("CrouchCollider"))
        {
            itemsOnPad++;
            if (itemsOnPad == desiredNrOfObjects)
            {
               
                colorObject.material = activatedColor;
                audioSource.PlayOneShot(activatedSound);
                foreach (AffectedObject affected in affectedObject)
                {
                
                    affected.ExecuteAction();
                    
                }
            }

            foreach(DoorLightChange light in lights)
            {
                light.ChangeEmission(Mathf.Min(itemsOnPad / desiredNrOfObjects, 1.0f));
            }
        }

    }

    /// <summary>
    /// Lowers the counter <see cref="itemsOnPad"/>.
    /// Changes color and starts <see cref="AffectedObject.ExecuteAction"/> on each connected object in the <see cref="affectedObject"/> list if <see cref="itemsOnPad"/> is less than <see cref="desiredNrOfObjects"/>.
    /// Only reacts to objects tagged as "Player", "CarryBox" or "CrouchCollider"
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("CarryBox") || other.CompareTag("CrouchCollider"))
        {
            itemsOnPad = Mathf.Max(0, itemsOnPad - 1);
            if (itemsOnPad < desiredNrOfObjects)
            {
                colorObject.material = startingColor;
                audioSource.PlayOneShot(deActivatedSound);
                foreach (AffectedObject affected in affectedObject)
                {
                
                    affected.ExecuteAction();
                    
                }
            }

            foreach(DoorLightChange light in lights)
            {
                light.ChangeEmission(Mathf.Min(itemsOnPad / desiredNrOfObjects, 1.0f));
            }
        }
    }

    /// <summary>
    /// Returns whether or not the pressure plate has the desired amount of objects on it or not.
    /// </summary>
    /// <returns></returns>
    public bool GetPushed()
    {
        if(itemsOnPad >= desiredNrOfObjects)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Lowers the counter <see cref="itemsOnPad"/> at the end of the frame.
    /// Changes color and starts <see cref="AffectedObject.ExecuteAction"/> on each connected object in the <see cref="affectedObject"/> list if <see cref="itemsOnPad"/> is less than <see cref="desiredNrOfObjects"/>.
    /// </summary>
    /// <returns></returns>
    private IEnumerator CounterDelay()
    {
        yield return null;
        itemsOnPad = Mathf.Max(0, itemsOnPad - 1);

        if (itemsOnPad < desiredNrOfObjects)
        {
            colorObject.material = startingColor;
            audioSource.PlayOneShot(deActivatedSound);
            foreach (AffectedObject affected in affectedObject)
            {

                affected.ExecuteAction();

            }
        }
        foreach (DoorLightChange light in lights)
        {
            light.ChangeEmission(Mathf.Min(itemsOnPad / desiredNrOfObjects, 1.0f));
        }
    }
}
