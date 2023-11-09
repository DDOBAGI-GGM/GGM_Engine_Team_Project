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
        // 시간 느리게 설정

        inputFieldPanel.SetActive(true);
        inputField.text = string.Empty;

        text = _text;
        type = _type;
        timer = _timer;

        backText.text = text;       // 쳐야하는 거 표시

        if (inputField.isFocused == false)          // 나에게 집중해
            inputField.OnPointerClick(new PointerEventData(EventSystem.current));

        Invoke("Check", timer);     // 이 시간 뒤에 호출해줘라
    }

    public void Effect()        // 타이핑 될 때마다 크기 키워주기
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

    public void Check()     // 엔터칠때, 시간이 지났을 때 사용됨.
    {
        // 체크하는 순간부터 시간 다시 정상화
        if (inputField.text == text)
        {
            Debug.Log("성공");
            // 성은이 코드 넣기여~
        }
        else
            Debug.Log("실패");

        inputField.text = string.Empty;
        if (playerMovement != null)
        {
            playerMovement.Is_typing = false;
        }
        inputFieldPanel.SetActive(false);
    }
}
