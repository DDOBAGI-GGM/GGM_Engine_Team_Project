using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    RandomCard _randomCard;
    PlayerHp _playerHp;

    public bool isShowCard = false;

    [SerializeField] ParticleSystem[] particles = new ParticleSystem[4];

    private void Awake()
    {
        _randomCard = GetComponent<RandomCard>();
        _playerHp = FindObjectOfType<PlayerHp>();
    }

    public void ClickCard1()
    {
        //Debug.Log("카드1 클릭");
       // Debug.Log(_randomCard.CardState1);
        StatsUpdate(_randomCard.CardState1);
        isShowCard = false;
    }

    public void ClickCard2()
    {
       // Debug.Log("카드2 클릭");    
       // Debug.Log(_randomCard.CardState2);
        StatsUpdate(_randomCard.CardState2);
        isShowCard = false;
    }

    public void ClickCard3()
    {
       // Debug.Log("카드3 클릭");
      //  Debug.Log(_randomCard.CardState3);
        StatsUpdate(_randomCard.CardState3);
        isShowCard = false;
    }

    private void StatsUpdate(State state)
    {
        switch (state)
        {
            case State.Resurrection:
                _playerHp.UpdateResurrection(true);
                particles[(int)State.Resurrection].Play();
                break;
            case State.IncreasedAttackPower:
                Debug.Log("공격력 증가");
                particles[(int)State.IncreasedAttackPower].Play();
                break;
            case State.HPIncreased:
                _playerHp.MaxHP++;
                particles[(int)State.HPIncreased].Play();
                break;
            case State.HPRegain:
                _playerHp.HpReSet();
                particles[(int)State.HPRegain].Play();

                break;
        }
    }
}
