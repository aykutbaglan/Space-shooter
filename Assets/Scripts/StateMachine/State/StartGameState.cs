using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGameState : State
{
    [SerializeField] private Button startButton;
    [SerializeField] private StateMachine stateMachine;


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
        GameManager.GamePause();
    }
    public override void OnExit()
    {
        base.OnExit();
    }
    public void StartButtonOnClick()
    {
        PlayerPrefs.SetInt("�sGameStarted",1);
        PlayerPrefs.Save();
        stateMachine.TransitionToNextState();
    }
}