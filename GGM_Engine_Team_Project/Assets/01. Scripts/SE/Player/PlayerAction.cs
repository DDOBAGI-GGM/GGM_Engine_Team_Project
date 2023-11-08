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
    void Jump();        // 점프하기
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

#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && movement.Is_onGround)        // 스페이스바를 누르고 바닥에 있을 때만 점프하게
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
                Debug.Log("올라가기가 입력됨.");
                movement.Climb();
                break;
            default:
                break;
        }
    }
}
