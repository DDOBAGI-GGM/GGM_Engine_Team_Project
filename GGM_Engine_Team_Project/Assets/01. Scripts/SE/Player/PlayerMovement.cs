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
    [SerializeField] private CapsuleCollider2D capsuleCollider;          // 시리얼라이즈필드지워주기

    private bool is_typing;
    public bool Is_typing { get { return is_typing; } set { is_typing = value; } }
    public bool is_onGround;       // 확인용, 나중에 private 로 변경하기
    public bool Is_onGround { get { return is_onGround; } private set { } }
   // private bool is_onJump;
    //public bool Is_onJump { get { return is_onJump; } set { is_onJump = value; }  }
    private bool is_ladder;

    private PlayerAnimation anim;
    private Vector2 rayOrigin, wallOrigin, raySize;

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<PlayerAnimation>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        raySize = capsuleCollider.size * gameObject.transform.localScale;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Vector2 x = capsuleCollider.bounds.center;
        Vector2 pos = new Vector2(x.x, x.y - raycastDistance);
        Gizmos.DrawWireCube(pos, capsuleCollider.size * gameObject.transform.localScale);
        Vector2 right = new Vector2(x.x + raycastDistance, x.y + 0.2f);
        Vector2 left = new Vector2(x.x - raycastDistance, x.y + 0.2f);
        Gizmos.DrawWireCube(right, capsuleCollider.size * gameObject.transform.localScale);
        Gizmos.DrawWireCube(left, capsuleCollider.size * gameObject.transform.localScale);
        Gizmos.color = Color.white;
    }

    private void Update()
    {
        if (!is_typing)
        {
            // 점프
            if (Input.GetKeyDown(KeyCode.Space))        // 스페이스바를 누르고 바닥에 있을 때와 애니가 재생중이 아닐 때에만
            {
                Debug.Log("점프키를 누름");
                Jump();
            }
        }

        // 플레이어 idle 애니 재생
        if (body.velocity.x != 0)
        {
            anim.Walk(true);
        }
        else
        {
            anim.Walk(false);
        }
    }

    private void FixedUpdate()      // 물리 계산
    {
        if (!is_typing)     // 타이핑 중이 아닐 때에만
        {
            #region 좌우 움직임 및 점프
            float x = Input.GetAxisRaw("Horizontal");

            if(x > 0)
            {
                spriteRenderer.flipX = false;        // 왼쪽
            }
            else if (x < 0)
            {
                spriteRenderer.flipX = true;        // 왼쪽
            }

            rayOrigin = capsuleCollider.bounds.center;
            RaycastHit2D Hit = Physics2D.BoxCast(rayOrigin, raySize, 0f, Vector2.down, raycastDistance, groundMask);        // 콜라이더 중심해서, 콜라이더 사이즈 만큼, 콜라이더는 세로로, 회전은 0, 방향은 아래로. 원점에서 갈정도는 0.2f, 감지할 것은 땅.
             
            if (Hit && body.velocity.y == 0)
            {
                is_onGround = true;        // 바닥이면
                anim.Jump(false);        
            }
            else
            {
                is_onGround = false;
            }
            #endregion

            #region 벽 관련 레이퀘스트
            wallOrigin = new Vector2(rayOrigin.x, rayOrigin.y + 0.2f);
            RaycastHit2D HitRight = Physics2D.BoxCast(wallOrigin, raySize, 0f, Vector2.right, raycastDistance, groundMask);
            RaycastHit2D HitLeft = Physics2D.BoxCast(wallOrigin, raySize, 0f, Vector2.left, raycastDistance, groundMask);
            if (HitRight)        // 왼쪽이나 오른쪽 벽에 닿았다면 x 를 0으로 바꿔줘서 벽에 닿으면 떨어지게
            {
                if (x > 0)      // 오른쪽으로 가고 있으면
                {
                    x = 0;
                }
            }
            if (HitLeft)
            {
                if (x < 0)
                {
                    x = 0;
                }
            }
            #endregion

            body.velocity = new Vector2(x * speed, body.velocity.y);

            #region 사다리 - Y 입력받고 중력 조절해줌.
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
            anim.Walk(false);       // 타이핑 중이니까.        아이들로 변경해줘라
            body.velocity = Vector2.zero;           // 그리고 그 자리에 멈춰라
        }
    }
    
    public void Jump()
    {
        if (is_onGround)
        {
            body.AddForce(Vector2.up * jump, ForceMode2D.Impulse);
            anim.Jump(true);
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

    public void Climb()
    {
        Debug.Log("올라가기에 따른 움직임");
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
