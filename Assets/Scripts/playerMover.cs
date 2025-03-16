using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMover : MonoBehaviour
{
    public float speed;
    private ScoreManager scoreManager;
    private DestroyByContact destroyByContact;
    private EnemyShipController enemyshipController;
    private Rigidbody _physic;

    void Start()
    {
        _physic = GetComponent<Rigidbody>();
        _physic.velocity = transform.forward * speed;

        GameObject scoreManagerGo = GameObject.FindWithTag("ScoreManager");
        if (scoreManagerGo != null)
        {
            scoreManager = scoreManagerGo.GetComponent<ScoreManager>();
        }
        GameObject enemyShipGo = GameObject.FindWithTag("Enemy");
        if (enemyShipGo != null)
        {
            enemyshipController = enemyShipGo.GetComponent<EnemyShipController>();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyShipController enemy = other.GetComponent<EnemyShipController>();
            if (enemy != null)
            {
                enemy.TakeHealt(20, gameObject);
                Destroy(gameObject);
            }
        }
    }
}