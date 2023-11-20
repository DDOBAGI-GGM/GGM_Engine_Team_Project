using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformIgnore : MonoBehaviour
{
    public Collider2D ladderGroundCollision;
    [SerializeField] private Collider2D ladderCollider;

    private GameObject player;
    private Collider2D playerCol;
    private PlayerAnimation anim;
    private PlayerMovement movement;
    private Rigidbody2D playerBody;
    
    private Vector2 playerPos, nowPlayerPos;
    private bool first = true;
    [SerializeField] LayerMask playerMask;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (player == null)
            {
                player = collision.gameObject;
            }
            if (anim == null || movement == null || playerBody == null || playerCol == null)
            {
                anim = player.GetComponent<PlayerAnimation>();
                movement = player.GetComponent<PlayerMovement>();
                playerBody = player.GetComponent<Rigidbody2D>();
                playerCol = playerBody.GetComponent<Collider2D>();
            }

            Physics2D.IgnoreCollision(playerCol, ladderGroundCollision, true);       // 무시해라

            nowPlayerPos = player.transform.position;
            playerPos = nowPlayerPos;
            anim.Climb(false);
            movement.Is_ladder = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (movement != null && movement.Is_ladder)
            {
                nowPlayerPos = collision.transform.position;
                if (playerPos == nowPlayerPos)      // 지금꺼랑 같으면
                {
                    anim.Climb(false);
                }
                else
                {
                    if (!movement.Is_onGround)      // 바닥에 닿아있지 않으면
                    {
                        anim.Climb(true);
                        Debug.Log("사다리 타는 중이자나");
                    }
                    playerPos = nowPlayerPos;
                }
            }
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (player != null && anim != null)
            {
                Physics2D.IgnoreCollision(playerCol, ladderGroundCollision, false);
                anim.Climb(false);
                movement.Is_ladder = false;
            }
        }
    }


    /*private void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(transform.position, ladderCollider.bounds.size);
            Gizmos.color = Color.white;
        }
    }*/
}



// 사다리 위에서 점프되는거 고치기!