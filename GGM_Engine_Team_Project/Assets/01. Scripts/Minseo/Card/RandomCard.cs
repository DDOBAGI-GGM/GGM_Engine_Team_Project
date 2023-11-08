using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEditor.Timeline.Actions;
using System.Runtime.ExceptionServices;
using Unity.VisualScripting;

public enum State
{
    Resurrection = 0,       // 부활권
    HPRegain,               // 체력 회복
    HPIncreased,            // 체력 증가
    IncreasedAttackPower    // 공격력 증가
}

public class RandomCard : MonoBehaviour
{
    [SerializeField] private GameObject panel;

    private bool iscard = true;

    CardManager _cardManager;

    //public bool isShowCard = false;

    #region 카드 텍스트
    [Header("카드 텍스트")]
    [SerializeField] private TextMeshProUGUI _titleTMP1;
    [SerializeField] private TextMeshProUGUI _titleTMP2;
    [SerializeField] private TextMeshProUGUI _titleTMP3;
    [SerializeField] private TextMeshProUGUI _cardExplain1;
    [SerializeField] private TextMeshProUGUI _cardExplain2;
    [SerializeField] private TextMeshProUGUI _cardExplain3;
    #endregion

    #region 카드 enum
    private State _cardState1;
    private State _cardState2;
    private State _cardState3;
    public State CardState1 { get { return _cardState1; } set { _cardState1 = value; } }
    public State CardState2 { get { return _cardState2; } set { _cardState2 = value; } }
    public State CardState3 { get { return _cardState3; } set { _cardState3 = value; } }
    #endregion

    #region 활률 조절
    [Header("확률 조절")]
    [SerializeField] private int resurrection = 0;
    [SerializeField] private int hPRegain = 0;
    [SerializeField] private int hPIncreased = 0;
    [SerializeField] private int increasedAttackPower = 0;
    #endregion

    void Awake()
    {
        _cardManager = GetComponent<CardManager>();
    }

    private void Update()
    {
        Cheak();
    }

    public void Cheak()
    {
        if (Input.GetKeyDown(KeyCode.F))
            _cardManager.isShowCard = true;

        if (_cardManager.isShowCard)
        {
            panel.transform.DOMoveY(540, 1.5f);
            iscard = true;
        }
        else if (!_cardManager.isShowCard)
        {
            panel.transform.DOMoveY(-540, 1.5f);

            if(iscard)
                ShuffleCard();

        }
    }

    private void ShuffleCard()
    {
        SetCard(_titleTMP1, _cardState1, _cardExplain1, 1);
        SetCard(_titleTMP2, _cardState2, _cardExplain2, 2);
        SetCard(_titleTMP3, _cardState3, _cardExplain3, 3);
        iscard = false;
    }

    private void SetCard(TextMeshProUGUI _cardTxt, State _state, TextMeshProUGUI _cardExplain, int num)
    {
        bool check = true;

        while (check)
        {
            float percent = Random.Range(1f, 100f);

            if(percent <= resurrection)
            {
                _state = State.Resurrection;
                _cardTxt.text = "부활권";
                _cardExplain.text = "이걸 누르면 부활권이 생겨용!!";
                check = false;
            }
            else if(percent <= hPRegain)
            {
                _state = State.HPRegain;
                _cardTxt.text = "체력 회복";
                _cardExplain.text = "체력을 회복해드릴게용~";
                check = false;
            }
            else if (percent <= hPIncreased)
            {
                _state = State.HPIncreased;
                _cardTxt.text = "체력 증가";
                _cardExplain.text = "체력을 증가시켜줄게!! 뾰로롱~";
                check = false;
            }
            else if(percent <= increasedAttackPower)
            {
                _state = State.IncreasedAttackPower;
                _cardTxt.text = "공격력 증가";
                _cardExplain.text = "공격력을 증가해드릴게용ㅇ! 뚜쉬따쉬";
                check = false;
            }
        }

        switch (num)
        {
            case 1:
                _cardState1 = _state;
                break;
            case 2:
                _cardState2 = _state;
                break;
            case 3:
                _cardState3 = _state;
                break;
            default:
                break;
        }
    }
}
