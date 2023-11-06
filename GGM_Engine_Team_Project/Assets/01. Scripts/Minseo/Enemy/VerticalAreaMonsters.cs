using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalAreaMonsters : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2.0f;  // �ӵ�
    [SerializeField] private float moveDistance = 5.0f; // �����̴� �Ÿ�

    private Vector3 startPosition; // ���� ��ġ
    private int direction = 1;

    private void Start()
    {
        startPosition = transform.position; // ó�� ��ġ ����
    }

    private void Update()
    {
        ChangeDirectionOfMovement(); // ������ ���� ��ȯ
    }

    private void ChangeDirectionOfMovement()
    {
        transform.Translate(Vector3.up * direction * moveSpeed * Time.deltaTime); // y �������� �̵�

        if (Mathf.Abs(transform.position.y - startPosition.y) >= moveDistance) // ���� �Ÿ� �̵��ϸ� ���� �ٲ��ֱ�
        {
            direction *= -1; // ���� ��ȯ
        }
    }
}

