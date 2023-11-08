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
    Resurrection = 0,       // ��Ȱ��
    HPRegain,               // ü�� ȸ��
    HPIncreased,            // ü�� ����
    IncreasedAttackPower    // ���ݷ� ����
}

public class RandomCard : MonoBehaviour
{
    [SerializeField] private GameObject panel;

    private bool iscard = true;

    CardManager _cardManager;

    //public bool isShowCard = false;

    #region ī�� �ؽ�Ʈ
    [Header("ī�� �ؽ�Ʈ")]
    [SerializeField] private TextMeshProUGUI _titleTMP1;
    [SerializeField] private TextMeshProUGUI _titleTMP2;
    [SerializeField] private TextMeshProUGUI _titleTMP3;
    [SerializeField] private TextMeshProUGUI _cardExplain1;
    [SerializeField] private TextMeshProUGUI _cardExplain2;
    [SerializeField] private TextMeshProUGUI _cardExplain3;
    #endregion

    #region ī�� enum
    private State _cardState1;
    private State _cardState2;
    private State _cardState3;
    public State CardState1 { get { return _cardState1; } set { _cardState1 = value; } }
    public State CardState2 { get { return _cardState2; } set { _cardState2 = value; } }
    public State CardState3 { get { return _cardState3; } set { _cardState3 = value; } }
    #endregion

    #region Ȱ�� ����
    [Header("Ȯ�� ����")]
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
                _cardTxt.text = "��Ȱ��";
                _cardExplain.text = "�̰� ������ ��Ȱ���� ���ܿ�!!";
                check = false;
            }
            else if(percent <= hPRegain)
            {
                _state = State.HPRegain;
                _cardTxt.text = "ü�� ȸ��";
                _cardExplain.text = "ü���� ȸ���ص帱�Կ�~";
                check = false;
            }
            else if (percent <= hPIncreased)
            {
                _state = State.HPIncreased;
                _cardTxt.text = "ü�� ����";
                _cardExplain.text = "ü���� ���������ٰ�!! �Ϸη�~";
                check = false;
            }
            else if(percent <= increasedAttackPower)
            {
                _state = State.IncreasedAttackPower;
                _cardTxt.text = "���ݷ� ����";
                _cardExplain.text = "���ݷ��� �����ص帱�Կ뤷! �ѽ�����";
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
