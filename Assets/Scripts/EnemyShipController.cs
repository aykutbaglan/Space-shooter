using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipController : MonoBehaviour
{
    public GameObject enemyShipGo;
    public Transform enemyShipTr;
    public Transform healthBartr;
    public GameObject bulletPrefab;
    public int enemyHealt = 200;
    public int currentHealth;
    public bool enemyShipZpos;
    public float xFollowSpeed = 5f;
    public float moveDistance = -2f;
    public float moveDuration = 5f;
    public bool isGameOver = false;

    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireRate = 2f;
    [SerializeField] private float bulletSpeed = 10f;
    [SerializeField] private EndGameState endGameState;
    [SerializeField] private StateMachine stateMachine;
    [SerializeField] private DestroyByContact destroyByContact;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private EnemyHealth enemyHealthBar;

    private AudioSource audioPlayer;
    private bool canfire = true;
    private float nexrFireTime;
    
    private void Start()
    {
        audioPlayer = GetComponent<AudioSource>();
        currentHealth = enemyHealt;
        enemyHealthBar.SetMaxHealth(enemyHealt);
        if (healthBartr != null)
        {
            healthBartr.gameObject.SetActive(false);
        }
    }
    void Update()
    {
        if (enemyShipTr != null && healthBartr != null)
        {
            Vector3 worldPosition = enemyShipTr.position + new Vector3(0, 2, 0);
            healthBartr.position = Camera.main.WorldToScreenPoint(worldPosition);
        }
        if (!healthBartr.gameObject.activeSelf)
        {
            healthBartr.gameObject.SetActive(true);
        }
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
        TargetPlayerShip();
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
        scoreManager.EnemyDeadScore();
    }

    public void TakeHealt(int damage, GameObject source)
    {
        if (source.CompareTag("Asteroids"))
        {
            return;
        }
        TakeDamage(damage);
        enemyHealt -= damage;
        if (enemyHealt <= 0)
        {
            EnemyDie();
        }
        if (enemyHealthBar != null)
        {
            enemyHealthBar.TakeDamage(20);
        }
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        enemyHealthBar.SetHealth(currentHealth);
        if (currentHealth <= 0)
        {
            EnemyDie();
        }
    }
    public void TargetPlayerShip()
    {
        if (isGameOver) return;
        Debug.Log("Game over. Stopping enemy ship movement.");

        if (playerController.playerShipTr != null)
            {
                Vector3 targetPosition = new Vector3(playerController.playerShipTr.position.x, transform.position.y, transform.position.z);

                transform.position = Vector3.Lerp(transform.position, targetPosition, xFollowSpeed * Time.deltaTime);
            }
    }
    public void MoveZigzag()
    {
        Debug.Log("isGameOver in MoveZigzag: " + isGameOver);
        if (isGameOver)
        {
            Debug.Log("Game Over, stopping zigzag movement.");
            enemyShipTr.DOKill();
            return;
        }
        if (enemyShipTr != null)
        {
            Debug.Log("Starting zigzag movement.");
            float startZ = transform.position.z;
            enemyShipTr.DOKill();
            transform.DOMoveZ(startZ + moveDistance, moveDuration).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
        }
    }
    public void ResetEnemyShipPosition()
    {
        if (enemyShipTr != null)
        {
            enemyShipTr.position = new Vector3(0, 0, 10);
        }
    }
    public void SetGameOver()
    {
        isGameOver = true;
        Debug.Log("Game Over. Enemy movement and firing should stop.");
        enemyShipTr.DOKill();
    }
    public void ResetEnemyShip()
    {
        enemyShipTr.DOKill(true);
        ClearAllExplosions();

        if (healthBartr != null)
        {
            healthBartr.gameObject.SetActive(false);
        }

        enemyShipGo.SetActive(false);
        ResetEnemyShipPosition();
        isGameOver = false;

        if (enemyShipGo.activeSelf == true)
        {
        enemyShipTr.DOMoveZ(7.5f, 1).SetEase(Ease.Linear).OnComplete(() =>
        {
            TargetPlayerShip();
            MoveZigzag();
            if (healthBartr != null)
            {
                healthBartr.gameObject.SetActive(true);
            }
        });

        }
    }
    public void ResetEnemyShipHealth()
    {
        enemyHealt = 200;
        currentHealth = enemyHealt;
        if (enemyHealthBar != null)
        {
            enemyHealthBar.ResetHealth();
        }
    }
    private void ClearAllExplosions()
    {
        GameObject[] explosions = GameObject.FindGameObjectsWithTag("Explosion");
        foreach (GameObject explosion in explosions)
        {
            Destroy(explosion);
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