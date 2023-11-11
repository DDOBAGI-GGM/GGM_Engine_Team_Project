using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] private Image fadePanel;
    [SerializeField] private float duration;

    public void ChangeScene(string sceneName)
    {
        fadePanel.DOFade(1, duration).OnComplete(() => { SceneManager.LoadScene(sceneName); }).SetEase(Ease.InExpo);
    }
}
