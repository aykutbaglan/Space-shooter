using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] EndGameState endGameState;
    private void Update()
    {
        QuitGame();
    }
    public void QuitGame()
    {
       if (Input.GetKeyDown(KeyCode.R))
       {
           SceneManager.LoadScene(0);
       }
       if (Input.GetKeyDown(KeyCode.Q))
       {
           Application.Quit();
       }
    }
    public static void GameResume()
    {
        Time.timeScale = 1f;
    }
    public static void GamePause()
    {
        Time.timeScale = 0f;
    }
}