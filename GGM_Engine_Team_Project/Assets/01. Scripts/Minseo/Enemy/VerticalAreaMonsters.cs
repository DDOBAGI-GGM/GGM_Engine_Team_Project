using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalAreaMonsters : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2.0f;  // �̵� �ӵ�
    [SerializeField] private float moveDistance = 1.5f; // ������ �Ÿ�
    [SerializeField] private int initialDirection = 1; // �ʱ� �̵� ����
    [SerializeField] private int HP = 1;

    private int direction; // ���� �̵� ����
    private float totalDistanceMoved; // �� �̵��� �Ÿ�'

    PlayerHp _playerHP;

    private void Start()
    {
        _playerHP = GetComponent<PlayerHp>();
        direction = initialDirection; // �ʱ� �̵� ���� ����
    }

    private void Update()
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

    public void Attack()
    {
        _playerHP.HpDown(1);
    }

    public void GetDamage(int damage = 1)
    {
        HP -= damage;

        if(HP <= 0)
        {
            Destroy(gameObject);
        }
    }
}
