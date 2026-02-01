using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;




public class DiceRoll : MonoBehaviour
{

    
    public VideoPlayer videoPlayer;

    void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(RollDice);
    }

    void RollDice()
    {
        Debug.Log("Dice rolled!");

        if (videoPlayer != null)
        {
            videoPlayer.Play();
        }
        else
        {
            Debug.LogError("VideoPlayer not assigned!");
        }
    }
}
