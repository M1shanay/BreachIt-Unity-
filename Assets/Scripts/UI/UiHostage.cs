using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UiHostage : MonoBehaviour
{
    public UnityEvent OnSaveHostageStart;
    public UnityEvent OnSaveHostageEnd;
    public UnityEvent<float> OnSaveHostageStartTimer;
    public UnityEvent OnSaveHostageEndTimer;
    public UnityEvent OnSaveHostageTimerFinish;
    public void SendUIHostageStart()
    {
        OnSaveHostageStart.Invoke();
    }
    public void SendUIHostageEnd()
    {
        OnSaveHostageEnd.Invoke();
    }
    public void SendUIHostageStartTimer(float time)
    {
        OnSaveHostageStartTimer.Invoke(time);
    }
    public void SendUIHostageTimerFinish()
    {
        OnSaveHostageTimerFinish.Invoke();
    }
    public void SendUIHostageEndTimer()
    {
        OnSaveHostageEndTimer.Invoke();
    }
}
