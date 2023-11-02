using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour, IPlayerAction
{
    [SerializeField] private float speed = 5;
    [SerializeField] private float jump = 5;
    private Rigidbody2D body;

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");

        Vector2 pos = new Vector2 (x * speed, body.velocity.y);

        body.velocity = pos;
    }
    
    public void Jump()
    {
        Debug.Log("점프에 따른 움직임");
        body.AddForce(new Vector2 (body.velocity.x, jump), ForceMode2D.Impulse);
    }

    public void Attack()
    {
        Debug.Log("어택에 따른 움직임");
    }

    public void Avoidance()
    {
        Debug.Log("피하기에 따른 움직임");
    }
}
