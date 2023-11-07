using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObject : MonoBehaviour
{
    [SerializeField] private float fallingSpeed = 6f;
    [SerializeField] private float detectionRange = 2.5f;

    private Transform _playerTransform;
    
    private bool isFalling = false;

    void Start()
    {
        _playerTransform = GameObject.FindWithTag("Player").transform; 
    }

    void Update()
    {
        Falling();
    }

    public void StartFalling()
    {
        isFalling = true;
    }

    private void Falling()
    {
        if (!isFalling)
        {
            // 플레이어와의 거리를 계산
            float distanceToPlayer = Vector3.Distance(transform.position, _playerTransform.position);

            if (distanceToPlayer <= detectionRange && _playerTransform.position.y < transform.position.y)
            {
                StartFalling();
            }
        }

        if (isFalling)
        {
            Vector3 newPosition = transform.position;
            newPosition.y -= fallingSpeed * Time.deltaTime;
            transform.position = newPosition;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
}
