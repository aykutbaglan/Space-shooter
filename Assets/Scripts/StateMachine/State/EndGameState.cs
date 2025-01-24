using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndGameState : State
{
    public TMP_Text gameOverTxt;
    public TMP_Text restartTxt;
    public TMP_Text quitTxt;
    public bool gameOver;
    public bool restart;
    [SerializeField] private Button restartButton;
    [SerializeField] private StateMachine stateMachine;
    [SerializeField] private AsteroidsSpawnController asteroidsSpawnController;
    [SerializeField] private DestroyAsteroidsInScene destroyAsteroidsInScene;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private Mover asteroidMover;
    private void OnEnable()
    {
        restartButton.onClick.AddListener(RestartButtonOnClick);
    }
    private void OnDisable()
    {
        restartButton.onClick.RemoveListener(RestartButtonOnClick);
    }
    public override void OnEnter()
    {
        base.OnEnter();
        GameOver();
    }
    public override void OnExit() 
    {
        base.OnExit();
    }
    public void GameOver()
    {
        gameOver = true;
        quitTxt.text = "Press 'Q' for Quit";
        restartTxt.text = "Press 'R' for Restart";
        gameOverTxt.text = "Game Over";
    }
    public void RestartButtonOnClick()
    {
        PlayerPrefs.SetInt("ÝsGameStarted", 1);
        PlayerPrefs.Save();
        scoreManager.ResetScore();
        destroyAsteroidsInScene.DestroyAsteroids();
        asteroidsSpawnController.ResetAsteroidMoverSpeed();
        restart = true;
        stateMachine.transitionToSpecificState(1);
        playerController.playerShipTr.position = new Vector3(0f, playerController.playerShipTr.position.y, playerController.playerShipTr.position.z);
    }
}