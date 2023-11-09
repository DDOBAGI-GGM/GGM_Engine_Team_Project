using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargingMonster : MonoBehaviour
{
    public float detectionRange = 5f;   // �÷��̾ ������ ����
    public float chargeSpeed = 10f;     // ���� �ӵ�
    public float rotationSpeed = 5f;    // ������ ȸ�� �ӵ�

    private Transform _player;
    private Rigidbody2D _rigid;

    private bool isCharging = false;
    private bool hasReachedPlayer = false;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform; // �±׷� �÷��̾� ã��
        _rigid = GetComponent <Rigidbody2D>();
    }

    private void Update()
    {
        DistanceCheck();
    }

    private void DistanceCheck()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, _player.position);

        if (distanceToPlayer <= detectionRange && !isCharging)  // �÷��̾ ���� ���� ���� �ְ� ���� ���� �ƴ϶�� ����
        {
            ChargeTowardsPlayer();
        }
    }

    private void OnDrawGizmos()
    {
        // ���� ������ �ð������� ��Ÿ����
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }

    private void ChargeTowardsPlayer()
    {
        if (!hasReachedPlayer)
        {
            isCharging = true;

            Vector2 direction = (_player.position - transform.position).normalized; //�÷��̾ �ٶ󺸵��� ȸ��
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            _rigid.rotation = angle;

            float distanceToPlayer = Vector3.Distance(transform.position, _player.position);

            if (distanceToPlayer <= detectionRange)
            {
                // ���� ����
                Vector2 velocity = direction * chargeSpeed;
                _rigid.velocity = velocity;
            }
            else
            {
                // �÷��̾ detectionRange �ۿ� ������ ���߱�
                _rigid.velocity = Vector2.zero;
                hasReachedPlayer = true;
            }
        }
        else
        {

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("�÷��̾� ���");
        }
    }
}
