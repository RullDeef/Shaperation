using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTracker : MonoBehaviour
{
    public Text scoreText;

    public int Score { get; private set; } = 0;
    private int HighScore = 0;

    public static ScoreTracker Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        scoreText.text = "0";
        HighScore = PlayerPrefs.GetInt("HighScore", 0);
        //HighScoreText.SetText(HighScore.ToString());
    }

    public void IncreaseScore(int addScore)
    {
        Score += addScore;
        scoreText.text = Score.ToString();
        HighScore = Mathf.Max(HighScore, Score);

        //HighScoreText.SetText(HighScore.ToString());

        PlayerPrefs.SetInt("HighScore", HighScore);
    }

    public void ResetScore()
    {
        Score = 0;
        scoreText.text = "0";
    }
}
