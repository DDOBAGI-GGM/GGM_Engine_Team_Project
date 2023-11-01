using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour, IPlayerAction
{
    private Animator animator;

    // 헤쉬들 가져오기
    private readonly int anythingHash = Animator.StringToHash("anything");

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Jump()
    {
        Debug.Log("점프에 따른 애니");
    }

    public void Attack()
    {
        Debug.Log("공격에 따른 애니");
    }

    public void Avoidance()
    {
        Debug.Log("피하기에 따른 애니");
    }
}
