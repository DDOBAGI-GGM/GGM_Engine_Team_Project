using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour, IPlayerAction
{
    [SerializeField] private float speed = 5;
    [SerializeField] private float jump = 5;
    [SerializeField] private float gravity = 1;
    [SerializeField] private float raycastDistance;
    [SerializeField] LayerMask groundMask;
    
    private Rigidbody2D body;
    private SpriteRenderer spriteRenderer;
    private PlayerHp hp;
    public float GetHP() { return hp.NowPlayerHp; }
    private EffectTest effectTest;
    [SerializeField] private PlayerParticle particle;
    [SerializeField] private CapsuleCollider2D capsuleCollider;          // �ø���������ʵ������ֱ�
    [SerializeField] private Animator jumpParticle;

    private bool is_typing;
    public bool Is_typing 
    {
        get
        { 
            return is_typing; 
        }
        set 
        {
            //Debug.Log("Ÿ���� �ٲ��! : " + value);
            is_typing = value; 
        }
    }
    
    [SerializeField] private bool is_onGround;       // Ȯ�ο�, ���߿� private �� �����ϱ�
    public bool Is_onGround { get { return is_onGround; } set { is_onGround = value; }  }
   
    [SerializeField] private bool is_Jumping;
   // private bool is_onJump;
    public bool Is_Jumping { get { return is_Jumping; } set { is_Jumping = value; }  }
    
    [SerializeField] private bool is_ladder;
    public bool Is_ladder { get { return is_ladder; } set { is_ladder = value; } }
    private bool first_ladder =false;

    private PlayerAnimation anim;
    private Vector2 rayOrigin, raySize;
    private bool attackFirst = true;
    private bool wallParticleFirst = false;

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        hp = GetComponent<PlayerHp>();
        effectTest = GetComponent<EffectTest>();
        anim = GetComponent<PlayerAnimation>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        raySize = new Vector2(capsuleCollider.size.x * gameObject.transform.localScale.x, capsuleCollider.size.y * gameObject.transform.localScale.y / 2);
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        Vector2 x = capsuleCollider.bounds.center;
        Vector2 bottomSize = capsuleCollider.size * gameObject.transform.localScale;
        Vector2 bottomPos = new Vector2(x.x, x.y - (raycastDistance + gameObject.transform.localScale.y * 0.2f));

        bottomSize = new Vector2(bottomSize.x, bottomSize.y / 2);
        Gizmos.DrawWireCube(bottomPos, bottomSize);

        Vector2 right = new Vector2(x.x + raycastDistance, x.y);
        Vector2 left = new Vector2(x.x - raycastDistance, x.y);
        Vector2 size = new Vector2(capsuleCollider.size.x * gameObject.transform.localScale.x, capsuleCollider.size.y * gameObject.transform.localScale.y / 2);

        Gizmos.DrawWireCube(right, size);
        Gizmos.DrawWireCube(left, size);

        Gizmos.color = Color.white;
    }
