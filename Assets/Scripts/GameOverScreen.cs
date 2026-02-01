using UnityEngine;
using TMPro;
using System.Collections;

public class GameOverScreen : MonoBehaviour
{
    
    public TextMeshProUGUI gameOverScore;

    public Scoreboard scoreboard;
    
    private bool started = false;

    private Canvas canvas = gameOverScore.GetComponent<Canvas>();
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
