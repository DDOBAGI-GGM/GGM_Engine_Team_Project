using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    RandomCard _randomCard;

    public bool isShowCard = false;


    private void Awake()
    {
        _randomCard = GetComponent<RandomCard>();
    }

    public void ClickCard1()
    {
        Debug.Log("카드1 클릭");
        Debug.Log(_randomCard.CardState1);
        isShowCard = false;
    }

    public void ClickCard2()
    {
        Debug.Log("카드2 클릭");    
        Debug.Log(_randomCard.CardState2);
        isShowCard = false;
    }

    public void ClickCard3()
    {
        Debug.Log("카드3 클릭");
        Debug.Log(_randomCard.CardState3); 
        isShowCard = false;
    }
}
