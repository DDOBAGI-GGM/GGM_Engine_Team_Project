using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour, IPlayerAction
{
    private Animator animator;
    private PlayerMovement movement;

    // 헤쉬들 가져오기
    private readonly int walkHash = Animator.StringToHash("is_walking");
    private readonly int jumpHash = Animator.StringToHash("is_jump");
    private readonly int jumpingHash = Animator.StringToHash("is_jumping");
    private readonly int winHash = Animator.StringToHash("is_win");
    private readonly int attackHash = Animator.StringToHash("is_attack");
    private readonly int climbHash = Animator.StringToHash("is_climb");
    private readonly int avoidanceHash = Animator.StringToHash("is_avoidance");
    private readonly int interactionHash = Animator.StringToHash("is_interaction");

    private void Awake()
    {
        animator = GetComponent<Animator>();
        movement = GetComponent<PlayerMovement>();
    }

    public void Walk(bool value)
    {
        animator.SetBool(walkHash, value);
    }

    public void Jump()
    {
        // 플레이어 상태에 따라 다르게

        Debug.Log("셋 트리거 이거 오류가 조금 있음.");
        animator.SetTrigger(jumpHash);
        if (movement.Is_onJump)       // 바닥이 아니면
        {
            //Debug.Log("점프 중");
            animator.SetBool(jumpingHash, true);
        }
    }

    public void JumpingEnd()
    {
        //Debug.Log("점프중 애니 끝.");
        animator.SetBool(jumpingHash, false);
    }

    public void Attack()
    {
        Debug.Log("공격에 따른 애니");
        animator.SetTrigger(attackHash);
    }

    public void Avoidance()
    {
        Debug.Log("피하기에 따른 애니");
        animator.SetTrigger(avoidanceHash);
    }

    public void Climb()
    {
        Debug.Log("올라가기에 따른 애니");
    }

    public void Win()
    {
        Debug.Log("이김에 따른 애니");
        animator.SetTrigger(winHash);
    }

    public void Interaction()
    {
        Debug.Log("인터랙션에 따른 애니");
        animator.SetTrigger(interactionHash);
    }
}
