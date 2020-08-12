using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreTracker : MonoBehaviour
{
    public TextMeshProUGUI Text;

    public int Score { get; private set; } = 0;

    public static ScoreTracker Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Text.SetText("0");
    }

    public void IncreaseScore(int addScore)
    {
        Score += addScore;
        Text.SetText(Score.ToString());
    }
}
