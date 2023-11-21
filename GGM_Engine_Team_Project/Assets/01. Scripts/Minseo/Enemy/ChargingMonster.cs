using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChargingMonster : Enemy
{
    public float direction = 3.5f;   // �÷��̾ ������ x�� ����
    public float chargeSpeed = 10f; // ���� �ӵ�
    public float chargeDuration = 2f; // ���� ���� �ð�

    private Transform player;
    private Rigidbody2D _rigid;
    private Animator _animator;
    private PlayerHp _playerHp; 

    private bool isCharging = false; // ���� ���¸� ��Ÿ���� ����

    private float chargeTimer = 0f; // ���� �ð��� �����ϴ� Ÿ�̸�

    override protected void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // �±׷� �÷��̾� ã��
        _rigid = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _playerHp = player.GetComponent<PlayerHp>();
    }

    override protected void Update()
    {
        _animator.speed = Time.timeScale;

        if (isCharging)
        {
            ChargeTowardsPlayer();
            chargeTimer += Time.deltaTime;

            if (chargeTimer >= chargeDuration)
            {
                isCharging = false;
                chargeTimer = 0f;
                chargeSpeed = 0f;
                Destroy(gameObject);
            }
        }
        else
        {
            DistanceCheck();
            _animator.SetBool("Attack", false);
        }

    }

    private void DistanceCheck()
    {
        Vector2 frontVec = new Vector2(_rigid.position.x + direction * 0.1f, _rigid.position.y - 0.4f);

        RaycastHit2D _rightRayHit = Physics2D.Raycast(frontVec, Vector3.right, direction, LayerMask.GetMask("Player"));
        RaycastHit2D _leftRayHit = Physics2D.Raycast(frontVec, Vector3.left, direction, LayerMask.GetMask("Player"));

        Debug.DrawRay(frontVec, Vector3.right * direction, Color.blue);
        Debug.DrawRay(frontVec, Vector3.left * direction, Color.red);

        if (_rightRayHit.collider != null || _leftRayHit.collider != null)
        {
            isCharging = true;
        }
    }
        
    public void ChargeTowardsPlayer()
    {
        Vector2 targetPosition = new Vector2(player.position.x, _rigid.position.y);
        Vector2 moveDirection = (targetPosition - _rigid.position).normalized;

        _rigid.velocity = new Vector2(moveDirection.x * chargeSpeed, _rigid.velocity.y);

        if (moveDirection.x > 0)
        { 
            transform.localScale = new Vector3(2, 2, 2);
        }
        else if (moveDirection.x < 0)
        {
            transform.localScale = new Vector3(-2, 2, 2);
        }
    }

    override public void Attack()
    {
        _animator.SetBool("Attack", true);
        Debug.Log("������");
        _playerHp.HpDown(1);
    }

    override public void GetDamage(int damage = 1)
    {
        base.GetDamage(damage);
        //EffectTest.Instance.Hit(gameObject, result =>
        //{
        //});

        Destroy(gameObject, 0.5f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            chargeSpeed = 0;
        }
    }
}
