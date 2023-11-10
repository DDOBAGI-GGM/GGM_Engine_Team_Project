using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//[RequireComponent(typeof(Collider))]          // 뭐 어떤 콜라이더든 있을거니까. isTrigger 켜주고
public class InputCheck : MonoBehaviour
{
    [Header("KeyWord")]
    [SerializeField] private string word;
    [SerializeField] private PlayerActionEnum wordType;
    [SerializeField] private float timer;
    [SerializeField] private CircleCollider2D collider;

    private PlayerMovement playerMovement;

    private void Awake()
    {
        collider = GetComponent<CircleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))     // 플레이어가 내 사거리에 닿았으면
        {
            if (playerMovement == null)
            {
                playerMovement = collision.GetComponent<PlayerMovement>();
            }
            playerMovement.Is_typing = true;
            InputFieldManager.Instance.Input(word, wordType, timer);
        }
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(transform.position, collider.radius);
    //    Gizmos.color = Color.white;
    //}
}
