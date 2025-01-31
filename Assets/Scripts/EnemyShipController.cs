using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipController : MonoBehaviour
{
    public GameObject enemyShipGo;
    public Transform enemyShipTr;
    public GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireRate = 2f;
    [SerializeField] private float bulletSpeed = 10f;
    [SerializeField] private EndGameState endGameState;
    [SerializeField] private StateMachine stateMachine;
    [SerializeField] private DestroyByContact destroyByContact;
    [SerializeField] private PlayerController playerController;
    private bool canfire = true;
    private float nexrFireTime;
    private AudioSource audioPlayer;
    public int enemyHealt = 100;
    public bool enemyShipZpos;

    private void Start()
    {
        audioPlayer = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (enemyShipGo != null)
        {
            if (enemyShipTr.position.z <= 7.5f)
            {
                TargetEnemyShitPos();
            }

            Debug.Log($"canfire status in Update: {canfire}");
            if (enemyShipZpos == true)
            {
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
        }
    }
    public void FireAtPlayer()
    {
        if (Time.time >= nexrFireTime)
        {
            nexrFireTime = Time.time + fireRate;

            GameObject bullet = Instantiate(bulletPrefab, firePoint.transform.position, firePoint.transform.rotation);
            audioPlayer.Play();
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
    public void EnemyDie()
    {
        if (destroyByContact.explosion != null)
        {
            Instantiate(destroyByContact.playerExplosion, transform.position, transform.rotation);
        }
        else
        {
            Debug.LogError("Explosion is not assigned in playerController!");
        }
        enemyShipGo.SetActive(false);
        
    }
    public void TakeHealt(int damage, GameObject source)
    {
        if (source.CompareTag("Asteroids"))
        {
            return;
        }
        enemyHealt -= damage;
        if (enemyHealt <= 0)
        {
            EnemyDie();
        }
    }
    public void ResetEnemyShipPosition()
    {
        if (enemyShipTr != null)
        {
            enemyShipTr.position = new Vector3(0, 0, 10);
        }
    }
    public void FirstEnemyShipPos()
    {
        enemyShipZpos = false;
    }
    public void TargetEnemyShitPos()
    {
        enemyShipZpos = true;
    }
    public void FirePause()
    {
        canfire = false;
    }
    public void ResumeFire()
    {
        canfire = true;
    }
}