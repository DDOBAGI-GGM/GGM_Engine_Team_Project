using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using System.Threading;
using UnityEngine.Events;

public class InputFieldManager : SINGLETON<InputFieldManager>
{
    [SerializeField] private GameObject inputFieldPanel;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private TextMeshProUGUI backText; 

    private string text;
    private PlayerActionEnum type;
    private float timer;

    PlayerAction player;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<PlayerAction>();
    }

    public void Input(string _text, PlayerActionEnum _type, float _timer)
    {
        // �ð� ������ ����

        inputFieldPanel.SetActive(true);
        inputField.text = string.Empty;

        text = _text;
        type = _type;
        timer = _timer;

        backText.text = text;

        if (inputField.isFocused == false)
            inputField.OnPointerClick(new PointerEventData(EventSystem.current));

        Invoke("Check", timer);
    }

    public void Effect()
    {
        inputField.transform.DOScale(1.5f, 0.25f).OnComplete(() => { inputField.transform.DOScale(1f, 0.25f); });
    }

    public void Check()
    {
        // üũ�ϴ� �������� �ð� �ٽ� ����ȭ
        if (inputField.text == text)
        {
            Debug.Log("����!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            player.action(type);
        }
        else
        {
            Debug.Log("����!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");

        }

        inputField.text = string.Empty;
        inputFieldPanel.SetActive(false);
    }
}
