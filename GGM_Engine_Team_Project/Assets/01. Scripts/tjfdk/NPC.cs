using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Windows;
using Unity.VisualScripting;

public class NPC : MonoBehaviour
{
    [Header("NPC")]
    [SerializeField] private GameObject speech;
    [SerializeField] private int toll;
    [SerializeField] private bool isLock = true;
    [SerializeField] private bool interaction = false;

    [SerializeField] CircleCollider2D collider;

    private void Awake()
    {
        collider = GetComponentInChildren<CircleCollider2D>();
        Debug.Log(collider.name);
    }

    private void Lock()
    {
        collider.isTrigger = !isLock;
    }

    public void UnLock(ref int itemCnt)     // 아직 안 쓰는 함수?
    {
        if (interaction)
        {
            if (itemCnt >= toll)
            {
                itemCnt -= toll;
                isLock = false;
                Debug.Log("얏따 성공!!");
            }
            else
                Debug.Log("저런 돈이 부족해요...");

            Lock();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)     // 플레이어가 들어왔어!
    {
        if (collision.CompareTag("Player"))
        {
            // 플레이어한테 현재 npc를 알려주는...

            interaction = true;
            speech.SetActive(true);

            speech.transform.DOScale(1.5f, 0.5f).SetLoops(-1, LoopType.Yoyo);

            Debug.Log(collision.name);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)      // 플레이어 나가면 다 꺼주기
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
