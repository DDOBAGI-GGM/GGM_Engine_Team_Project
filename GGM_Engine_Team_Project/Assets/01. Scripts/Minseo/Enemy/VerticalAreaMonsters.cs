using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalAreaMonsters : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2.0f;  // 이동 속도
    [SerializeField] private float moveDistance = 1.5f; // 움직일 거리
    [SerializeField] private int initialDirection = 1; // 초기 이동 방향

    private int direction; // 현재 이동 방향
    private float totalDistanceMoved; // 총 이동한 거리

    private void Start()
    {
        direction = initialDirection; // 초기 이동 방향 설정
    }

    private void Update()
    {
        MoveObject(); // 오브젝트 이동 처리
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
}
