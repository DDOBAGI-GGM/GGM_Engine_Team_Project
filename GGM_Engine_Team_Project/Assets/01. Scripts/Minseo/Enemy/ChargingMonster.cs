using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargingMonster : MonoBehaviour
{
    public float detectionRange = 5f;   // 플레이어를 감지할 범위
    public float chargeSpeed = 10f;     // 돌진 속도
    public float rotationSpeed = 5f;    // 몬스터의 회전 속도

    private Transform _player;
    private Rigidbody2D _rigid;

    private bool isCharging = false;
    private bool hasReachedPlayer = false;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform; // 태그로 플레이어 찾기
        _rigid = GetComponent <Rigidbody2D>();
    }

    private void Update()
    {
        DistanceCheck();
    }

    private void DistanceCheck()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, _player.position);

        if (distanceToPlayer <= detectionRange && !isCharging)  // 플레이어가 일정 범위 내에 있고 돌진 중이 아니라면 돌진
        {
            ChargeTowardsPlayer();
        }
    }

    private void OnDrawGizmos()
    {
        // 감지 범위를 시각적으로 나타내기
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }

    private void ChargeTowardsPlayer()
    {
        if (!hasReachedPlayer)
        {
            isCharging = true;

            Vector2 direction = (_player.position - transform.position).normalized; //플레이어를 바라보도록 회전
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            _rigid.rotation = angle;

            float distanceToPlayer = Vector3.Distance(transform.position, _player.position);

            if (distanceToPlayer <= detectionRange)
            {
                // 돌진 시작
                Vector2 velocity = direction * chargeSpeed;
                _rigid.velocity = velocity;
            }
            else
            {
                // 플레이어가 detectionRange 밖에 있으면 멈추기
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
            Debug.Log("플레이어 닿기");
        }
    }
}
