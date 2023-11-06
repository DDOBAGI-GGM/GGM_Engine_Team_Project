using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP : MonoBehaviour
{
    [SerializeField] private Transform hp;

    public void HpSet(float scale)     // 0.0f ~ 1f
    {
        hp.localScale = new Vector3(scale, 1, 1);
    }

    public void HpDown(float down)
    {
        float downHP = Mathf.Clamp(hp.localScale.x - down, 0, 1);
        hp.localScale = new Vector3(downHP, 1, 1);
    }
}
