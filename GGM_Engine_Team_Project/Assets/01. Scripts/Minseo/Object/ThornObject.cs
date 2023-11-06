using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThornObject : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // 플레이어 hp --하는 코드
            Debug.Log("플레이어 부닥침ㅋㅋ");
        }
    }
}
