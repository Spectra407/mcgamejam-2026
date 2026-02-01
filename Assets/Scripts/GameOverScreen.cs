using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class GameOverScreen : MonoBehaviour
{
    
    public TextMeshProUGUI gameOverScore;

    public Scoreboard scoreboard;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        gameOverScore.text = endOfGameScore;

    }

    // Update is called once per frame
    void Update()
    {
        
        
    }
    
    
}
