using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPoint : MonoBehaviour
{

    private const string sceneName = "GameClearScene";
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            SoundManager.Instance.PlaySFX("clear");
            UIManager.Instance.ChangeScene(sceneName);
        }
    }
}
