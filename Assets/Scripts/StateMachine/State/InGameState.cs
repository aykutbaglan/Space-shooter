using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameState : State
{
    [SerializeField] private AsteroidsSpawnController asteroidsSpawnController;
    [SerializeField] private GameObject playerShip;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private StateMachine statemachine;
    public override void OnEnter()
    {
        base.OnEnter();
        GameManager.GameResume();
        asteroidsSpawnController.ResetAsteroids();
        asteroidsSpawnController.StartSpawning();
        //if (playerController.healt <= 0) //can�m�z 0 de�il ise playership setactive ini a� 0 ise kapat
        //{
        //    playerShip.SetActive(false);
        //    statemachine.TransitionToNextState();
        //}
        //else
        //{
        //    playerShip.SetActive(true);
        //}
        playerShip.SetActive(true);
        playerController.enabled = true;
    }
    public override void OnExit()
    {
        base.OnExit();
        asteroidsSpawnController.StopSpawning();
    }
}
