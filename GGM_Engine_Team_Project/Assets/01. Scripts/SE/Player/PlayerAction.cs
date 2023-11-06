using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerActionEnum
{
    Jump = 0,       // ����
    Attack = 1,     // ����
    Avoidance = 2,      // ���ϱ�
}

public interface IPlayerAction
{
    void Jump();        // �����ϱ�
    void Attack();      // �����ϱ�
    void Avoidance();       // ���ϱ�
}

public class PlayerAction : MonoBehaviour
{
    private PlayerMovement movement;

    private void Awake()
    {
        movement = GetComponent<PlayerMovement>();
    }

#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && movement.Is_onGround)        // �����̽��ٸ� ������ �ٴڿ� ���� ���� �����ϰ�
        {
            movement.Is_onJump = true;
            movement.Jump();
        }
    }
#endif

    public void action(PlayerActionEnum action)
    {
        switch (action)
        {
            case PlayerActionEnum.Jump:
                movement.Jump();
                break;
            case PlayerActionEnum.Attack:
                movement.Attack();
                break;
            case PlayerActionEnum.Avoidance:
                movement.Avoidance();
                break;
            default:
                break;
        }
    }
}
