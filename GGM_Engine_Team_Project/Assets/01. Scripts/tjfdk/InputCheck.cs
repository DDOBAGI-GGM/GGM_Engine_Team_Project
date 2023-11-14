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
    [SerializeField] private PlayerActionEnum playerType;
    [SerializeField] private EnemyEnum enemyType;
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
            InputFieldManager.Instance.Input(word, playerType, enemyType, timer);
        } 
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(transform.position, collider.radius);
    //    Gizmos.color = Color.white;
    //}
}
