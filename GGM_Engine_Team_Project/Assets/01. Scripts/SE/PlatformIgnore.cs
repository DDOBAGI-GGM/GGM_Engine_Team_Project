using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformIgnore : MonoBehaviour
{
    public Collider2D ladderGroundCollision;
    public GameObject player;
    [SerializeField] private BoxCollider2D ladderCollider;
    private PlayerAnimation anim;
    private PlayerMovement movement;
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
                if (anim == null && movement == null)
                {
                    anim = raycastHit2D.collider.gameObject.GetComponent<PlayerAnimation>();
                    movement = raycastHit2D.collider.gameObject.GetComponent<PlayerMovement>();
                }
                player = raycastHit2D.collider.gameObject;

                movement.Is_ladder = true;

                Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), ladderGroundCollision, true);       // �浹���� ���ƶ�
                nowPlayerPos = player.transform.position;
                if (first)
                {
                    playerPos = nowPlayerPos;
                    Debug.Log("ó����");
                    first = false;
                }
                Debug.Log($"{playerPos}, {nowPlayerPos}");
                if (playerPos == nowPlayerPos)      // ���ݲ��� ������
                {
                    anim.Climb(false);
                }
                else
                {
                    if (!movement.Is_onGround)
                    {
                        anim.Climb(true);
                        Debug.Log("�����̰� �̽�");
                    }
                    playerPos = nowPlayerPos;
                }
            }
        }
        else
        {
            Debug.Log("��ٸ� ��Ÿ�� ����!");
            if (player != null && anim! != null)
            {
                Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), ladderGroundCollision, false);
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