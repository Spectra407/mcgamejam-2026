using UnityEngine;
using TMPro;

public class Scoreboard : MonoBehaviour
{
    public TextMeshProUGUI scoretext;
    public float gameLength = 300f; // 5 minutes
    private float timer;

    
    public int penaltyPerMinute = 5;

    

    public int[] predatorCounts;   // array comes from Animal class


    public int score;

    private int lastMinute = -1;

    void Start()
    {
        timer = gameLength;
    }

    void Update()
    {
        if (timer <= 0f) return;

        timer -= Time.deltaTime;

        int currentMinute = Mathf.FloorToInt((gameLength - timer) / 60f);

        if (currentMinute != lastMinute)
        {
            CalculateScore();

            

            Debug.Log("Minute: " + currentMinute + " | Score: " + score);
            lastMinute = currentMinute;

            scoretext.text = score;
        }
    }

    void CalculateScore()
    {
        int totalPredators = 0;

        foreach (int count in predatorCounts)
        {
            totalPredators += count;
        }

        int lowestPredator = GetLowestPredatorCount();

        score = (totalPredators * lowestPredator);
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