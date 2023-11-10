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
        Debug.Log("ī��1 Ŭ��");
        Debug.Log(_randomCard.CardState1);
        isShowCard = false;
    }

    public void ClickCard2()
    {
        Debug.Log("ī��2 Ŭ��");    
        Debug.Log(_randomCard.CardState2);
        isShowCard = false;
    }

    public void ClickCard3()
    {
        Debug.Log("ī��3 Ŭ��");
        Debug.Log(_randomCard.CardState3); 
        isShowCard = false;
    }
}
