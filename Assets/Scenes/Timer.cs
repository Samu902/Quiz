﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    public float RemainingTime { get; private set; }
    public bool IsPaused { get; private set; }

    public UnityEvent OnTimerStart;
    public UnityEvent<bool> OnTimerToggle;
    public UnityEvent OnTimerEnd;
    public UnityEvent OnTimerReset;

    private void Start()
    {
        RemainingTime = 0;
        IsPaused = true;
    }

    private IEnumerator WaitTime()
    {
        while (true)
        {
            if(!IsPaused)
            {
                RemainingTime -= Time.deltaTime;
                if (RemainingTime <= 0)
                {
                    OnTimerEnd.Invoke();
                    break;
                }
            }
            yield return new WaitForEndOfFrame();
        }
    }

    public void StartTimer(float time)
    {
        RemainingTime = time;
        IsPaused = true;
        OnTimerStart.Invoke();
        StartCoroutine(WaitTime());
    }

    public void ToggleTimer()
    {
        IsPaused = !IsPaused;
        OnTimerToggle.Invoke(IsPaused);
    }

    public void ResetTimer()
    {
        RemainingTime = 0;
        IsPaused = true;
        OnTimerReset.Invoke();
    }
}
