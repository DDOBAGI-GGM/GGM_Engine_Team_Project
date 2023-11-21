using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalAreaMonsters : Enemy
{
    [SerializeField] private float moveSpeed = 2.0f;  // �̵� �ӵ�
    [SerializeField] private float moveDistance = 1.5f; // ������ �Ÿ�
    [SerializeField] private int initialDirection = 1; // �ʱ� �̵� ����

    [SerializeField] private int maxHP = 1;
    [SerializeField] private int HP;

    private int direction; // ���� �̵� ����
    private float totalDistanceMoved; // �� �̵��� �Ÿ�'

    PlayerHp _playerHP;

    override protected void Start()
    {
        HP = maxHP;

        _playerHP = FindObjectOfType<PlayerHp>();
        direction = initialDirection; // �ʱ� �̵� ���� ����
    }

    override protected void Update()
    {
        MoveObject(); // ������Ʈ �̵� ó��    
    }

    private void MoveObject()
    {
        float movement = direction * moveSpeed * Time.deltaTime;
        transform.Translate(Vector3.up * movement);

        totalDistanceMoved += Mathf.Abs(movement);

        if (totalDistanceMoved >= moveDistance)
        {
            totalDistanceMoved = 0;
            direction *= -1;
        }
    }

    override public void Attack()
    {
        _playerHP.HpDown(1);
    }

    override public void GetDamage(int damage = 1)
    {
        HP -= damage;
        base.GetDamage(damage);
        //EffectTest.Instance.Hit(gameObject, result =>
        //{
        //    Debug.Log("������");
        //});

        if (HP <= 0)
        {
            HP = maxHP;

            Destroy(gameObject, 0.5f);
        }
    }
}
