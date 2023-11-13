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

        if (nowPlayerHp <= 0)      // �÷��̾� ü���� 0�̸�
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
