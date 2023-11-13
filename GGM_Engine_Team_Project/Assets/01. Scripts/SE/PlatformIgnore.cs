using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformIgnore : MonoBehaviour
{
    public Collider2D ladderGroundCollision;
    private PlayerAnimation anim;
    private PlayerMovement movement;
    private Vector2 playerPos, nowPlayerPos;
    private bool first = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (anim == null && movement == null)
            {
                anim = collision.gameObject.GetComponent<PlayerAnimation>();
                movement = collision.gameObject.GetComponent<PlayerMovement>();
            }
            playerPos = collision.transform.position;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Physics2D.IgnoreCollision(collision.GetComponent<Collider2D>(), ladderGroundCollision, true);
            nowPlayerPos = collision.transform.position;
            if (first)
            {
                playerPos = nowPlayerPos;
                first = false;
            }
            if (playerPos == nowPlayerPos)      // Áö±Ý²¨¶û °°À¸¸é
            {
                anim.Climb(false);
            }
            else
            {
                if (!movement.Is_onGround)
                {
                    anim.Climb(true);
                }
                playerPos = nowPlayerPos;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Physics2D.IgnoreCollision(collision.GetComponent<Collider2D>(), ladderGroundCollision, false);
            anim.Climb(false);
            first = true;
        }
    }
}
