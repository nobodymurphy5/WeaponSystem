using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletLifeTime;
    public Rigidbody rb;
    Gun gun;
   
    void Start()
    {
        gun = FindObjectOfType<Gun>();
    }

    
    void Update()
    {
        rb.velocity = transform.forward * gun.bulletMoveSpeed;

        bulletLifeTime -= Time.deltaTime;

        if(bulletLifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<EnemyHealth>().DamageEnemy(gun.bulletDamage);
        }

        Destroy(gameObject);
        Instantiate(gun.impactEffect, transform.position + (transform.forward * (-gun.bulletMoveSpeed * Time.deltaTime)), transform.rotation);
    }
}
