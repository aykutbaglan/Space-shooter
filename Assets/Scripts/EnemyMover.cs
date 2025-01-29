using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    public float speed;
    private EndGameState endGameState;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity =  transform.forward * speed;
    }
    public void SetEndGameState(EndGameState state)
    {
        endGameState = state;
    }
    private void OnTriggerEnter(Collider other)
    {
        //if (other.CompareTag("Asteroids"))
        //{
        //    Debug.Log("asdasdasd");
        //    return;
        //}
        Debug.Log($"Collision with: {other.gameObject.name}"); // Çarpýþma mesajý
        if (other.CompareTag("Player"))
        {
            
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                Debug.Log("Player hit by enemy bullet!");
                player.TakeHealt(20);
            if (player.playerHealt <= 0 && endGameState != null)
            {
                endGameState.OnEnter();
            }
                else if (endGameState == null)
                {
                    Debug.LogError("EndGameState is not assigned!");
                }
            }
            Destroy(gameObject);
        }
    }
}
