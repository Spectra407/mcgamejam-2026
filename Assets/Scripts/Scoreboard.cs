using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class Scoreboard : MonoBehaviour
{
    public TextMeshProUGUI scoretext;

    public float scoringInterval;
    public int penaltyPerMinute = 5;

    private int score;

    void Start()
    {
        score = 0;
        // Invoke(EndOfGameScore(), 5);
        Timer.instance.AddIntervalAction(scoringInterval, UpdateScore, 5);
    }

    void Update()
    {
        UpdateScore();
    }

    private void UpdateScore()
    {
        CalculateScore();
        scoretext.text = "Score\n" + score.ToString();
    }

    private void CalculateScore()
    {
        int totalCount = CountTracker.Instance.GetTotalPredatorCount();
        int modifier = CountTracker.Instance.GetNumberWithAnimals();
        score = totalCount * modifier;
    }

    private void EndOfGameScore()
    {
       Debug.Log("Game Over\n" + "Your Score Was: " + score.ToString("000000"));
    }
}