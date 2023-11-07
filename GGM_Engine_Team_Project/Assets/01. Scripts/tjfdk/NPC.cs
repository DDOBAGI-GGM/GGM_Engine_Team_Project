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

    //[Header("Ray")]
    //[SerializeField] private float radius;

    //RaycastHit2D hit;
    BoxCollider2D boxCollider;

    private void Awake()
    {
        boxCollider = GetComponentInChildren<BoxCollider2D>();
        Debug.Log(boxCollider.name);
    }

    //private void Update()
    //{
    //hit = Physics2D.CircleCast(transform.position, radius, Vector2.up);
    //if (hit.collider.gameObject != this.gameObject)
    //{   
    //    Debug.Log(hit.collider.name);
    //    if (hit.collider.CompareTag("Player"))
    //    {
    //        speech.SetActive(true);
    //        speech.transform.DOScale(1.5f, 0.25f).SetLoops(-1, LoopType.Yoyo);
    //    }
    //}
    //else
    //{
    //    speech.SetActive(false);
    //}
    //if (islock)
    //    boxCollider.isTrigger = true;
    //}

    //private void OnDrawGizmos()
    //{
    //    // 에디터 창에서 원을 시각화합니다.
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(transform.position, radius);
    //}
    private void Lock()
    {
        boxCollider.isTrigger = !isLock;
    }

    public void UnLock(ref int itemCnt)
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            interaction = true;
            speech.SetActive(true);

            speech.transform.DOScale(1.5f, 0.5f).SetLoops(-1, LoopType.Yoyo);

            Debug.Log(collision.name);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
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
