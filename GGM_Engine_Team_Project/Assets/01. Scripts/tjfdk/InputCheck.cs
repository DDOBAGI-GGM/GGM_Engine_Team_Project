using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
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
    private RaycastHit2D hit;

    private bool check = false;
    [SerializeField] private Enemy obj;

    private void Awake()
    {
        obj = gameObject.transform.parent.parent.GetComponent<Enemy>();
        Debug.Log(obj);
        collider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (InputFieldManager.Instance.Is_typing == false)
            hit = Physics2D.CircleCast(transform.position, radius, Vector3.forward, 0f, layer);

        if (hit.collider != null)
        {
            //if (obj == null) obj.isHiting = false;
            if (!check && GameManager.Instance.GetHp() >= 0 /*&& obj.isHiting == false*/)
            {
                GameManager.Instance.playerMovementTypeSet(true);

                string enemyTag = gameObject.transform.parent.parent.tag;
                Debug.Log(enemyTag);

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

                Debug.Log("적의 hp! : "+EnemyManager.Instance.GetEnemyHp(enemyType));
                if (EnemyManager.Instance.GetEnemyHp(enemyType) <= 0)
                {
                    return;
                }

                GameManager.Instance.TimeSlow();
                InputFieldManager.Instance.Input(word, playerType, enemyType, timer);

                check = true;
            }
        }
        else
            check = false;
    }

    private void OnDrawGizmos()
    {
        if (InputFieldManager.Instance.Is_typing == false)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, radius);
        }
    }
}
