using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputCheck : MonoBehaviour
{
    [Header("KeyWord")]
    [SerializeField] private string word;
    [SerializeField] private TextType wordType;
    [SerializeField] private float timer;
    [SerializeField] private string funcName;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            InputFieldManager.Instance.Input(word, wordType, timer, funcName);
        }
    }
}
