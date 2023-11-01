using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class PanelUI : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    //[SerializeField] private TextMeshProUGUI _cardTxt1;
    //[SerializeField] private TextMeshProUGUI _cardTxt2;
    //[SerializeField] private TextMeshProUGUI _cardTxt3;
    //[SerializeField] private TextMeshProUGUI _cardExplain1;
    //[SerializeField] private TextMeshProUGUI _cardExplain2;
    //[SerializeField] private TextMeshProUGUI _cardExplain3;

    private void Awake()
    {
        panel.transform.DOMoveY(540, 2f);
    }
}
