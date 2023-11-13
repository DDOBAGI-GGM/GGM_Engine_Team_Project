using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//[RequireComponent(typeof(Collider))]          // �� � �ݶ��̴��� �����Ŵϱ�. isTrigger ���ְ�
public class InputCheck : MonoBehaviour
{
    [Header("KeyWord")]
    [SerializeField] private string word;
    [SerializeField] private PlayerActionEnum wordType;
    [SerializeField] private float timer;
    [SerializeField] private BoxCollider2D collider;

    private PlayerMovement playerMovement;
    private ChargingMonster ChargingMonster;
    private HorizontalAreaMonsters HorizontalAreaMonsters;
    private VerticalAreaMonsters VerticalAreaMonsters;

    private void Awake()
    {
        collider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))     // �÷��̾ �� ��Ÿ��� �������
        {
            if (playerMovement == null)
            {
                playerMovement = collision.GetComponent<PlayerMovement>();
            }

            playerMovement.Is_typing = true;
                
        }
        else if (collision.CompareTag("CEnemy"))
        {
            if(ChargingMonster == null)
            {
                ChargingMonster = collision.GetComponent<ChargingMonster>();
            }

            ChargingMonster.isTyping = true;
        }
        else if (collision.CompareTag("HEnemy"))
        {
            if (HorizontalAreaMonsters == null)
            {
                HorizontalAreaMonsters = collision.GetComponent<HorizontalAreaMonsters>();
            }

            HorizontalAreaMonsters.isTyping = true;
        }
        else if (collision.CompareTag("VEnemy"))
        {
            if (VerticalAreaMonsters == null)
            {
                VerticalAreaMonsters = collision.GetComponent<VerticalAreaMonsters>();
            }

            ChargingMonster.isTyping = true;
        }
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(transform.position, collider.radius);
    //    Gizmos.color = Color.white;
    //}
}
