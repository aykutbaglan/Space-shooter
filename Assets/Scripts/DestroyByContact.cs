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
    private void Start()
    {
        scoreManager = GameObject.FindWithTag("ScoreManager").GetComponent<ScoreManager>();
        endGameState = GameObject.FindWithTag("StateMachine").GetComponent<StateMachine>().GetStates[2];
        stateMachine = GameObject.FindWithTag("StateMachine").GetComponent<StateMachine>();
        playerShip = GameObject.FindWithTag("Player");
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Boundary")
        {
            return;
        }
        if (other.tag == "Player")
        {
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
            playerShip.SetActive(false);
            //(endGameState as EndGameState).GameOver();
            stateMachine.ChangeState(endGameState);
            return;
        }
        if (other.CompareTag("Enemy"))
        {
            Destroy(gameObject);
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