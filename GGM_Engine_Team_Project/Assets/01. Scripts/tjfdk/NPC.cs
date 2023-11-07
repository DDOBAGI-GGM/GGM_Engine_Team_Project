using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Windows;

public class NPC : MonoBehaviour
{
    [Header("NPC")]
    [SerializeField] private GameObject speech;
    [SerializeField] private int itemCount;
    [SerializeField] private bool islock = true;

    [Header("Ray")]
    [SerializeField] private float radius;

    RaycastHit2D hit;
    BoxCollider2D boxCollider;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        hit = Physics2D.CircleCast(transform.position, radius, Vector2.up);
        if (hit.collider.gameObject != this.gameObject)
        {   
            Debug.Log(hit.collider.name);
            if (hit.collider.CompareTag("Player"))
            {
                speech.SetActive(true);
                speech.transform.DOScale(1.5f, 0.25f).SetLoops(-1, LoopType.Yoyo);
            }
        }
        else
        {
            speech.SetActive(false);
        }

        if (islock)
            boxCollider.isTrigger = true;
    }
    private void OnDrawGizmos()
    {
        // 에디터 창에서 원을 시각화합니다.
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
