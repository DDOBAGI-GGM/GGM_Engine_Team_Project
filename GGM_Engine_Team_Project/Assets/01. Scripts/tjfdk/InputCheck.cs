using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputCheck : MonoBehaviour
{
    [Header("KeyWord")]
    [SerializeField] private string word;
    [SerializeField] private TextType wordType;
    [SerializeField] private float timer;

    [Header("Check")]
    [SerializeField] private float distance;

    private void Update()
    {
        // ħ���ϸ� ^^ ���̷� �˻�����
        //if ()
        InputFieldTest.Instance.Input(word, wordType, timer);
    }
}
