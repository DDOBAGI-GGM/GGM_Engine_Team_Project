using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalAreaMonsters : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2.0f;  // �̵� �ӵ�
    [SerializeField] private float moveDistance = 1.5f; // ������ �Ÿ�
    [SerializeField] private int initialDirection = 1; // �ʱ� �̵� ����

    private int direction; // ���� �̵� ����
    private float totalDistanceMoved; // �� �̵��� �Ÿ�'

    public bool isTyping = false;

    PlayerHp _playerHP;

    private void Start()
    {
        _playerHP = GetComponent<PlayerHp>();
        direction = initialDirection; // �ʱ� �̵� ���� ����
    }

    private void Update()
    {
        if (!isTyping)
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
        isTyping = false;
        _playerHP.HpDown(1);
    }
}
