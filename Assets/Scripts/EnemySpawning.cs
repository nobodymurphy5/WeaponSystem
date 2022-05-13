using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawning : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject targetPrefab;
    int i = 0;
    public bool isDestroyed;


   
    void Start()
    {
       Instantiate(targetPrefab, spawnPoints[i].transform.position, Quaternion.Euler(0, 180, 0));

    }


    void Update()
    {

       if (i > 2)
       {
            i = 0;
       }

        if (isDestroyed)
        {
            
            Instantiate(targetPrefab, spawnPoints[i].transform.position, Quaternion.Euler(0, 180, 0));
            i++;
        }
    }
}
