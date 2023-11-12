using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HorizontalAreaMonsters : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2.0f;
    [SerializeField] private float moveDistance = 3.0f;

    Rigidbody2D _rigid;
    SpriteRenderer _spriteRenderer;
    Animator _animator;
    PlayerHp _playerHp;

    private int direction = 1;
    private float totalDistance = 0f;


    private void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _playerHp = GetComponent<PlayerHp>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        _animator.SetBool("Walk", true);
        float distanceThisFrame = direction * moveSpeed * Time.fixedDeltaTime;

        _rigid.velocity = new Vector2(direction * moveSpeed, _rigid.velocity.y); 

        totalDistance += Mathf.Abs(distanceThisFrame);

        Vector2 frontVec = new Vector2(_rigid.position.x + direction * 0.1f, _rigid.position.y);
        Vector2 downfrontVec = new Vector2(_rigid.position.x + direction * 0.7f, _rigid.position.y);

        RaycastHit2D _downRayHit = Physics2D.Raycast(downfrontVec, Vector3.down, 2f, LayerMask.GetMask("Ground"));
        RaycastHit2D _rightRayHit = Physics2D.Raycast(frontVec, Vector3.right, 1f, LayerMask.GetMask("Ground"));
        RaycastHit2D _leftRayHit = Physics2D.Raycast(frontVec, Vector3.left, 1f, LayerMask.GetMask("Ground"));

        Debug.DrawRay(frontVec, Vector3.right * 1f, Color.blue);
        Debug.DrawRay(frontVec, Vector3.left * 1f, Color.red);

        if (totalDistance >= moveDistance || _downRayHit.collider == null ||  _rightRayHit.collider != null || _leftRayHit.collider != null)
        {
            direction = direction * -1;
            _spriteRenderer.flipX = (direction == -1);
            totalDistance = 0f;
        }


    }

    public void Attack()
    {
        _animator.SetTrigger("Attack");
        _playerHp.HpDown(1);
    }
}
