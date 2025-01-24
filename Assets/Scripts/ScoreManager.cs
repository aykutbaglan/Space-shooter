using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    
    public TMP_Text scoreTxt;
    public TMP_Text highScoreText;
    public int score = 0;
    public int highScore;

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
    public void ResetScore()
    {
        score = 0;
        scoreTxt.text = "Score: " + score;
    }
}