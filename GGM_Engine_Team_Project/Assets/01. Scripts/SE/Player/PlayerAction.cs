using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerActionEnum
{
    Jump = 0,       // 점프
    Attack = 1,     // 공격
    Avoidance = 2,      // 피하기
    Climb = 3,
}

public interface IPlayerAction
{
    void Jump(bool a);        // 점프하기
    void Attack();      // 공격하기
    void Avoidance();       // 피하기
    void Climb();   // 공격
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
                Debug.Log("올라가기가 입력됨.");
                movement.Climb();
                break;
            default:
                break;
        }
    }
}
