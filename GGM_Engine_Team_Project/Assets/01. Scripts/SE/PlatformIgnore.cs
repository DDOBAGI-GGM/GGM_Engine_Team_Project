using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformIgnore : MonoBehaviour
{
    public Collider2D ladderGroundCollision;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("»ç´Ù¸®¿Í ºÎµúÇô");
            Physics2D.IgnoreCollision(collision.GetComponent<Collider2D>(), ladderGroundCollision, true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("³ª°¬¿À");
            Physics2D.IgnoreCollision(collision.GetComponent<Collider2D>(), ladderGroundCollision, false);
        }
    }
}
