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
    void Jump();        // �����ϱ�
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

#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && movement.Is_onGround)        // �����̽��ٸ� ������ �ٴڿ� ���� ���� �����ϰ�
        {
            movement.Is_onJump = true;
            movement.Jump();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            animation.Attack();
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            animation.Avoidance();
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            animation.Win();
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            animation.Interaction();
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
            case PlayerActionEnum.Climb:
                Debug.Log("�ö󰡱Ⱑ �Էµ�.");
                movement.Climb();
                break;
            default:
                break;
        }
    }
}
