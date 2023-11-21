using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class Enemy : MonoBehaviour
{
    public bool isHiting = false;

    virtual protected void Awake() { }
    virtual protected void Start() { }
    virtual protected void Update() { }
    virtual public void Attack() { }
    virtual public void GetDamage(int damage = 1)
    {
        isHiting = true;
        EffectTest.Instance.Hit(gameObject, result =>
        {
            isHiting = false;
        });
    }
}
