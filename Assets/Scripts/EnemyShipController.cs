using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipController : MonoBehaviour
{
    public GameObject enemyShipGo;
    public GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireRate = 2f;
    [SerializeField] private float bulletSpeed = 10f;
    [SerializeField] private EndGameState endGameState;
    [SerializeField] private StateMachine stateMachine;
    private bool canfire = true;
    private float nexrFireTime;
    void Update()
    {
        Debug.Log($"canfire status in Update: {canfire}");
        if (canfire)
        {
            ResumeFire();
            FireAtPlayer();

        }
        else
        {
            FirePause();
        }

    }
    public void FireAtPlayer()
    {
       
        if (Time.time >= nexrFireTime)
        {
            nexrFireTime = Time.time + fireRate;

            GameObject bullet = Instantiate(bulletPrefab, firePoint.transform.position, firePoint.transform.rotation);
            Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
            if (bullet != null)
            {
                bulletRb.velocity = firePoint.forward * bulletSpeed;
            }
            EnemyMover mover = bullet.GetComponent<EnemyMover>();
            if (mover != null)
            {
                mover.SetEndGameState(endGameState);
            }
            Destroy(bullet, 5f);
        }
    }
    public void FirePause()
    {
        canfire = false;
        //if (stateMachine != null && endGameState != null)
        //{
        //    stateMachine.ChangeState(endGameState);
        //    //Debug.Log("State transitioned, firing paused.");
        //}
    }
    public void ResumeFire()
    {
        canfire = true;
    }
}