using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class Timer : MonoBehaviour
{
    public float RemainingTime { get; private set; }
    public bool IsPaused { get; private set; }

    public UnityEvent OnTimerStart;
    public UnityEvent<bool> OnTimerToggle;
    public UnityEvent OnTimerEnd;
    public UnityEvent OnTimerReset;

    private TMP_Text uiText;

    private void Awake()
    {
        RemainingTime = 0;
        IsPaused = true;

        uiText = GetComponentInChildren<TMP_Text>();
    }

    private IEnumerator WaitTime()
    {
        while (true)
        {
            if(!IsPaused)
            {
                RemainingTime -= Time.deltaTime;
                uiText.text = RemainingTime.ToString("F1") + " s";
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
        IsPaused = false;
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
        StopCoroutine(WaitTime());
        RemainingTime = 0;
        IsPaused = true;
        OnTimerReset.Invoke();
    }
}
