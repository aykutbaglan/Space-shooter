using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGameState : State
{
    [SerializeField] private Button startButton;
    [SerializeField] private StateMachine stateMachine;
    [SerializeField] private AsteroidsSpawnController asteroidSpawnController;
    [SerializeField] private PlayerController playerController;


    private void OnEnable()
    {
        startButton.onClick.AddListener(StartButtonOnClick);
    }
    private void OnDisable()
    {
        startButton.onClick.RemoveListener(StartButtonOnClick);
    }
    public override void OnEnter()
    {
        base.OnEnter();
        asteroidSpawnController.StopSpawning();
        playerController.enabled = false;
    }
    public override void OnExit()
    {
        base.OnExit();
    }
    public void StartButtonOnClick()
    {
        PlayerPrefs.SetInt("ÝsGameStarted",1);
        PlayerPrefs.Save();
        stateMachine.TransitionToNextState();
    }
}