using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHp : MonoBehaviour
{
    [SerializeField] Text hpText;

    public float playerHp = 3;

    public bool resurrection = false;

    public void HpUp(int up = 1)
    {
        playerHp += up;
        hpText.text = $"X {playerHp}";
    }

    public void HpDown(int damage = 1)
    {
        playerHp -= damage;
        hpText.text = $"X {playerHp}";

        if (playerHp <= 0)      // �÷��̾� ü���� 0�̸�
        {
            if (resurrection)
            {
                Debug.Log("��Ȱ�� �־ ��Ȱ");
                HpUp(1);
            }
            else
            {
                Debug.Log("�׾����.");
            }
        }
    }
}
