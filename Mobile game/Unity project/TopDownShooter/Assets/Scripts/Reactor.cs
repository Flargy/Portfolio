using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reactor : MonoBehaviour {

    public Sprite disabledSprite;
    public GameObject explosionCollider;
    public GameObject explosionEffect;
    public AudioClip explosionSound;

    private int damage = 1;
    private ParticleSystem particleEffect;
    private bool exploded = false;
    private AudioSource source;

    // Use this for initialization
    void Start () {
        particleEffect = GetComponent<ParticleSystem>();
        particleEffect.Play();
        source = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void explode()
    {
        if (!exploded)
        {
            Debug.Log("sprängd");
            source.PlayOneShot(explosionSound);
            exploded = true;
            particleEffect.Stop();
            GetComponent<SpriteRenderer>().sprite = disabledSprite;
            explosionEffect.SetActive(true);
            explosionCollider.SetActive(true);
            explosionCollider.GetComponent<ReactorExplosion>().explosionTrigger();
        }
    }

    
}
