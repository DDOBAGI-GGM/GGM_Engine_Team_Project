using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using Unity.VisualScripting;
using System.Runtime.InteropServices.WindowsRuntime;
using System;

public class EffectTest : SINGLETON<EffectTest>
{
    [Header("Hit")]
    [SerializeField] private float hitPower;
    [SerializeField] private Color hitColor;
    [SerializeField] private Renderer objRenderer;

    public void Hit(GameObject obj, Action<bool> onComplete)
    {
        objRenderer = obj.GetComponent<Renderer>();

        objRenderer.material.DOColor(hitColor, 0.5f).OnComplete(() => { objRenderer.material.DOColor(Color.white, 0f); });
        obj.transform.DOMoveY(obj.transform.position.y + hitPower, 0.25f).SetEase(Ease.OutQuad)
            .SetLoops(2, LoopType.Yoyo).OnComplete(() => { onComplete(true); });
    }
}   
