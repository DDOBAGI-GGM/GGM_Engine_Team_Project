using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

//[RequireComponent(typeof(Collider))]          // 뭐 어떤 콜라이더든 있을거니까. isTrigger 켜주고
public class InputCheck : MonoBehaviour
{
    [Header("KeyWord")]
    [SerializeField] private string word;
    [SerializeField] private PlayerActionEnum playerType;
    [SerializeField] private EnemyEnum enemyType;
    [SerializeField] private float timer;
    [SerializeField] private BoxCollider2D collider;

    [Header("Raycast")]
    [SerializeField] private float radius;
    [SerializeField] private LayerMask layer;

    [Header("TimeScale")]
    [SerializeField] private float timeSlow;

    private PlayerMovement playerMovement;
    private RaycastHit2D hit;

    private bool check = false;

    private void Awake()
    {
        collider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        hit = Physics2D.CircleCast(transform.position, radius, Vector3.forward, 0f, layer);

        if (hit.collider != null)
        {
            if (!check)
            {
                if (playerMovement == null)
                    playerMovement = hit.collider.GetComponent<PlayerMovement>();

                string enemyTag = hit.collider.tag;

                switch (enemyTag)
                {
                    case "CEnemy":
                        enemyType = EnemyEnum.Charging;
                        break;
                    case "HEnemy":
                        enemyType = EnemyEnum.HorizontalArea;
                        break;
                    case "VEnemy":
                        enemyType = EnemyEnum.VerticalArea;
                        break;
                }

                InputFieldManager.Instance.Input(word, playerType, enemyType, timer);
                GameManager.Instance.TimeSlow();

                check = true;
            }
        }
        else
            check = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
