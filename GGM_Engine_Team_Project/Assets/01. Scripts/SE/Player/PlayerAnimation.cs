using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour, IPlayerAction
{
    private Animator animator;
    private PlayerMovement movement;

    // �콬�� ��������
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
        // �÷��̾� ���¿� ���� �ٸ���
        if (jump_ok)
        {
            jump_ok = false;
            animator.SetTrigger(jumpHash);
        }
    }

    public void JumpStart()     // ������ ���۵Ǿ���.
    {
        Debug.Log("�������� ����");
        animator.SetBool(jumpingHash, true);
    }

    public void JumpComplete()          // �÷��̾� ����_���� �ִ� �������� �̺�Ʈ�� ��ϵ�.
    {
        Debug.Log("�����ִϰ� ������!");
        jump_ok = true;
    }

    public void JumpingEnd()
    {
        //Debug.Log("������ �ִ� ��.");
        animator.SetBool(jumpingHash, false);
    }

    public void Attack()
    {
        Debug.Log("���ݿ� ���� �ִ�");
        animator.SetTrigger(attackHash);
    }

    public void Avoidance()
    {
        Debug.Log("���ϱ⿡ ���� �ִ�");
        animator.SetTrigger(avoidanceHash);
    }

    public void Climb()
    {
        Debug.Log("�ö󰡱⿡ ���� �ִ�");
    }

    public void Win()
    {
        Debug.Log("�̱迡 ���� �ִ�");
        animator.SetTrigger(winHash);
    }

    public void Interaction()
    {
        Debug.Log("���ͷ��ǿ� ���� �ִ�");
        animator.SetTrigger(interactionHash);
    }
}
