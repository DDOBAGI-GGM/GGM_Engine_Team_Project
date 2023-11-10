using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalAreaMonsters : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2.0f;
    Rigidbody2D _rigid;
    SpriteRenderer _spriteRenderer;

    private int direction = 1;

    private void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        _rigid.velocity = new Vector2(direction * moveSpeed, _rigid.velocity.y); // y 축의 속도를 유지하지 않도록 수정

        Vector2 frontVec = new Vector2(_rigid.position.x + direction * 0.1f, _rigid.position.y);
        Vector2 downfrontVec = new Vector2(_rigid.position.x + direction * 0.7f, _rigid.position.y);

        RaycastHit2D _downRayHit = Physics2D.Raycast(downfrontVec, Vector3.down, 2f, LayerMask.GetMask("Ground"));
        RaycastHit2D _rightRayHit = Physics2D.Raycast(frontVec, Vector3.right, 1f, LayerMask.GetMask("Ground"));
        RaycastHit2D _leftRayHit = Physics2D.Raycast(frontVec, Vector3.left, 1f, LayerMask.GetMask("Ground"));

        Debug.DrawRay(frontVec, Vector3.right * 1f, Color.blue);
        Debug.DrawRay(frontVec, Vector3.left * 1f, Color.red);

        if (_downRayHit.collider == null ||  _rightRayHit.collider != null || _leftRayHit.collider != null)
        {
            direction = direction * -1;
            _spriteRenderer.flipX = (direction == -1);
        }


    }
}
