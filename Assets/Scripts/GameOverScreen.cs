using UnityEngine;
using TMPro;
using System.Collections;

public class GameOverScreen : MonoBehaviour
{
    
    public TextMeshProUGUI gameOverScore;

    public Scoreboard scoreboard;
    
    public SpriteRenderer gameOverBox;
    
    private bool started = false;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (started) return;
        started = true;
        
        gameOverScore.gameObject.SetActive(false);
        gameOverBox.gameObject.SetActive(false);
        StartCoroutine(ShowText());

    }

    public IEnumerator ShowText()
    {
        yield return new WaitForSeconds(301);
        gameOverScore.text = scoreboard.endOfGameScore;
        gameOverScore.gameObject.SetActive(true);
        gameOverBox.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }
    
    
}
