using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameManager : SINGLETON<GameManager>
{
    [Header("Time")]
    [SerializeField] private float timeSlow;
    
    [Header("Item")]
    [SerializeField] private int itemCount = 0;

    public void TimeSlow() { Time.timeScale = timeSlow; }
    public void TimeReset() { Time.timeScale = 1; }
    public float TimeNormalize(float time) { return time * timeSlow; }

    public void SetItem() { itemCount++; }
    public int GetItem() { return itemCount; }
}
