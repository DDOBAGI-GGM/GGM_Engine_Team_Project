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
    [SerializeField] private EnemyManager enemy;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private CircleCollider2D collider;

    private bool checking = false;
    private string text;
    private PlayerActionEnum playerType;
    private EnemyEnum enemyType;
    private float timer;

    private void Awake()
    {
        // collider = GetComponent<CircleCollider2D>();
        inputFieldPanel.SetActive(false);
    }

    public void Input(string _text, PlayerActionEnum _type, float _timer)
    {
        // 시간 느리게 설정

        inputFieldPanel.SetActive(true);
        inputField.text = string.Empty;
        checking = false;

        text = _text;
        playerType = _type;
        timer = _timer;

        backText.text = text;       // 쳐야하는 거 표시

        if (inputField.isFocused == false)          // 나에게 집중해
            inputField.OnPointerClick(new PointerEventData(EventSystem.current));

        Invoke("Check", timer);     // 이 시간 뒤에 호출해줘라
    }

    public void Effect()        // 타이핑 될 때마다 크기 키워주기
    {
        inputField.transform.DOScale(1.5f, 0.25f).OnComplete(() => { inputField.transform.DOScale(1f, 0.25f); });
    }

    public void Check()     // 엔터칠때, 시간이 지났을 때 사용됨.
    {
        // 체크하는 순간부터 시간 다시 정상화
        if (checking == false)
        {
            if (inputField.text == text)
            {
                Debug.Log("성공");
                player.action(playerType);
            }
            else
            {
                Debug.Log("실패");
                enemy.Enemy(enemyType);
            }

            checking = true;
        }

        inputField.text = string.Empty;
        inputFieldPanel.SetActive(false);

        if (playerMovement != null)
        {
            playerMovement.Is_typing = false;
        }
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(transform.position, collider.radius);
    //    Gizmos.color = Color.white;
    //}
}
