using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameManager : SINGLETON<GameManager>
{
    [SerializeField] private float timeSlow;
    
    public void TimeSlow() { Time.timeScale = timeSlow; }
    public void TimeReset() { Time.timeScale = 1; }
    public float TimeNormalize(float time) { return time * timeSlow; }
}
