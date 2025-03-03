using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour
{
    public GameObject playerShipGo;
    public Transform playerShipTr;
    public Boundary boundary;
    public GameObject shot;
    public GameObject shotSpawn;
    public int playerHealt = 100;
    [SerializeField] int speed;
    [SerializeField] int tilt;
    [SerializeField] float nextFire;
    [SerializeField] float fireRate;
    [SerializeField] private StateMachine stateMachine;
    [SerializeField] private DestroyByContact destroyByContact;
    [SerializeField] private EnemyShipController enemyShipController;
    [SerializeField] private PlayerHealth playerhealth;
    private Rigidbody physic;
    private AudioSource audioPlayer;

    // Dinamik limitler için kamera referansı
    private Camera mainCamera;

    void Start()
    {
        physic = GetComponent<Rigidbody>();
        audioPlayer = GetComponent<AudioSource>();

        // Kamerayı referans al
        mainCamera = Camera.main;

        // Boundary limitlerini dinamik olarak ayarla
        UpdateMovementLimits();
    }

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.transform.position, shotSpawn.transform.rotation);
            audioPlayer.Play();
        }
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);

        physic.velocity = movement * speed;

        // X ve Z pozisyonlarını dinamik sınırlarla sınırlıyoruz
        Vector3 position = new Vector3(
            Mathf.Clamp(physic.position.x, boundary.xMin, boundary.xMax),
            0,
            Mathf.Clamp(physic.position.z, boundary.zMin, boundary.zMax)
            );

        physic.position = position;

        physic.rotation = Quaternion.Euler(0, 0, physic.velocity.x * tilt);
    }

    public void PlayerDie()
    {
        if (destroyByContact.explosion != null)
        {
            Instantiate(destroyByContact.playerExplosion, transform.position, transform.rotation);
        }
        else
        {
            Debug.LogError("Explosion is not assigned in playerController!");
        }
        Debug.Log("Player is dead. Calling SetGameOver on enemy ship.");
        playerShipGo.SetActive(false);
        if (enemyShipController != null)
        {
            enemyShipController.SetGameOver();
        }
        stateMachine.TransitionToNextState();
    }

    public void TakeHealt(int damage)
    {
        if (playerhealth != null)
        {
            playerhealth.TakeDamage(20);
            playerhealth.fill.color = playerhealth.gradient.Evaluate(playerhealth.healtBar.normalizedValue);
        }

        playerHealt -= damage;
        if (playerHealt <= 0)
        {
            PlayerDie();
            stateMachine.TransitionToNextState();
            audioPlayer.Play();
        }
    }

    // Ekran çözünürlüğüne göre limitleri güncelleme fonksiyonu
    void UpdateMovementLimits()
    {
        // Ekranın genişliğine göre limitleri dinamik olarak ayarlıyoruz
        float screenWidth = mainCamera.orthographicSize * Screen.width / Screen.height;

        // X eksenindeki sağ ve sol sınırları ayarla
        boundary.xMin = -screenWidth + 0.5f;
        boundary.xMax = screenWidth - 0.5f;

        // Debug logları ile doğru limitlerin ayarlandığını kontrol et
        Debug.Log("Boundary xMin: " + boundary.xMin);
        Debug.Log("Boundary xMax: " + boundary.xMax);
    }
}