using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactorExplosion : MonoBehaviour {

    public int damage = 20;
    int i;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<EnemyVariables>().takeDamage(damage, transform);
        }
        else if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerVariables>().takeDamage(damage, transform);
        }
        else if (other.tag == "Reactor")
        {
            Debug.Log("boom");
            other.gameObject.GetComponent<Reactor>().explode();
        }
    }

    

    public void explosionTrigger()
    {
        StartCoroutine(explosion());
    }

    private IEnumerator explosion()
    {
        yield return new WaitForSeconds(0.1f);
        gameObject.SetActive(false);
    }

}
