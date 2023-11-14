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
