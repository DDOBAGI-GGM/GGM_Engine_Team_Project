using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHp : MonoBehaviour
{
    [SerializeField] Text hpText;

    public float MaxHP = 3;
    public float playerHp;

    public bool resurrection = false;

    private void Awake()
    {
        playerHp = MaxHP;
    }

    public void HpUp(int up = 1)
    {
        playerHp += up;
        hpText.text = $"X {playerHp}";
    }

    public void HpDown(int damage = 1)
    {
        playerHp -= damage;
        hpText.text = $"X {playerHp}";

        if (playerHp <= 0)      // 플레이어 체력이 0이면
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
