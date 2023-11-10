using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargingMonster : MonoBehaviour
{
    public float direction = 5f;   // 플레이어를 감지할 x축 범위
    public float chargeSpeed = 10f; // 돌진 속도

    private Transform player;
    private Rigidbody2D _rigid;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // 태그로 플레이어 찾기
        _rigid = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        DistanceCheck();
    }

    private void DistanceCheck()
    {
        Vector2 frontVec = new Vector2(_rigid.position.x + direction * 0.1f, _rigid.position.y);

        RaycastHit2D _rightRayHit = Physics2D.Raycast(frontVec, Vector3.right, direction, LayerMask.GetMask("Player"));
        RaycastHit2D _leftRayHit = Physics2D.Raycast(frontVec, Vector3.left, direction, LayerMask.GetMask("Player"));

        Debug.DrawRay(frontVec, Vector3.right * direction, Color.blue);
        Debug.DrawRay(frontVec, Vector3.left * direction, Color.red);

        if (_rightRayHit.collider != null || _leftRayHit.collider != null)
        {
            ChargeTowardsPlayer();
        }
    }

    private void ChargeTowardsPlayer()
    {
        Vector2 targetPosition = new Vector2(player.position.x, _rigid.position.y);
        Vector2 moveDirection = (targetPosition - _rigid.position).normalized;

        _rigid.velocity = new Vector2(moveDirection.x * chargeSpeed, _rigid.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("플레이어 충돌");
        chargeSpeed = 0;
    }
}
