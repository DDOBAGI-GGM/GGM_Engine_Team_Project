using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class Enemy : MonoBehaviour
{
    public bool isHiting = false;

    virtual protected void Awake() { }
    virtual protected void Start() { }
    virtual protected void Update() { }
    virtual public void Attack()
    {
        SoundManager.Instance.PlaySFX("attack");
    }
    virtual public void GetDamage(int damage = 1)
    {
        SoundManager.Instance.PlaySFX("hit");
        isHiting = true;
        Debug.Log(isHiting);
        EffectTest.Instance.Hit(gameObject, result =>
        {
            isHiting = false;
            Debug.Log(isHiting);
        });
    }
}
