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

    public bool jump_ok = true;
    public bool Jump_ok {  get { return jump_ok; } }

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
        if (jump_ok)
        {
            jump_ok = false;
            animator.SetTrigger(jumpHash);
        }
    }

    public void JumpStart()     // 점프가 시작되었음.
    {
        Debug.Log("공중으로 뛰자");
        animator.SetBool(jumpingHash, true);
    }

    public void JumpComplete()          // 플레이어 점프_엔드 애니 마지막에 이벤트로 등록됨.
    {
        Debug.Log("점프애니가 끝났음!");
        jump_ok = true;
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
