using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMover : MonoBehaviour
{
    public float speed;
    private ScoreManager scoreManager;
    private DestroyByContact destroyByContact;
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
        //GameObject destroyByContactObj = GameObject.FindWithTag("DestroyByContact");
        //if (destroyByContactObj != null)
        //{
        //    destroyByContact = destroyByContactObj.GetComponent<DestroyByContact>();
        //}
        //else
        //{
        //    Debug.LogError("DestroyByContact scriptine sahip obje bulunamadý! 'DestroyByContact' tag'ini kontrol et.");
        //}
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
                //if (scoreManager != null)
                //{
                //   scoreManager.score += 50;
                //}
            }
        }
        //if (other.CompareTag("Asteroids") && destroyByContact != null)
        //{
        //    Instantiate(destroyByContact.explosion, transform.position, transform.rotation);
        //    Destroy(gameObject);
        //}
    }
}
