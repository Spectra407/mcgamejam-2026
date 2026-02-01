using UnityEngine;
using TMPro;
using System.Collections;

public class GameOverScreen : MonoBehaviour
{
    
    public TextMeshProUGUI gameOverScore;

    public Scoreboard scoreboard;
    
    public Canvas gameOverBox;
    
    private bool started = false;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (started) return;
        started = true;
        
        GetComponent<Canvas>(gameOverBox).enabled = false;
        StartCoroutine(ShowText());

    }

    public IEnumerator ShowText()
    {
        yield return new WaitForSeconds(301);
        gameOverScore.text = scoreboard.endOfGameScore;
        GetComponent<Canvas>(gameOverBox).enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }
    
    
}
