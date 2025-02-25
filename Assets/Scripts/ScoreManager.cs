using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public TMP_Text scoreTxt;
    public TMP_Text highScoreText;
    public TMP_Text missNumberText;
    public int score = 0;
    public int highScore;
    public int missNumber = 0;
    [SerializeField] private EndGameState endGameState;
    [SerializeField] private PlayerController playerController;

    private void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        UpdateHighScoreText();
    }
    public void UpdateScoreText()
    {
        score += 10;
        scoreTxt.text = "Score: " + score;
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
            UpdateHighScoreText();
        }
    }
    public void UpdateHighScoreText()
    {
        highScoreText.text = "High Score: " + highScore;   
    }
    public void MissNumber()
    {
        if (missNumber < 20 && playerController.playerShipGo.activeSelf)
        {
            missNumber++;
            missNumberText.text = "Miss Number: " + missNumber;
        }
        if (missNumber >= 20)
        {
            endGameState.OnEnter();
        }
    }
    public void EnemyDeadScore()
    {
        score += 200;
        scoreTxt.text = "Score: " + score;
    }
    public void ResetScoreAndMissNumberScore()
    {
        score = 0;
        scoreTxt.text = "Score: " + score;
        missNumber = 0;
        missNumberText.text = "Miss Number: " + missNumber;
    }
}