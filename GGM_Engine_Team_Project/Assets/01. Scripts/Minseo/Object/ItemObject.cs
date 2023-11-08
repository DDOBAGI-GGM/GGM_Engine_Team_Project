using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    CardManager _cardManager;

    private void Awake()
    {
        _cardManager = GetComponent<CardManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            _cardManager.isShowCard = true;
        Debug.Log("dddddd");
    }
}
