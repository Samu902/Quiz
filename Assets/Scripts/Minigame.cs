using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minigame : MonoBehaviour
{
    public float TotalTime { get; protected set; }

    public virtual void StartMinigame(float duration)
    {
        TotalTime = duration;
    }
}
