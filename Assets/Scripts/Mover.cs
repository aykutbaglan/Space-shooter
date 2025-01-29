using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    public float speed;
    private Rigidbody physic;

    void Start()
    {
        physic = GetComponent<Rigidbody>();
        physic.velocity = transform.forward * speed;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (other.CompareTag("Asteroids"))
            {
                return;
            }
            EnemyShipController enemy = other.GetComponent<EnemyShipController>();
            if (enemy != null)
            {
                enemy.TakeHealt(20,gameObject);
            }
        }
    }
}