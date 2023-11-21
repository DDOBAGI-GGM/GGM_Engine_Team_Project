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

    public void HpReSet()       // �߽���ŭ ���µ�.
    {
        playerHp = maxHP;
        for (int i = 0; i < playerHp; i++)
        {
            hpVisual[i].SetActive(true);
        }
    }

    public void MaxHpUp()
    {
        // ������ �ϳ��� �þ.
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

        if (playerHp <= 0)      // �÷��̾� ü���� 0�̸�
        {
            if (resurrection > 0)       // ��Ȱ���� ������
            {
                Debug.Log("��Ȱ�� �־ ��Ȱ");
                anim.ResurrectionAnimStart();
                --resurrection;
                UpdateResurrection();
                ResurrectionHp();
            }
            else
            {
                Debug.Log("�׾����. ���ӿ��� ���嵵 �������!");
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
