using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using Unity.VisualScripting;
using System.Runtime.InteropServices.WindowsRuntime;

public class EffectTest : SINGLETON<EffectTest>
{
    [Header("Hit")]
    [SerializeField] private float hitPower;
    [SerializeField] private Color hitColor;
    [SerializeField] private Renderer objRenderer;

    bool end = false;

    //public bool Hit(GameObject obj)
    //{
    //    end = false;
    //    Debug.Log("hit ¿Ã∆Â∆Æ");
    //    objRenderer = obj.GetComponent<Renderer>();

    //    objRenderer.material.DOColor(hitColor, 0.5f).OnComplete(() => { objRenderer.material.DOColor(Color.white, 0f); });
    //    obj.transform.DOMoveY(obj.transform.position.y + hitPower, 0.25f).SetEase(Ease.OutQuad).SetLoops(2, LoopType.Yoyo).OnComplete(() => { }) ;

    //    return true;
    //}

    public IEnumerator Hit(GameObject obj)
    {
        end = false;
        Debug.Log("hit ¿Ã∆Â∆Æ");
        objRenderer = obj.GetComponent<Renderer>();

        objRenderer.material.DOColor(hitColor, 0.5f).OnComplete(() => { objRenderer.material.DOColor(Color.white, 0f); });
        yield return obj.transform.DOMoveY(obj.transform.position.y + hitPower, 0.25f).SetEase(Ease.OutQuad).SetLoops(2, LoopType.Yoyo).WaitForCompletion();
    }
}   
