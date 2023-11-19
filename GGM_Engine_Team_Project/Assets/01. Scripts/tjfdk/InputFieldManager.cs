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
    [SerializeField] private PlayerAction player;
    [SerializeField] private PlayerMovement playerMovement;

    private bool checking = false;
    private string text;
    private PlayerActionEnum playerType;
    private EnemyEnum enemyType;
    private float timer;

    private bool is_typing = false;
    public bool Is_typing { get { return is_typing; } set { is_typing = value; } }

    private void Awake()
    {
        inputFieldPanel.SetActive(false);
    }

    private void Update()
    {
        inputField.placeholder.enabled = true;
    }

    public void Input(string _text, PlayerActionEnum _playerType, EnemyEnum _enemyType, float _timer)
    {
        checking = false;
        inputField.text = string.Empty;
        inputField.characterLimit = _text.Length;
        inputFieldPanel.SetActive(true);

        text = _text;
        playerType = _playerType;
        enemyType = _enemyType;
        timer = GameManager.Instance.TimeNormalize(_timer);
        //timer = _timer;

        backText.text = text;       // 쳐야하는 거 표시

        if (inputField.isFocused == false)          // 나에게 집중해
            inputField.OnPointerClick(new PointerEventData(EventSystem.current));

        Debug.Log(timer);
        Invoke("Check", timer);     // 이 시간 뒤에 호출해줘라

        is_typing = true;
    }

    public void Effect()        // 타이핑 될 때마다 크기 키워주기
    {
        inputField.transform.DOScale(1.1f, GameManager.Instance.TimeNormalize(0.25f))
            .OnComplete(() => { inputField.transform.DOScale(1f, GameManager.Instance.TimeNormalize(0.25f)); });
    }

    public void Check()     // 엔터칠때, 시간이 지났을 때 사용됨.
    {
        // 체크하는 순간부터 시간 다시 정상화
        if (checking == false)
        {
            Debug.Log("체크");
            if (inputField.text == text)
            {
                Debug.Log("성공");
                player.action(playerType);
                EnemyManager.Instance.EnemyDamage(enemyType);
            }
            else
            {
                Debug.Log("실패");
                EnemyManager.Instance.EnemyAttack(enemyType); 
            }

            checking = true;
            CameraAction.Instance.Shake();
        }

        GameManager.Instance.TimeReset();
        is_typing = false;
        inputField.text = string.Empty;
        inputFieldPanel.SetActive(false);

        if (playerMovement != null)
        {
            playerMovement.Is_typing = false;
        }
    }
}