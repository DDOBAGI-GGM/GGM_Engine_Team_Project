using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
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
    private CapsuleCollider2D capsuleCollider;
    private bool is_onGround;
    public bool Is_onGround { get { return is_onGround; } private set { } }
    private bool is_onJump;
    public bool Is_onJump { get { return is_onJump; } set { is_onJump = value; }  }


    private PlayerAnimation anim;

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<PlayerAnimation>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    private void FixedUpdate()
    {
        #region �¿� �����Ӱ� �ִϸ��̼�
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
        #endregion

        RaycastHit2D Hit = Physics2D.CapsuleCast(capsuleCollider.bounds.center, capsuleCollider.size, CapsuleDirection2D.Vertical, 0f, Vector2.down, 0.2f, groundMask);
        // �ݶ��̴� �߽��ؼ�, �ݶ��̴� ������ ��ŭ, �ݶ��̴��� ���η�, ȸ���� 0, ������ �Ʒ���. �������� �������� 0.2f, ������ ���� ��.
        if (Hit && !is_onJump && body.velocity.y == 0) is_onGround = true;
        else is_onGround = false;

        if (is_onGround && body.velocity.y == 0 && !is_onJump)
        {
            anim.JumpingEnd();
        }
    }
    
    public void Jump()
    {
        //Debug.Log("������ ���� ������ - �ٴڿ� �ִٸ�");
        if (is_onJump)
        {
            body.AddForce(new Vector2(body.velocity.x, jump), ForceMode2D.Impulse);
            anim.Jump();
            is_onJump = false;
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
