using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeComponent;
    [SerializeField] private TextMeshProUGUI waveComponent;
    public float time;

    void Start()
    {
        time = 300;
        UpdateText();
    }

    void Update()
    {
        if (time > 0)
        {
            time -= 50 * Time.deltaTime;
            if (time < 0) time = 0;
        }
        UpdateText();
    }

    void UpdateText()
    {
        int minutes = (int) time / 60;
        int seconds = (int) time % 60;
        timeComponent.text = string.Format("{0}:{1:00}", minutes, seconds);

        int wave = 5 - minutes;
        waveComponent.text = string.Format("Wave {0}/5", wave);
    }
}
