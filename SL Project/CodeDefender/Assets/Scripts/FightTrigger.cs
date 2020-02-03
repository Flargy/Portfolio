using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightTrigger : MonoBehaviour
{

    public List<GameObject> enemies;
    [SerializeField] private GameObject door;
    private bool fightHasStarted = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (fightHasStarted == false)
            {
                fightHasStarted = true;
                foreach (GameObject enemy in enemies)
                {
                    enemy.GetComponent<EnemyScript>().playerLocated = true;
                }
            }
        }
    }

    public void RemoveEnemy(GameObject removeObject)
    {
        GameObject objectToRemove = null;
        foreach(GameObject enemy in enemies)
        {
            if(removeObject == enemy)
            {
                objectToRemove = enemy;
            }
        }

        if(objectToRemove != null)
        {
            enemies.Remove(objectToRemove);
        }
        if (enemies.Count.Equals(0) && door != null)
        {
            door.SetActive(false);
        }
    }
}
