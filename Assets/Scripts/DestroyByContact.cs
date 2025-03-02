using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class DestroyByContact : MonoBehaviour
{
    public GameObject explosion;
    public GameObject playerExplosion;
    private ScoreManager scoreManager;
    [SerializeField] private State endGameState;
    [SerializeField] private StateMachine stateMachine;
    [SerializeField] private GameObject playerShip;
    [SerializeField] private EnemyShipController enemyShipController;
    private void Start()
    {
        scoreManager = GameObject.FindWithTag("ScoreManager").GetComponent<ScoreManager>();
        endGameState = GameObject.FindWithTag("StateMachine").GetComponent<StateMachine>().GetStates[2];
        stateMachine = GameObject.FindWithTag("StateMachine").GetComponent<StateMachine>();
        playerShip = GameObject.FindWithTag("Player");
    }
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Çarpışan obje: " + other.gameObject.name);
        if(other.gameObject.tag == "Boundary")
        {
            return;
        }
        if (other.tag == "Player")
        {
            PlayerHealt playerHealth = other.GetComponent<PlayerHealt>();

            if (playerHealth == null) 
            {
                Debug.LogError("PlayerHealt bileşeni bulunamadı! Player objesinde var mı?");
                return;
            }
            playerHealth.TakeDamage(20);
            
            if (playerHealth.currentHealt <= 0)
            {
                if (explosion != null)
                {
                    Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
                }
                other.gameObject.SetActive(false);
                stateMachine.ChangeState(endGameState);
                if (enemyShipController != null)
                {
                    enemyShipController.MoveZigzag();
                }
            }
            if (explosion != null)
            {
                Instantiate(explosion, transform.position, transform.rotation);
            }

            Destroy(gameObject);
            return;
        }
        if (other.CompareTag("Enemy"))
        {
            return;
        }
        if (other.CompareTag("Asteroids") && gameObject.CompareTag("Asteroids"))
        {
            return;
        }
        else
        {
            scoreManager.UpdateScoreText();
        }
        if (explosion != null)
        {
            Instantiate(explosion, transform.position, transform.rotation);
        }
        Destroy(gameObject);   
    }
}