using UnityEngine;
using TMPro;
using System.Collections;

public class GameOverScreen : MonoBehaviour
{
    
    public TextMeshProUGUI gameOverScore;

    public Scoreboard scoreboard;
    
    private bool started = false;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        if (started) return;
        started = true;
        
        this.gameObject.SetActive(false);
        StartCoroutine(ShowText());

    }

    public IEnumerator ShowText()
    {
        yield return new WaitForSeconds(6);
        gameOverScore.text = scoreboard.endOfGameScore;
        this.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }
    
    
}
