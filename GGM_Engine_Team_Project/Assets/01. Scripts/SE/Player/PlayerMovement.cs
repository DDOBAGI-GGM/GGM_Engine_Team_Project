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
        if (x < 0)      // ����
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

        // �ϳ��� �ٴڿ� ��������� true ��.
        raycastPos.ToList().ForEach(pos => { 
            RaycastHit2D ray = Physics2D.Raycast(pos.position, Vector2.down, raycastDistance, groundMask);
            Debug.DrawRay(pos.position, new Vector2(0, -0.1f), Color.red);
            if (ray)
            {
                Debug.Log("�ٴ���");
                is_onGround = true;
                anim.JumpingEnd();          // �ٴ��̿��� ��      ���⼭ �ٷ� �Ǿ������ ������ ����� ����
            }
            else
            {
                Debug.Log("�ٴھƴ�");
            }
        });
    }
    
    public void Jump()
    {
        Debug.Log("������ ���� ������ - �ٴڿ� �ִٸ�");
        if (is_onGround)
        {
            body.AddForce(new Vector2(body.velocity.x, jump), ForceMode2D.Impulse);
        }
    }

    public void Attack()
    {
        Debug.Log("���ÿ� ���� ������");
    }

    public void Avoidance()
    {
        Debug.Log("���ϱ⿡ ���� ������");
    }
}
