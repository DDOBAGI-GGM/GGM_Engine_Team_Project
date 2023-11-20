using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHp : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI hpText;

    public float MaxHP = 3;
    public float playerHp;

    private int resurrection = 0;
    public int Resurrection { get { return resurrection; } set {  resurrection = value; } }

    private void Awake()
    {
        playerHp = MaxHP;
        hpText.text = $"X {playerHp}";
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
        SoundManager.Instance.PlaySFX("hit");

        if (playerHp <= 0)      // �÷��̾� ü���� 0�̸�
        {
            if (resurrection > 0)       // ��Ȱ���� ������
            {
                Debug.Log("��Ȱ�� �־ ��Ȱ");
                --resurrection;
                HpUp(1);
            }
            else
            {
                Debug.Log("�׾����. ���ӿ��� ���嵵 �������!");
                //SoundManager.Instance.PlaySFX("");
            }
        }
    }
}
