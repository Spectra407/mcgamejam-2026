using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    
    public TextMeshProUGUI gameOverScore;

    public Image leftHappyHorse;
    public Image rightHappyHorse;

    public Image leftAngryHorse;

    public Image rightAngryHorse;


    public Scoreboard scoreboard;
    
    public Image gameOverSprite;
    
    private bool started = false;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        if (started) return;
        started = true;
        
        rightAngryHorse.gameObject.SetActive(false);
        leftAngryHorse.gameObject.SetActive(false);
        rightHappyHorse.gameObject.SetActive(false);
        leftHappyHorse.gameObject.SetActive(false);
        gameOverScore.gameObject.SetActive(false);
        gameOverSprite.gameObject.SetActive(false);
        StartCoroutine(ShowText());

    }

    public IEnumerator ShowText()
    {
        yield return new WaitForSeconds(6);
        gameOverScore.text = scoreboard.endOfGameScore;
        gameOverScore.gameObject.SetActive(true);
        gameOverSprite.gameObject.SetActive(true);
        if (scoreboard.score > 200)
        {
            rightHappyHorse.gameObject.SetActive(true);
            leftHappyHorse.gameObject.SetActive(true);
        }
        else
        {
            rightAngryHorse.gameObject.SetActive(true);
            leftAngryHorse.gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }
    
    
}
