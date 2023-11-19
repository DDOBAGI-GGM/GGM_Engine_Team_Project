using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Windows;
using Unity.VisualScripting;
using TMPro;

public class NPC : SINGLETON<NPC>
{
    [Header("NPC")]
    [SerializeField] private GameObject speech;
    [SerializeField] private int toll;
    [SerializeField] private bool isLock = true;
    [SerializeField] private bool interaction = false;
    [SerializeField] private TextMeshProUGUI tolltxt;

    [SerializeField] CircleCollider2D collider;

    private void Awake()
    {
        collider = GetComponentInChildren<CircleCollider2D>();
        tolltxt.text = "x" + toll;
    }

    private void Lock()
    {
        collider.isTrigger = !isLock;
    }

    public void UnLock(ref int itemCnt)     // ���� �� ���� �Լ�?
    {
        if (interaction)
        {
            if (itemCnt >= toll)
            {
                itemCnt -= toll;
                isLock = false;
                Debug.Log("��� ����!!");
            }
            else
                Debug.Log("���� ���� �����ؿ�...");

            Lock();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)     // �÷��̾ ���Ծ�!
    {
        if (collision.CompareTag("Player"))
        {
            // �÷��̾����� ���� npc�� �˷��ִ�...

            interaction = true;
            speech.SetActive(true);

            speech.transform.DOScale(1.5f, 0.5f).SetLoops(-1, LoopType.Yoyo);

            SoundManager.Instance.PlaySFX("npc");

            Debug.Log(collision.name);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)      // �÷��̾� ������ �� ���ֱ�
    {
        if (collision.CompareTag("Player"))
        {
            interaction = false;
            speech.SetActive(false);

            speech.transform.DOKill();
            speech.transform.localScale = Vector3.one;
        }
    }
}
