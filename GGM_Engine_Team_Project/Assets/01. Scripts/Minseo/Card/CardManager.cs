using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    RandomCard _randomCard;

    private void Awake()
    {
        _randomCard = new RandomCard();
    }

    public void ClickCard1()
    {
        Debug.Log("카드1 클릭");
        Debug.Log(_randomCard.CardState1);
    }

    public void ClickCard2()
    {
        Debug.Log("카드2 클릭");    
        Debug.Log(_randomCard.CardState2);
    }

    public void ClickCard3()
    {
        Debug.Log("카드3 클릭");
        Debug.Log(_randomCard.CardState3);
    }
}
