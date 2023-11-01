using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour, IPlayerAction
{
    private Animator animator;

    // �콬�� ��������
    private readonly int anythingHash = Animator.StringToHash("anything");

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Jump()
    {
        Debug.Log("������ ���� �ִ�");
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
