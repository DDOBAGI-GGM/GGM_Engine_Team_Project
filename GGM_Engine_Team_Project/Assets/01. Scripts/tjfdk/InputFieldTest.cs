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
    [SerializeField] private GameObject inputFieldPanel;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private GameObject saveP;

    private string text;
    private TextType type;
    private float timer;

    public void Input(string _text, TextType _type, float _timer)
    {
        // �ð� ������ ����

        inputFieldPanel.SetActive(true);
        inputField.text = string.Empty;

        text = _text;
        type = _type;
        timer = _timer;

        if (inputField.isFocused == false)
            inputField.OnPointerClick(new PointerEventData(EventSystem.current));

        Invoke("Check", timer);
    }

    public void Effect()
    {
        //input.transform.DOKill();
        //input.transform.DOScale(1.5f, 0.25f).SetLoops(2, LoopType.Yoyo);

        inputField.transform.DOScale(1.5f, 0.25f).OnComplete(() => { inputField.transform.DOScale(1f, 0.25f); });
        //text = input.text.Substring(input.text.Length - 1);
        //char t;
        //Debug.Log(text);
        //tweener = DOTween.To(() => text, x => input.text.Substring(0) = x, );
        //tex.maxVisibleCharacters = 0;
        //DOTween.To(x => tex.maxVisibleCharacters = (int)x, 0f, tex.text.Length, 3f);
    }

    public void Check()
    {
        // üũ�ϴ� �������� �ð� �ٽ� ����ȭ
        if (inputField.text == text)
        {
            Debug.Log("����!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            // ������ �ڵ� �ֱ⿩~
        }
        else
            Debug.Log("����!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");

        inputField.text = string.Empty;

        inputFieldPanel.SetActive(false);
    }
}
