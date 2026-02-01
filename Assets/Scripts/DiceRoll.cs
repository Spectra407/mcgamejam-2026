using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;




public class DiceRoll : MonoBehaviour
{
    private Queue queue;
    
    public VideoPlayer videoPlayer;

    void Start()
    {
        queue = GetComponent<Queue>();
        Button button = GetComponent<Button>();
        button.onClick.AddListener(RollDice);
    }

    void RollDice()
    {
        Debug.Log("Dice rolled!");
        queue.RandomizeQueue();

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
