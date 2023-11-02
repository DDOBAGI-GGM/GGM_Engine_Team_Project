using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerMovement : MonoBehaviour, IPlayerAction
{
    [SerializeField] private float speed = 5;
    [SerializeField] private float jump = 5;
    [SerializeField] private Transform[] raycastPos;
    [SerializeField] private float raycastDistance;
    [SerializeField] LayerMask groundMask;
    
    private Rigidbody2D body;
    private SpriteRenderer spriteRenderer;
    private bool is_onGround;
    public bool Is_onGround { get { return is_onGround; } private set { } }

    private PlayerAnimation anim;

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<PlayerAnimation>();
    }

    private void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        body.velocity = new Vector2 (x * speed, body.velocity.y);
        if (x < 0)      // 왼쪽
        {
            spriteRenderer.flipX = true;
        }
        else if (x > 0)
        {
            spriteRenderer.flipX = false;
        }

        if (x != 0)
        {
            anim.Walk(true);
        }
        else { anim.Walk(false); }

        is_onGround = false;    

        // 하나라도 바닥에 닿아있으면 true 임.
        raycastPos.ToList().ForEach(pos => { 
            RaycastHit2D ray = Physics2D.Raycast(pos.position, Vector2.down, raycastDistance, groundMask);
            Debug.DrawRay(pos.position, new Vector2(0, -0.1f), Color.red);
            if (ray)
            {
                Debug.Log("바닥임");
                is_onGround = true;
                anim.JumpingEnd();          // 바닥이여서 끝      여기서 바로 되어버려서 문제가 생기는 것임
            }
            else
            {
                Debug.Log("바닥아님");
            }
        });
    }
    
    public void Jump()
    {
        Debug.Log("점프에 따른 움직임 - 바닥에 있다면");
        if (is_onGround)
        {
            body.AddForce(new Vector2(body.velocity.x, jump), ForceMode2D.Impulse);
        }
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
