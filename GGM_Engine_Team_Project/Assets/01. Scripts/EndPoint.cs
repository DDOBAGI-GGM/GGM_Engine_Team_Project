using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPoint : MonoBehaviour
{
    private const string sceneName = "GameClearScene";
    private Animator playerAnimator;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            SoundManager.Instance.PlaySFX("clear");

            //if (playerAnimator == null)
            playerAnimator = collision.GetComponent<Animator>();
            playerAnimator.SetTrigger("is_win");
            //UIManager.Instance.ChangeScene(sceneName);
        }
    }
}
