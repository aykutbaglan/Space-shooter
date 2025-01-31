using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMover : MonoBehaviour
{
    public float speed;
    private Rigidbody _physic;

    void Start()
    {
        _physic = GetComponent<Rigidbody>();
        _physic.velocity = transform.forward * speed;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyShipController enemy = other.GetComponent<EnemyShipController>();
            if (enemy != null)
            {
                enemy.TakeHealt(20, gameObject);
            }
        }
    }
}
