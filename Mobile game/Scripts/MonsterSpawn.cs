using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawn : MonoBehaviour {

    public Transform[] spawnGate;
    public float timeBetweenWaves, spawnRate;
    public int[] waveSize;
    public GameObject[] monsterType;
    public int monsterLimit, nextWaveLimit;
    public GameObject boss;

    [HideInInspector]
    public static int numberOfMonsters;
    [HideInInspector]
    public static bool monstersSpawned;

    private int currentWave, monsterIndex = 0, monsterCounter = 0;
    private float nextWaveTimeLimit;
    private bool spawnPause;
    private int[] divideMonster;

    void Start() {
        monstersSpawned = false;
        currentWave = 0;
        numberOfMonsters = 0;
        StartCoroutine(spawnMonster());
        divideMonster = new int[monsterType.Length];

        for (int i = 0; i < divideMonster.Length; i++)
        {
            divideMonster[i] = i * 4 -1;
        }
        
    }

    public int getCurrentWave()
    {
        return currentWave;
    }

    IEnumerator spawnMonster()
    {
        StartCoroutine(countDown(timeBetweenWaves));
        nextWaveTimeLimit = timeBetweenWaves;
        foreach (Transform t in spawnGate)
        {
            
            yield return new WaitUntil(() => numberOfMonsters < monsterLimit);
            
            if (waveSize[currentWave] > 0)
            {
                if (monsterCounter != 0)
                {
                    for (int i = 0; i < divideMonster.Length; i++)
                    {
                        if (divideMonster[i] == -1 || monsterCounter % divideMonster[i] == 0)
                        {
                            monsterIndex = i;
                        }
                    }
                }
                else
                {
                    yield return new WaitForSeconds(5.0f);
                }

                

                Instantiate(monsterType[monsterIndex], t.position, Quaternion.identity);
                monsterCounter++;
                waveSize[currentWave]--;

            }


            yield return new WaitForSeconds(spawnRate);

        }


        if (waveSize[currentWave] > 0)
            StartCoroutine(spawnMonster());
        else
        {
            if (currentWave < (waveSize.Length - 1))
            {


                yield return new WaitUntil(() => numberOfMonsters <= nextWaveLimit || nextWaveTimeLimit == 0);

                
                currentWave++;
                yield return new WaitForSeconds(2.0f);
                StartCoroutine(spawnMonster());
            }
            else
            {
                yield return new WaitForSeconds(2.0f);
                Instantiate(boss, spawnGate[0].position, Quaternion.identity);
                yield return new WaitUntil(() => numberOfMonsters == 0);
                yield return new WaitForSeconds(1.0f);
                monstersSpawned = true;
            }
        }
    }

    IEnumerator countDown(float time)
    {
        yield return new WaitForSeconds(1.0f);
        time--;
        if (time > 0)
        { 
            StartCoroutine(countDown(time));
        }
        else
        {
            nextWaveTimeLimit = time;
        }
    }
}
