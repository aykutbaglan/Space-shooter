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
        playerShip.SetActive(true);
        playerController.enabled = true;
    }
    public override void OnExit()
    {
        base.OnExit();
        asteroidsSpawnController.StopSpawning();
    }
}