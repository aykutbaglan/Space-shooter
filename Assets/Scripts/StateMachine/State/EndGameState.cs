using DG.Tweening;
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
    [SerializeField] private Button restartButton;
    [SerializeField] private StateMachine stateMachine;
    [SerializeField] private AsteroidsSpawnController asteroidsSpawnController;
    [SerializeField] private DestroyAsteroidsInScene destroyAsteroidsInScene;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private EnemyShipController enemyShipController;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private PlayerHealt playerHealth;
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
        //enemyShipController.FirePause();
        //Game over sesini oynat.
    }
    public override void OnExit() 
    {
        base.OnExit();
    }
    public void GameOver()
    {
        gameManager.gameOver = true;
        enemyShipController.FirePause();
        playerController.playerShipGo.SetActive(false);
        enemyShipController.SetGameOver();
        quitTxt.text = "Press 'Q' for Quit";
        restartTxt.text = "Press 'R' for Restart";
        gameOverTxt.text = "Game Over";
    }
    public void RestartButtonOnClick()
    {
        PlayerPrefs.SetInt("ÝsGameStarted", 1);
        PlayerPrefs.Save();
        scoreManager.ResetScoreAndMissNumberScore();
        destroyAsteroidsInScene.DestroyAsteroids();
        asteroidsSpawnController.ResetAsteroidMoverSpeed();
        gameManager.restart = true;
        OnExit();
        stateMachine.transitionToSpecificState(1);
        playerController.playerShipTr.position = new Vector3(0f, playerController.playerShipTr.position.y, 0);
        enemyShipController.gameObject.SetActive(false);
        playerController.playerHealt = 100;
        enemyShipController.ResumeFire();
        enemyShipController.enemyHealt = 100;
        enemyShipController.ResetEnemyShip();
        playerHealth.currentHealt = 100;
    }
}