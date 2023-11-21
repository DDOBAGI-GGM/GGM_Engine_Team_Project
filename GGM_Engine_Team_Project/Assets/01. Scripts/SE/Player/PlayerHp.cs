using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHp : MonoBehaviour
{
    [SerializeField] private GameObject[] hpVisual = new GameObject[8];
    [SerializeField] private TextMeshProUGUI resurrectionTxt;
    [SerializeField] private ResurrectionAnim anim;

    private float maxHP = 3;
    public float MaxHP { get { return maxHP; } set { maxHP = value; } }
    private float playerHp;
    //public float NowPlayerHp { get { return playerHp; } set { playerHp = value; } }

    private int resurrection = 0;
   // public int Resurrection { get { return resurrection; } set {  resurrection = value; } }

    private void Awake()
    {
        HpReSet();
    }

    public void HpReSet()       // 멕스만큼 리셋됨.
    {
        playerHp = maxHP;
        for (int i = 0; i < playerHp; i++)
        {
            hpVisual[i].SetActive(true);
        }
    }

    public void MaxHpUp()
    {
        // 무조건 하나만 늘어남.
        maxHP++;
    }

    public void HpDown(int damage = 1)
    {
        playerHp -= damage;
        Debug.Log(playerHp);
        for (int i = 0; i < hpVisual.Length; i++)
        {
            hpVisual[i].SetActive(false);
        }
        for (int i = 0; i < playerHp; i++)
        {
            hpVisual[i].SetActive(true);
        }

        SoundManager.Instance.PlaySFX("hit");

        if (playerHp <= 0)      // 플레이어 체력이 0이면
        {
            if (resurrection > 0)       // 부활권이 있으면
            {
                Debug.Log("부활권 있어서 부활");
                anim.ResurrectionAnimStart();
                --resurrection;
                UpdateResurrection();
                ResurrectionHp();
            }
            else
            {
                Debug.Log("죽었어용. 게임오버 사운드도 출력해줘!");
                SoundManager.Instance.PlaySFX("over");
                UIManager.Instance.ChangeScene("GameOverScene");
            }
        }
    }

    public void UpdateResurrection(bool add = false)
    {
        if (add)
        {
            resurrection++;
        }
        resurrectionTxt.text = $"X {resurrection}";
    }

    public void ResurrectionHp()
    {
        playerHp = 1;
        hpVisual[0].SetActive(true);
    }
}
