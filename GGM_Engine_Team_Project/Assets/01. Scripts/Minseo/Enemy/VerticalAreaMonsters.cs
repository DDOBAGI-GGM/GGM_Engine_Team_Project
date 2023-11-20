using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalAreaMonsters : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2.0f;  // 이동 속도
    [SerializeField] private float moveDistance = 1.5f; // 움직일 거리
    [SerializeField] private int initialDirection = 1; // 초기 이동 방향
    [SerializeField] private int HP = 1;

    private int direction; // 현재 이동 방향
    private float totalDistanceMoved; // 총 이동한 거리'

    PlayerHp _playerHP;

    private void Start()
    {
        _playerHP = GetComponent<PlayerHp>();
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

    public void Attack()
    {
        _playerHP.HpDown(1);
    }

    public void GetDamage(int damage = 1)
    {
        HP -= damage;

        if(HP <= 0)
        {
            Transform parent = transform.parent;

            if (parent != null)
            {
                // 부모 오브젝트의 부모 오브젝트 얻기
                Transform grandparent = parent.parent;

                if (grandparent != null)
                {
                    // 부모의 부모 오브젝트 삭제
                    Destroy(grandparent.gameObject);
                }
                else
                {
                    Debug.Log("부모의 부모 오브젝트가 없습니다.");
                }
            }
            else
            {
                Debug.Log("부모 오브젝트가 없습니다.");
            }
        }
    }
}
