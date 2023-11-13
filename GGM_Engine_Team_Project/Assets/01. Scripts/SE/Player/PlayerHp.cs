using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHp : MonoBehaviour
{
    [SerializeField] float nowPlayerHp = 3, maxPlayerHp = 5;
    [SerializeField] Text hpText;

    private bool resurrection = false;

    public void HpUp(int up = 1)
    {
        nowPlayerHp += up;
        hpText.text = $"X {nowPlayerHp} / {maxPlayerHp}";
    }

    public void HpDown(int damage = 1)
    {
        nowPlayerHp -= damage;
        hpText.text = $"X {nowPlayerHp} / {maxPlayerHp}";

        if (nowPlayerHp <= 0)      // 플레이어 체력이 0이면
        {
            if (resurrection)
            {
                Debug.Log("부활권 있어서 부활");
                HpUp(1);
            }
            else
            {
                Debug.Log("죽었어용.");
            }
        }
    }
}
