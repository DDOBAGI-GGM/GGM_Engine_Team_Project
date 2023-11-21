using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEditor.Timeline.Actions;
using System.Runtime.ExceptionServices;
using Unity.VisualScripting;
using UnityEngine.UI;
using UnityEditorInternal.Profiling.Memory.Experimental;

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
    [SerializeField] private GameObject HPPanel;
    [SerializeField] private GameObject ItemPanel;
    [SerializeField] private GameObject ResurrectionPanel;

    private bool iscard = true;

    CardManager _cardManager;
    [SerializeField] private PlayerMovement _playerMovement;

    #region 아이템 
    [Header("아이템")]
    [SerializeField] private TextMeshProUGUI _itemCoundTMP;
    #endregion

    #region 아이콘
    [Header("아이콘 스프라이트")]
    [SerializeField] private Sprite _resurrectionSprite;
    [SerializeField] private Sprite _hPRegainSprite;
    [SerializeField] private Sprite _hPIncreasedSprite;
    [SerializeField] private Sprite _increasedAttackPowerSprite;
    #endregion

    #region 이미지
    [Header("아이콘 이미지")]
    [SerializeField] private Image _lmage1;
    [SerializeField] private Image _lmage2;
    [SerializeField] private Image _lmage3;
    #endregion

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
        if (_cardManager.isShowCard)
        {
            panel.transform.DOMoveY(540, 1.5f);
            
            HPPanel.SetActive(false);
            ItemPanel.SetActive(false);
            ResurrectionPanel.SetActive(false);
            
            iscard = true;

            if (_playerMovement != null)
                _playerMovement.Is_typing = true;
        }
        else if (!_cardManager.isShowCard)
        {
            panel.transform.DOMoveY(-540, 1.5f);

            HPPanel.SetActive(true);
            ItemPanel.SetActive(true);
            ResurrectionPanel?.SetActive(true);

            if (_playerMovement != null)
                _playerMovement.Is_typing = false;

            if (iscard)
                ShuffleCard();

        }
    }

    public void CoundUp()
    {
        GameManager.Instance.SetItem();
        _itemCoundTMP.text = $"X {GameManager.Instance.GetItem()}";
    }

    private void ShuffleCard()
    {
        SetCard(_titleTMP1, _cardState1, _cardExplain1, 1, _lmage1);
        SetCard(_titleTMP2, _cardState2, _cardExplain2, 2, _lmage2);
        SetCard(_titleTMP3, _cardState3, _cardExplain3, 3, _lmage3);
        iscard = false;
    }

    private void SetCard(TextMeshProUGUI _cardTxt, State _state, TextMeshProUGUI _cardExplain, int num, Image _image) // 이미지 추가하기
    {
        bool check = true;

        while (check)
        {
            float percent = Random.Range(1f, 100f);

            if(percent <= resurrection)
            {
                _state = State.Resurrection;
                _cardTxt.text = "부활";
                _image.sprite = _resurrectionSprite;
                _cardExplain.text = "빵구가 죽으면 다시 \n 부활할 수 있는 횟수가 \n +1 증가한다.";
                check = false;
            }
            else if(percent <= hPRegain)
            {
                _state = State.HPRegain;
                _cardTxt.text = "체력 회복";
                _image.sprite = _hPRegainSprite;
                _cardExplain.text = "빵구의 현재 체력을 \n 최대로 회복한다.";
                check = false;
            }
            else if (percent <= hPIncreased)
            {
                _state = State.HPIncreased;
                _cardTxt.text = "체력 증가";
                _image.sprite = _hPIncreasedSprite;
                _cardExplain.text = "빵구의 최대 체력이 \n +1 증가한다.";
                check = false;
            }
            else if(percent <= increasedAttackPower)
            {
                _state = State.IncreasedAttackPower;
                _cardTxt.text = "공격력 증가";
                _image.sprite = _increasedAttackPowerSprite;
                _cardExplain.text = "빵구의 공격력이 \n +1 증가한다.";
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
