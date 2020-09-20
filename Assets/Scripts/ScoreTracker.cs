using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTracker : MonoBehaviour
{
<<<<<<< HEAD
    public Text scoreText;
=======
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI HighScoreText;
>>>>>>> 8e35af6303873e40396d9fcc37004062c1555f0e

    public int Score { get; private set; } = 0;
    private int HighScore = 0;

    public static ScoreTracker Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
<<<<<<< HEAD
        scoreText.text = "0";
=======
        HighScore = PlayerPrefs.GetInt("HighScore", 0);

        ScoreText.SetText("0");
        HighScoreText.SetText(HighScore.ToString());
>>>>>>> 8e35af6303873e40396d9fcc37004062c1555f0e
    }

    public void IncreaseScore(int addScore)
    {
        Score += addScore;
<<<<<<< HEAD
        scoreText.text = Score.ToString();
=======
        HighScore = Mathf.Max(HighScore, Score);

        ScoreText.SetText(Score.ToString());
        HighScoreText.SetText(HighScore.ToString());

        PlayerPrefs.SetInt("HighScore", HighScore);
    }

    public void ResetScore()
    {
        Score = 0;
        ScoreText.SetText(Score.ToString());
>>>>>>> 8e35af6303873e40396d9fcc37004062c1555f0e
    }
}
