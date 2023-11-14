using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHp : MonoBehaviour
{
    [SerializeField] Text hpText;

    public float playerHp = 3;

    private int resurrection = 0;
    public int Resurrection { get { return resurrection; } set {  resurrection = value; } }

    public void HpUp(int up = 1)
    {
        playerHp += up;
        hpText.text = $"X {playerHp}";
    }

    public void HpDown(int damage = 1)
    {
        playerHp -= damage;
        hpText.text = $"X {playerHp}";
        SoundManager.Instance.PlaySFX("hit");

        if (playerHp <= 0)      // 플레이어 체력이 0이면
        {
            if (resurrection > 0)       // 부활권이 있으면
            {
                Debug.Log("부활권 있어서 부활");
                --resurrection;
                HpUp(1);
            }
            else
            {
                Debug.Log("죽었어용. 게임오버 사운드도 출력해줘!");
                //SoundManager.Instance.PlaySFX("");
            }
        }
    }
}
