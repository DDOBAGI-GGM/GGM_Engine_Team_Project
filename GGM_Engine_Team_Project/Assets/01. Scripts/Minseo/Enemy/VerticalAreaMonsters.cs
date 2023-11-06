using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalAreaMonsters : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2.0f;  // 속도
    [SerializeField] private float moveDistance = 5.0f; // 움직이는 거리

    private Vector3 startPosition; // 시작 위치
    private int direction = 1;

    private void Start()
    {
        startPosition = transform.position; // 처음 위치 저장
    }

    private void Update()
    {
        ChangeDirectionOfMovement(); // 움직임 방향 전환
    }

    private void ChangeDirectionOfMovement()
    {
        transform.Translate(Vector3.up * direction * moveSpeed * Time.deltaTime); // y 방향으로 이동

        if (Mathf.Abs(transform.position.y - startPosition.y) >= moveDistance) // 일정 거리 이동하면 방향 바꿔주기
        {
            direction *= -1; // 방향 전환
        }
    }
}

