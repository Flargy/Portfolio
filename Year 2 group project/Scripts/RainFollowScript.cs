using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainFollowScript : MonoBehaviour
{
    [SerializeField] private AudioSource AC;
    [SerializeField] private Transform player;
    [SerializeField] private AudioClip rain;
    // Start is called before the first frame update
    void Start()
    {
        StopRain();
        AC.clip = rain;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            StopRain();
        }
        transform.position = player.position + new Vector3(0, 10, 0);
    }

    public void StartRain()
    {
        var allaminaps = GetComponentsInChildren<ParticleSystem>();
        
        foreach(ParticleSystem ps in allaminaps)
        {
            ChangeEmissionOverTime(ps, 0, 50);

            //var emmis = ps.emission;
            //emmis.rate = 50;
        }
        AC.Play();
    }

    public void StopRain()
    {
        var allaminaps = GetComponentsInChildren<ParticleSystem>();

        foreach (ParticleSystem ps in allaminaps)
        {
            //var emmis = ps.emission;
            ChangeEmissionOverTime(ps, 50, 0);
            //emmis.rate = 0;
        }
        AC.Stop();
    }


    private void ChangeEmissionOverTime(ParticleSystem ps, float emissioFrom,float emissionTo)
    {
        var emmis = ps.emission;
        emmis.rate = Mathf.Lerp(emissioFrom, emissionTo, 5f);

        if (emissionTo > 1)
            emissionTo = 1;
        else if(emissioFrom > 1)
                emissioFrom = 1;
        AC.volume = Mathf.Lerp(emissioFrom, emissionTo, 5);
    }

  
}
