using UnityEngine;
using TMPro;

public class Scoreboard : MonoBehaviour
{
    public TextMeshProUGUI scoretext;
    public Timer timer;
    public float scoringInterval;

    public int penaltyPerMinute = 5;

    private float lastTime;

    //public int[] predatorCounts;   // array comes from Animal class
    int[] predatorCounts = {1,5,7};

    public int score;

    void Start()
    {
        score = 0;
        lastTime = timer.gameLength;
    }

    void Update()
    {
        float currentTime = timer.GetTime();

        if (lastTime != timer.gameLength && (int) (currentTime / scoringInterval) != (int) (lastTime / scoringInterval))
        {
            CalculateScore();
            scoretext.text = "Score\n" + score.ToString("000000");
        }

        lastTime = currentTime;
    }

    void CalculateScore()
    {
        int totalPredators = 0;

        foreach (int count in predatorCounts)
        {
            totalPredators += count;
        }

        int lowestPredator = GetLowestPredatorCount();

        score += (totalPredators * lowestPredator);
    }

    // gets the lowest predator count from an array
    int GetLowestPredatorCount()
    {
        if (predatorCounts.Length == 0)
            return 0;

        int lowest = predatorCounts[0];

        foreach (int count in predatorCounts)
        {
            if (count < lowest)
                lowest = count;
        }

        return lowest;
    }
}