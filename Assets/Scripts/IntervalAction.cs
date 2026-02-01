using System;
using UnityEngine;

public class IntervalAction
{
    public float interval;
    public Action action;
    // 0 = least important, 999999 = most important
    public int priority;

    public IntervalAction(float interval, Action action, int priority)
    {
        this.interval = interval;
        this.action = action;
        this.priority = priority;
    }
}
