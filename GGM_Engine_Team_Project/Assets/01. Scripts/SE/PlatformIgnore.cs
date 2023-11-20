using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformIgnore : MonoBehaviour
{
    public Collider2D ladderGroundCollision;
    [SerializeField] private BoxCollider2D ladderCollider;

    private GameObject player;
    private Collider2D playerCol;
    private PlayerAnimation anim;
    private PlayerMovement movement;
    private Rigidbody2D playerBody;
    
    private Vector2 playerPos, nowPlayerPos;
    private bool first = true;
    [SerializeField] LayerMask playerMask;

    private void Awake()
    {
        ladderCollider = GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(transform.position, ladderCollider.bounds.size, 0, Vector2.zero, 0, playerMask);
        if (raycastHit2D)
        {
            if (raycastHit2D.collider.gameObject.CompareTag("Player"))
            {
                if (player == null) {
                    player = raycastHit2D.collider.gameObject;
                }
                if (anim == null || movement == null || playerBody == null || playerCol == null)
                {
                    anim = player.GetComponent<PlayerAnimation>();
                    movement = player.GetComponent<PlayerMovement>();
                    playerBody = player.GetComponent<Rigidbody2D>();
                    playerCol = playerBody.GetComponent<Collider2D>();
                }

                movement.Is_ladder = true;
                Physics2D.IgnoreCollision(playerCol, ladderGroundCollision, true);       // �����ض�

                nowPlayerPos = player.transform.position;
                if (first)
                {
                    playerPos = nowPlayerPos;
                    anim.Climb(false);
                    first = false;
                }
                if (playerPos == nowPlayerPos)      // ���ݲ��� ������
                {
                    anim.Climb(false);
                }
                else
                {
                    if (!movement.Is_onGround)      // �ٴڿ� ������� ������
                    {
                        anim.Climb(true);
                        Debug.Log("��ٸ� Ÿ�� �����ڳ�");
                    }
                    playerPos = nowPlayerPos;
                }
            }
        }
        else
        {
            if (player != null && anim != null)
            {
                Physics2D.IgnoreCollision(playerCol, ladderGroundCollision, false);
                anim.Climb(false);
                movement.Is_ladder = false;
            }
            first = true;
        }
    }

    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(transform.position, ladderCollider.bounds.size);
            Gizmos.color = Color.white;
        }
    }
}



// ��ٸ� ������ �����Ǵ°� ��ġ��!