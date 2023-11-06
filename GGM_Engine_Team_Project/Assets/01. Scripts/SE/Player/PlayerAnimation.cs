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

        Debug.Log("�� Ʈ���� �̰� ������ ���� ����.");
        animator.SetTrigger(jumpHash);
        if (movement.Is_onJump)       // �ٴ��� �ƴϸ�
        {
            //Debug.Log("���� ��");
            animator.SetBool(jumpingHash, true);
        }
    }

    public void JumpingEnd()
    {
        //Debug.Log("������ �ִ� ��.");
        animator.SetBool(jumpingHash, false);
    }

    public void Attack()
    {
        Debug.Log("���ݿ� ���� �ִ�");
    }

    public void Avoidance()
    {
        Debug.Log("���ϱ⿡ ���� �ִ�");
    }
}
