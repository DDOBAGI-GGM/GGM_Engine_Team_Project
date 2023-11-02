using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public enum State
{
    Resurrection = 0,       // 부활권
    HPRegain,               // 체력 회복
    HPIncreased,            // 체력 증가
    IncreasedAttackPower    // 공격력 증가
}

public class RandomCard : MonoBehaviour
{
    #region 카드 텍스트
    [SerializeField] private TextMeshProUGUI _titleTMP1;
    [SerializeField] private TextMeshProUGUI _titleTMP2;
    [SerializeField] private TextMeshProUGUI _titleTMP3;
    [SerializeField] private TextMeshProUGUI _cardExplain1;
    [SerializeField] private TextMeshProUGUI _cardExplain2;
    [SerializeField] private TextMeshProUGUI _cardExplain3;
    [SerializeField] private GameObject panel;
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
    int Resurrectionper = 0, HPRegainPer = 0, HPIncreasedPer = 0, IncreasedAttackPower = 0;
    #endregion

    void Awake()
    {
        panel.transform.DOMoveY(540, 2f);
        SetCard(_titleTMP1, _cardState1, _cardExplain1, 1);
        SetCard(_titleTMP2, _cardState2, _cardExplain2, 2);
        SetCard(_titleTMP3, _cardState3, _cardExplain3, 3);
    }

    private void SetCard(TextMeshProUGUI _cardTxt, State _state, TextMeshProUGUI _cardExplain, int num)
    {
        bool check = true;

        while (check)
        {
            float percent = Random.Range(1f, 100f);

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
