using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using System.Threading;

public class InputFieldManager : SINGLETON<InputFieldManager>
{
    [SerializeField] private GameObject inputFieldPanel;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private TextMeshProUGUI backText;
    [SerializeField] private PlayerMovement playerMovement;

    private string text;
    private PlayerActionEnum type;
    private float timer;

    public void Input(string _text, PlayerActionEnum _type, float _timer)
    {
        // �ð� ������ ����

        inputFieldPanel.SetActive(true);
        inputField.text = string.Empty;

        text = _text;
        type = _type;
        timer = _timer;

        backText.text = text;       // �ľ��ϴ� �� ǥ��

        if (inputField.isFocused == false)          // ������ ������
            inputField.OnPointerClick(new PointerEventData(EventSystem.current));

        Invoke("Check", timer);     // �� �ð� �ڿ� ȣ�������
    }

    public void Effect()        // Ÿ���� �� ������ ũ�� Ű���ֱ�
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

    public void Check()     // ����ĥ��, �ð��� ������ �� ����.
    {
        // üũ�ϴ� �������� �ð� �ٽ� ����ȭ
        if (inputField.text == text)
        {
            Debug.Log("����");
            // ������ �ڵ� �ֱ⿩~
        }
        else
            Debug.Log("����");

        inputField.text = string.Empty;
        if (playerMovement != null)
        {
            playerMovement.Is_typing = false;
        }
        inputFieldPanel.SetActive(false);
    }
}
