using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class UIManager : SINGLETON<UIManager>
{
    [Header("Sound")]
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider bgm_slider;
    [SerializeField] private Slider sfx_slider;

    [Header("Fade")]
    [SerializeField] private Image fadePanel;
    [SerializeField] private float duration;
    [SerializeField] private Ease easingFunc;

    private void Start()
    {
        fadePanel.gameObject.SetActive(true);
        fadePanel.DOFade(0, duration).SetEase(easingFunc).OnComplete(() => fadePanel.gameObject.SetActive(false));
    }

    public void SetActivePanel(GameObject panel)
    {
        panel.SetActive(!panel.activeSelf);
    }

    public void Set_BGMVolume()
    {
        if (bgm_slider.value == -40)
            audioMixer.SetFloat("BGM", -80);
        else
            audioMixer.SetFloat("BGM", bgm_slider.value);
    }

    public void Set_SFXVolume()
    {
        if (sfx_slider.value == -40)
            audioMixer.SetFloat("SFX", -80);
        else
            audioMixer.SetFloat("SFX", sfx_slider.value);
    }

    public void ChangeScene(string sceneName)
    {
        SoundManager.Instance.PlaySFX("sceneChange");
        fadePanel.gameObject.SetActive(true);
        fadePanel.DOFade(1, duration).OnComplete(() => { SceneManager.LoadScene(sceneName); }).SetEase(easingFunc);
    }

    public void Click()
    {
        SoundManager.Instance.PlaySFX("click");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
