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

    [SerializeField] private PlayerHp playerHp;
    [SerializeField] private PlayerMovement playerMovement;

    public void TimeSlow() { Time.timeScale = timeSlow; }
    public void TimeReset() { Time.timeScale = 1; }
    public float TimeNormalize(float time) { return time * timeSlow; }

    public void SetItem() { itemCount++; }
    public int GetItem() { return itemCount; }

    public void HpDown(int down = 1)
    {
        playerHp.HpDown(down);
    }

    public int GetHp()
    {
        return (int)playerHp.NowPlayerHp;
    }

    public void playerMovementTypeSet(bool value)
    {
        playerMovement.Is_typing = value;
    }

    public bool playerMovementTypeGet()
    {
        return playerMovement.Is_typing;
    }
}
