using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public static Timer instance;

    [SerializeField] private TextMeshProUGUI timeComponent;
    [SerializeField] private TextMeshProUGUI waveComponent;

    public float gameLength = 300;
    public List<IntervalAction> intervalActions;
    private float time;

    void Awake()
    {
        if (instance != null) Destroy(this);
        instance = this;

        time = gameLength;
        intervalActions = new();
        UpdateText();
    }

    void Start()
    {
        
    }

    void Update()
    {
        float lastTime = time;

        if (time > 0)
        {
            time -= Time.deltaTime;
            if (time < 0) time = 0;
        }

        if (lastTime != gameLength) // stops actions from executing right at start
        {
            foreach (IntervalAction intervalAction in intervalActions)
            {
                if ((int) (time / intervalAction.interval) != (int) (lastTime / intervalAction.interval))
                {
                    intervalAction.action.Invoke();
                }
            }
        }

        UpdateText();
    }

    public void AddIntervalAction(float interval, Action action)
    {
        intervalActions.Add(new IntervalAction(interval, action, 0));
    }

    public void AddIntervalAction(float interval, Action action, int priority)
    {
        IntervalAction ia = new IntervalAction(interval, action, priority);
        for (int i = intervalActions.Count; i > 0; i--)
        {
            if (intervalActions[i - 1].priority >= priority)
            {
                intervalActions.Insert(i, ia);
                return;
            }
        }
        intervalActions.Insert(0, ia);
    }

    void UpdateText()
    {
        int minutes = (int) time / 60;
        int seconds = (int) time % 60;
        timeComponent.text = string.Format("{0}:{1:00}", minutes, seconds);

        int wave = 5 - minutes;
        waveComponent.text = string.Format("Wave {0}/5", wave);
    }

    public float GetTime()
    {
        return time;
    }
}
