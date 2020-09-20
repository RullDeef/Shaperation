using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTracker : MonoBehaviour
{
    public Text scoreText;

    public int Score { get; private set; } = 0;

    public static ScoreTracker Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        scoreText.text = "0";
    }

    public void IncreaseScore(int addScore)
    {
        Score += addScore;
        scoreText.text = Score.ToString();
    }
}
