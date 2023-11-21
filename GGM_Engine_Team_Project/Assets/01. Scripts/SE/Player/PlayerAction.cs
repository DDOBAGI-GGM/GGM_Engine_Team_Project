using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerActionEnum
{
    Jump = 0,       // ����
    Attack = 1,     // ����
    Avoidance = 2,      // ���ϱ�
    Climb = 3,
}

public interface IPlayerAction
{
    void Jump(bool a);        // �����ϱ�
    void Attack();      // �����ϱ�
    void Avoidance();       // ���ϱ�
    void Climb();   // ����
}

public class PlayerAction : MonoBehaviour
{
    private PlayerMovement movement;
    private PlayerAnimation animation;

    private void Awake()
    {
        movement = GetComponent<PlayerMovement>();
        animation = GetComponent<PlayerAnimation>();
    }

    public void action(PlayerActionEnum action)
    {
        switch (action)
        {
            case PlayerActionEnum.Jump:
                Debug.Log("dd");
                movement.Jump(true);
                break;
            case PlayerActionEnum.Attack:
                movement.Attack();
                break;
            case PlayerActionEnum.Avoidance:
                movement.Avoidance();
                break;
            case PlayerActionEnum.Climb:
                Debug.Log("�ö󰡱Ⱑ �Էµ�.");
                movement.Climb();
                break;
            default:
                break;
        }
    }
}
