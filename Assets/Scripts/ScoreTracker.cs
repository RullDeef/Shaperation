using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreTracker : MonoBehaviour
{
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI HighScoreText;

    public int Score { get; private set; } = 0;
    private int HighScore = 0;

    public static ScoreTracker Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        HighScore = PlayerPrefs.GetInt("HighScore", 0);

        ScoreText.SetText("0");
        HighScoreText.SetText(HighScore.ToString());
    }

    public void IncreaseScore(int addScore)
    {
        Score += addScore;
        HighScore = Mathf.Max(HighScore, Score);

        ScoreText.SetText(Score.ToString());
        HighScoreText.SetText(HighScore.ToString());

        PlayerPrefs.SetInt("HighScore", HighScore);
    }

    public void ResetScore()
    {
        Score = 0;
        ScoreText.SetText(Score.ToString());
    }
}
