using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObject : MonoBehaviour
{
    [SerializeField] private float fallingSpeed = 6f;
    [SerializeField] private float detectionRange = 2.5f;

    private Transform _playerTransform;
    Rigidbody2D _rigid;

    private bool isFalling = false;

    void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _playerTransform = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        Falling();
    }

    private void Falling()
    {
        if (!isFalling)
        {
            Vector2 frontVec = new Vector2(_rigid.position.x + detectionRange * 0.1f, _rigid.position.y);

            RaycastHit2D _downRayHit = Physics2D.Raycast(frontVec, Vector3.down, 2f, LayerMask.GetMask("Player"));

            if (_downRayHit.collider != null)
            {
                isFalling = true;
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
        if (collision.CompareTag("Player") || collision.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
