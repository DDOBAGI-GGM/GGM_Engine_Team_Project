using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using System.Threading;

public class InputFieldTest : SINGLETON<InputFieldTest>
{
    [SerializeField] private TMP_InputField input;
    [SerializeField] private GameObject saveP;

    private string text;
    private TextType type;
    private float timer;

    public void Input(string _text, TextType _type, float _timer)
    {   
        // 시간 느리게 설정

        text = _text;
        type = _type;
        timer = _timer;

        input.gameObject.SetActive(true);
        saveP.gameObject.SetActive(true);

        if (input.isFocused == false)
            input.OnPointerClick(new PointerEventData(EventSystem.current));

        Invoke("Check", timer);
    }

    public void Effect()
    {
        //input.transform.DOKill();
        //input.transform.DOScale(1.5f, 0.25f).SetLoops(2, LoopType.Yoyo);
        input.transform.DOScale(1.5f, 0.25f).OnComplete(() => { input.transform.DOScale(1f, 0.25f); }) ;
    }

    public void Check()
    {
        // 체크하는 순간부터 시간 다시 정상화
        if (input.text == text)
        {
            Debug.Log("성공!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            // 성은이 코드 넣기여~
        }
        else
            Debug.Log("병신!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");

        input.text = string.Empty;
    }
}