#endif

    private void Update()
    {
        if (!is_typing)
        {
            // ����
            if (Input.GetKeyDown(KeyCode.Space))        // �����̽��ٸ� ������ �ٴڿ� ���� ���� �ִϰ� ������� �ƴ� ������
            {
                Jump();
            }
        }

        // �÷��̾� idle �ִ� ���
        if (Mathf.Abs(body.velocity.x) > 1)
        {
            anim.Walk(true);
        }
        else
        {
            anim.Walk(false);
        }
    }

    private void FixedUpdate()      // ���� ���
    {
        if (!is_typing)     // Ÿ���� ���� �ƴ� ������
        {
            #region �¿� ������ �� ����
            float x = Input.GetAxisRaw("Horizontal");

            if(x > 0)
            {
                spriteRenderer.flipX = false;        // ����
            }
            else if (x < 0)
            {
                spriteRenderer.flipX = true;        // ����
            }

            rayOrigin = capsuleCollider.bounds.center;
            Vector2 bottomPos = new Vector2(rayOrigin.x, rayOrigin.y - (raycastDistance + gameObject.transform.localScale.y * 0.2f));
            RaycastHit2D Hit = Physics2D.BoxCast(bottomPos, raySize, 0f, Vector2.down, raycastDistance, groundMask);        // �ݶ��̴� �߽��ؼ�, �ݶ��̴� ������ ��ŭ, �ݶ��̴��� ���η�, ȸ���� 0, ������ �Ʒ���. �������� �������� 0.2f, ������ ���� ��.

            if (Hit&&Hit.collider.gameObject.CompareTag("Obstacle"))
            {
                switch (Hit.collider.gameObject.name)
                {
                    case "one":
                        gameObject.transform.position = new Vector2(35, 9.2f);
                        break;
                    case "Two":
                        gameObject.transform.position = new Vector2(23, 47.2f);
                        break;
                    case "Three":
                        gameObject.transform.position = new Vector2(42.2f, 72.2f);
                        break;
                    default:
                        Debug.Log("�׳� ���Ŀ�");
                        break;
                }

                //effectTest.Hit(gameObject);
                EffectTest.Instance.Hit(gameObject, result =>
                {
                    //if (result)
                    // Ÿ�� ����Ʈ ���� �� ������ �͵� (ex. destory, ����Ʈ �����ְ� �������� �� �Ƴ�) ���� �� �����ְ�� ���Ұǵ�
                    //hp.HpDown(1);
                });

                if (attackFirst)
                {
                    hp.HpDown(1);
                    attackFirst = false;
                }

            }
            else
            {
                attackFirst = true;
            }

            //Debug.Log();
            if (Hit && Mathf.Abs(body.velocity.y) < 1)      // y �� ���ν�Ƽ�� 0�̸�, �� �������°� �ƴϸ�
            {
                anim.Jump(false);
                body.velocity = new Vector2(body.velocity.x, 0);
                is_onGround = true;        // �ٴ�
                is_Jumping = false;
            }
            else
            {
                is_onGround = false;
            }
            #endregion

            #region �� ���� ��������Ʈ (����, ������)
            RaycastHit2D HitRight = Physics2D.BoxCast(rayOrigin, raySize, 0f, Vector2.right, raycastDistance, groundMask);
            RaycastHit2D HitLeft = Physics2D.BoxCast(rayOrigin, raySize, 0f, Vector2.left, raycastDistance, groundMask);
            if (HitRight)        // �����̳� ������ ���� ��Ҵٸ� x �� 0���� �ٲ��༭ ���� ������ ��������
            {
                if (x > 0)      // ���������� ���� ������
                {
                    x = 0;
                }
                if (wallParticleFirst)
                {
                    particle.WallParticlePlay(true);
                    wallParticleFirst = false;
                }
            }
            if (HitLeft)
            {
                if (x < 0)
                {
                    x = 0;
                }
                if (wallParticleFirst)
                {
                    particle.WallParticlePlay(false);
                    wallParticleFirst = false;
                }
            }
            if (!HitRight && !HitLeft)
            {
                wallParticleFirst = true;
            }

            #endregion

            body.velocity = new Vector2(x * speed, body.velocity.y);

            #region ��ٸ� - Y �Է¹ް� �߷� ��������.
            if (is_ladder)
            {
                anim.Jump(false);
                float y = Input.GetAxisRaw("Vertical");
                    body.gravityScale = 0;
                    body.velocity = new Vector2(body.velocity.x, y * speed);
                first_ladder = true;
            }
            else
            {
                body.gravityScale = gravity;
                if (first_ladder && !is_Jumping)
                {
                    Debug.Log("��ٸ��� �ƴ�");
                    body.velocity = Vector2.zero;
                    first_ladder = false;
                }
            }
            #endregion
        }
        else
        {
            anim.Walk(false);       // Ÿ���� ���̴ϱ�.        ���̵�� ���������
            body.velocity = Vector2.zero;           // �׸��� �� �ڸ��� �����
        }
    }
    
    public void Jump(bool unconditional = false)
    {
        if ((is_onGround && !is_ladder) || unconditional) 
        {
            //Debug.Log("��������");
            SoundManager.Instance.PlaySFX("jump");

            jumpParticle.gameObject.SetActive(true);
            jumpParticle.gameObject.transform.position = new Vector2(transform.position.x, transform.position.y - gameObject.transform.localScale.x / 2);

            is_Jumping = true;
            is_ladder = false;
            body.velocity = new Vector2(0, 0);
            body.AddForce(Vector2.up * jump, ForceMode2D.Impulse);
            anim.Jump(true);
        }
    }

    public void Attack()
    {
        Debug.Log("���ÿ� ���� ������");
        SoundManager.Instance.PlaySFX("attack");
        anim.Attack();
    }

    public void Avoidance()
    {
        Debug.Log("���ϱ⿡ ���� ������");
        anim.Avoidance();
    }

    public void Climb()
    {
        Debug.Log("�ö󰡱⿡ ���� ������");
    }
}
