using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using DG.Tweening;
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
    [SerializeField] private Animator tlqfk;

    //[SerializeField] PolygonCollider2D collider;

    private void Awake()
    {
        //collider = GetComponentInChildren<PolygonCollider2D>();
        tolltxt.text = "x" + toll;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
            UnLock(GameManager.Instance.GetItem());
    }

    public void UnLock(int itemCnt)     // 아직 안 쓰는 함수?
    {
        if (interaction)
        {
            tlqfk.SetTrigger("is_interaction");

            if (itemCnt >= toll)
            {
                itemCnt -= toll;
                isLock = false;
                Destroy(gameObject);
            }
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

            SoundManager.Instance.PlaySFX("npc");

            Debug.Log(collision.name);

            if (tlqfk == null)
                tlqfk = collision.GetComponent<Animator>();
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
