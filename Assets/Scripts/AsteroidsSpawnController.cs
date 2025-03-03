using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class AsteroidsSpawnController : MonoBehaviour
{
    public Coroutine _spawnCoroutine;
    public GameObject asteroids;
    [SerializeField] private int _spawnCount;
    [SerializeField] private float _startSpawn;
    [SerializeField] private float _spawnWait;
    [SerializeField] private float _waveWait;
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private EndGameState endGameState;
    [SerializeField] private AsteroidMover asteroidMover;
    [SerializeField] private EnemyShipController enemyShipController;
    [SerializeField] private PlayerController playerController;

    private Camera _mainCamera;

    private void Start()
    {
        _mainCamera = Camera.main;
        ResetAsteroids();
        ResetAsteroidMoverSpeed();
        if (enemyShipController.enemyShipGo != null)
        {
            enemyShipController.enemyShipGo.SetActive(false);
        }
    }

    private void Update()
    {
        if (gameManager.gameOver && _spawnCoroutine != null)
        {
            StopSpawning();
        }
    }

    public IEnumerator SpawnAsteroids()
    {
        yield return new WaitForSeconds(_startSpawn);
        int waveCount = _spawnCount;
        Vector3 astroidScale = asteroids.transform.localScale;

        // Ekran geni�li�ini dinamik olarak al�yoruz
        float screenWidth = _mainCamera.orthographicSize * Screen.width / Screen.height;

        while (true)
        {
            for (int i = 0; i < _spawnCount; i++)
            {
                // Asteroidlerin spawn pozisyonunu ekran�n sa� ve sol kenarlar�ndan ba��ms�z olarak hesapl�yoruz
                float spawnX = Random.Range(-screenWidth, screenWidth);
                Vector3 spawnPosition = new Vector3(spawnX, 0, 10);
                Quaternion spawnRotation = Quaternion.identity;

                GameObject asteroidInstance = Instantiate(asteroids, spawnPosition, spawnRotation);
                asteroidInstance.transform.SetParent(base.transform);

                asteroidInstance.transform.localScale = astroidScale;

                yield return new WaitForSeconds(_spawnWait);
            }

            yield return new WaitForSeconds(_waveWait);

            _spawnCount += 5;
            asteroidMover.speed -= 1;

            if (_spawnCount == 15)
            {
                if (enemyShipController.enemyShipGo != null)
                {
                    enemyShipController.enemyShipGo.SetActive(true);
                    enemyShipController.enemyShipTr.DOMove(new Vector3(0, 0, 7.5f), 1).OnComplete(() =>
                    {
                        if (!enemyShipController.isGameOver)
                        {
                            enemyShipController.MoveZigzag();
                        }
                        else
                        {
                            Debug.Log("Game Over, Zigzag not started.");
                        }
                    });
                }
            }

            astroidScale += new Vector3(-0.05f, -0.05f, -0.05f);
            if (gameManager.gameOver == true)
            {
                break;
            }
        }
    }

    public void StartSpawning()
    {
        if (_spawnCoroutine == null)
        {
            _spawnCoroutine = StartCoroutine(SpawnAsteroids());
        }
    }

    public void StopSpawning()
    {
        if (_spawnCoroutine != null)
        {
            StopCoroutine(_spawnCoroutine);
            _spawnCoroutine = null;
        }
    }

    public void ResetAsteroids()
    {
        endGameState.gameOverTxt.text = "";
        endGameState.restartTxt.text = "";
        endGameState.quitTxt.text = "";
        gameManager.gameOver = false;
        gameManager.restart = false;
    }

    public void ResetAsteroidMoverSpeed()
    {
        _spawnCount = 10;
        asteroidMover.speed = -3f;
    }
}