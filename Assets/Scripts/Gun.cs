using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    
    public GameObject bullet;
    public Transform firePoint;

    public GameObject impactEffect;

    //RaycastHit hit;
    public float bulletMoveSpeed;
    public int bulletDamage = 1;

  
    public int Mag;
    public int currentAmmo;

    public string gunName;
   

    void Start()
    {
       currentAmmo = Mag;
    }

    void Update()
    {
       
       
    }

    
}
