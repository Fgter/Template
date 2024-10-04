using UnityEngine;
using QFramework;
using System;
class Timer : IUtility
{
    float timer;
    float time;
    bool active;
    bool repeat;

    Action callback;
    public Timer()
    {
        CommonMono.AddUpdateAction(Update);
    }

    public void Time(float time, Action callback, bool repeat = false)
    {
        this.time = time;
        timer = 0;
        this.callback = callback;
        this.repeat = repeat;
        active = true;
    }

    void Update()
    {
        if (!active)
            return;
        timer += UnityEngine.Time.deltaTime;

        if (timer >= time)
        {
            callback?.Invoke();
            active = false;
            if (repeat)
                this.Time(time, callback, true);
        }
    }
}
