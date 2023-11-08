using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalAreaMonsters : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2.0f;  // 속도

    Rigidbody2D _rigid;

    private int direction = 1;
    private bool isGrounded = true; // 처음에는 땅에 닿아 있다고 가정

    private void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        _rigid.velocity = new Vector2(direction, _rigid.velocity.y);

        //지형 체크
        Vector2 frontVec = new Vector2(_rigid.position.x + direction * 0.4f, _rigid.position.y);

        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Ground"));
        if (rayHit.collider == null) 
        {
            direction = direction * -1;
        }
    }

}
