using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour, IPlayerAction
{
    [SerializeField] private float speed = 5;
    [SerializeField] private float jump = 5;
    [SerializeField] private float raycastDistance;
    [SerializeField] LayerMask groundMask;
    
    private Rigidbody2D body;
    private SpriteRenderer spriteRenderer;
    private CapsuleCollider2D capsuleCollider;          // �ø���������ʵ������ֱ�

    private bool is_typing;
    public bool Is_typing { get { return is_typing; } set { is_typing = value; } }
    public bool is_onGround;       // Ȯ�ο�, ���߿� private �� �����ϱ�
    public bool Is_onGround { get { return is_onGround; } private set { } }
   // private bool is_onJump;
    //public bool Is_onJump { get { return is_onJump; } set { is_onJump = value; }  }
    private bool is_ladder;

    private PlayerAnimation anim;
    private Vector2 rayOrigin, wallOrigin;

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<PlayerAnimation>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Vector2 pos = new Vector2(rayOrigin.x, rayOrigin.y - raycastDistance);
        Gizmos.DrawWireCube(pos, capsuleCollider.size);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))        // �����̽��ٸ� ������ �ٴڿ� ���� ���� �ִϰ� ������� �ƴ� ������
        {
            Debug.Log("����Ű�� ����");
            if (is_onGround)
            {
                Debug.Log("�ٴ���");
                if (anim.jump_ok)
                {
                    Debug.Log("�ִ�������� �ƴ�");
                    Jump();
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if (!is_typing)     // Ÿ���� ���� �ƴ� ������
        {
            #region �¿� ������ �� ������ ���� ����� �� velocity.x ����
            float x = Input.GetAxisRaw("Horizontal");

            rayOrigin = capsuleCollider.bounds.center;
            RaycastHit2D Hit = Physics2D.BoxCast(rayOrigin, capsuleCollider.size, 0f, Vector2.down, raycastDistance, groundMask);
            // �ݶ��̴� �߽��ؼ�, �ݶ��̴� ������ ��ŭ, �ݶ��̴��� ���η�, ȸ���� 0, ������ �Ʒ���. �������� �������� 0.2f, ������ ���� ��.

            if (Hit && body.velocity.y == 0)
            {
                is_onGround = true;        // �ٴڿ� �浹�� �Ǿ��� �������� ���� ���� ���¶��
                anim.JumpingEnd();
            }
            else
            {
                is_onGround = false;
            }

            wallOrigin = new Vector2(rayOrigin.x, rayOrigin.y + 0.2f);
            RaycastHit2D HitRight = Physics2D.BoxCast(wallOrigin, capsuleCollider.size, 0f, Vector2.right, raycastDistance, groundMask);
            RaycastHit2D HitLeft = Physics2D.BoxCast(wallOrigin, capsuleCollider.size, 0f, Vector2.left, raycastDistance, groundMask);
            if (HitRight || HitLeft)        // �����̳� ������ ���� ��Ҵٸ� x �� 0���� �ٲ��༭ ���� ������ ��������
            {
                x = 0;
            }

            body.velocity = new Vector2(x * speed, body.velocity.y);
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

            #region ��ٸ� - Y �Է¹ް� �߷� ��������.
            if (is_ladder)
            {
                float y = Input.GetAxisRaw("Vertical");
                body.gravityScale = 0;
                body.velocity = new Vector2(body.velocity.x, y * speed);
            }
            else
            {
                body.gravityScale = 1;
            }
            #endregion
        }
        else
        {
            anim.Walk(false);       // Ÿ���� ���̴ϱ�.        ���̵�� ���������
            body.velocity = Vector2.zero;           // �׸��� �� �ڸ��� �����!
        }
    }
    
    public void Jump()
    {
        //Debug.Log("������ ���� ������ - �ٴڿ� �ִٸ�");
        if (anim.Jump_ok)
        {
            body.AddForce(new Vector2(body.velocity.x, jump), ForceMode2D.Impulse);
            anim.Jump();
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

    public void Climb()
    {
        Debug.Log("�ö󰡱⿡ ���� ������");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            is_ladder = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            is_ladder = false;
        }
    }
}
