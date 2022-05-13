using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int currentHealth = 5;
    EnemySpawning es;

    void Start()
    {
        es = FindObjectOfType<EnemySpawning>();
        es.isDestroyed = false;
    }

    void Update()
    {
        
    }

    public void DamageEnemy(int damgeAmount)
    {
        currentHealth-= damgeAmount;

        if(currentHealth <= 0)
        {
            es.isDestroyed = true;
            Destroy(gameObject);
            
        }
    }
}
