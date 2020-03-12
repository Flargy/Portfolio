using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Main Author: Marcus Lundqvist

public class DoorLightChange : MonoBehaviour
{
    [SerializeField] private Color activationColor;
    [SerializeField] private AudioClip activationSound = null;
    [SerializeField] private bool playSound = false;

    private Renderer lightRenderer = null;
    private MaterialPropertyBlock propertyBlock = null;
    private Color startColor = Color.red;
    private AudioSource source = null;

    /// <summary>
    /// Sets values to variables on startup.
    /// </summary>
    private void Start()
    {
        lightRenderer = GetComponent<Renderer>();
        propertyBlock = new MaterialPropertyBlock();
        startColor = GetComponent<MeshRenderer>().material.GetColor("_EmissionColor");
        source = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Sets the emission color of the object depending on the value received.
    /// </summary>
    /// <param name="strength">The value of how strong the emission color will be.</param>
    public void ChangeEmission(float strength)
    {

        lightRenderer.GetPropertyBlock(propertyBlock);
        if(strength == 0)
        {
            propertyBlock.SetColor("_EmissionColor", startColor);
        }
        else
        {
            propertyBlock.SetColor("_EmissionColor", activationColor * (strength * 3));

        }
        lightRenderer.SetPropertyBlock(propertyBlock);

        if(strength >= 1 && playSound == true)
        {
            source.PlayOneShot(activationSound);
            playSound = false;
            StartCoroutine(SoundCooldown());
        }

    }

    private IEnumerator SoundCooldown()
    {
        yield return new WaitForSeconds(2.0f);
        playSound = true;
    }

   
}
