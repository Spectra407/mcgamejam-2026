using UnityEngine;
using TMPro;
using System.Collections;

public class GameOverScreen : MonoBehaviour
{
    
    public TextMeshProUGUI gameOverScore;

    public Scoreboard scoreboard;
    
    private bool started = false;

    public Canvas canvas;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canvas = gameOverScore.GetComponent<Canvas>();
        
        if (started) return;
        started = true;
        
        canvas.gameObject.SetActive(false);
        StartCoroutine(ShowText());

    }

    public IEnumerator ShowText()
    {
        yield return new WaitForSeconds(301);
        gameOverScore.text = scoreboard.endOfGameScore;
        canvas.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }
    
    
}
