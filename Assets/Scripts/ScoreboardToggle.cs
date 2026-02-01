using TMPro;
using UnityEngine;

// This toggles between showing total score and showing how many of each animal you have
public class ScoreboardToggle : MonoBehaviour
{
    private bool scoreboardState;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scoreboardState = false;
    }
    
    public void swapScoreboard() {
        if (!scoreboardState) {
            scoreboardState = true;
            Vector2 pos1  = new Vector2(0f, 200f);
            GameObject.Find("Score").transform.localPosition = pos1;
            Vector2 pos2 = new Vector2(-90f, -115);
            GameObject.Find("Scoreboard 2").transform.localPosition = pos2;
        }
        else {
            scoreboardState = false;
            Vector2 pos1  = new Vector2(0f, 8f);
            GameObject.Find("Score").transform.localPosition = pos1;
            Vector2 pos2 = new Vector2(-90f, 200);
            GameObject.Find("Scoreboard 2").transform.localPosition = pos2;
        }
    }
}
