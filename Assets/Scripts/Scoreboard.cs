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
        Invoke(nameof(EndOfGameScore), 5);
        Timer.instance.AddIntervalAction(scoringInterval, UpdateScore, 5);
    }

    void Update()
    {
        // float currentTime = timer.GetTime();

        // if (lastTime != timer.gameLength && (int) (currentTime / scoringInterval) != (int) (lastTime / scoringInterval))
        // {
        //     UpdateScore();
        // }

        // lastTime = currentTime;
    }

    private void UpdateScore()
    {
        CalculateScore();
        scoretext.text = "Score\n" + score.ToString("000000");
    }

    private void CalculateScore()
    {
        int totalCount = CountTracker.Instance.GetTotalPredatorCount();
        int lowestCount = CountTracker.Instance.GetLowestPredatorCount();

        score += totalCount * lowestCount;
    }

    private void EndOfGameScore()
    {
       Debug.Log("Game Over\n" + "Your Score Was: " + score.ToString("000000"));
    }
}