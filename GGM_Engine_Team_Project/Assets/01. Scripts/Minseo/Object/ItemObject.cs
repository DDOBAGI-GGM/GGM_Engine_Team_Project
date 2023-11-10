using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    CardManager _cardManager;
    RandomCard _randomCard;

    private void Awake()
    {
        _cardManager = FindObjectOfType<CardManager>();
        _randomCard = FindObjectOfType<RandomCard>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _randomCard.CoundUp();
            _cardManager.isShowCard = true;

            Destroy(gameObject);
        }
         
    }


}
