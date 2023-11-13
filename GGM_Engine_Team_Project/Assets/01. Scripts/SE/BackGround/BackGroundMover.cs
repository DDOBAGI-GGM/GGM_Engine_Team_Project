using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundMover : MonoBehaviour
{
    [SerializeField] private Transform player;

    private void OnTriggerExit2D(Collider2D collision)
    {
        //if (!collision.CompareTag("Area"))        // ���࿡ ������ �ݶ��̴��� Area �� �ƴϸ� ���������
        //    return;

        Vector3 playerPos = player.transform.position;     // �÷��̾� ������
        Vector3 myPos = transform.position;     // ����

        float diffx = Mathf.Abs(playerPos.x - myPos.x);
        float diffy = Mathf.Abs(playerPos.y - myPos.y);

        Vector3 playerDir = transform.position;     // ����?
        float dirx = playerDir.x < 0 ? -1 : 1;
        float diry = playerDir.y < 0 ? -1 : 1;

        if (diffx > diffy)
        {
            transform.Translate(Vector2.right * dirx * 40);
        }
        else if (diffx < diffy)
        {
            transform.Translate(Vector2.up * diry * 40);
        }
    }
}
