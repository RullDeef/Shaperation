using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTracker : MonoBehaviour
{
    public Text ScoreText;
    public Text HighScoreText;

    private int HighScore = 0;

    public int Score { get; private set; } = 0;

    public static ScoreTracker Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        ScoreText.text = "0";
        HighScore = PlayerPrefs.GetInt("HighScore", 0);
        HighScoreText.text = HighScore.ToString();
    }

    public void IncreaseScore(int addScore)
    {
        Score += addScore;
        ScoreText.text = Score.ToString();
        HighScore = Mathf.Max(HighScore, Score);

        ScoreText.text = Score.ToString();
        HighScoreText.text = HighScore.ToString();

        PlayerPrefs.SetInt("HighScore", HighScore);
    }

    public void ResetScore()
    {
        Score = 0;
        ScoreText.text = Score.ToString();
    }
}
