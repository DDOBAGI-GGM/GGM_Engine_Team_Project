using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThornObject : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // �÷��̾� hp --�ϴ� �ڵ�
            Debug.Log("�÷��̾� �δ�ħ����");
        }
    }
}
