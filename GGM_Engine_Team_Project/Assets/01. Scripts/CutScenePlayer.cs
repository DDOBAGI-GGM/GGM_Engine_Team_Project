using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class CutScenePlayer : MonoBehaviour
{
    [SerializeField] Sprite[] cutSceneImages;
    [SerializeField] SpriteRenderer cutSceneViewer;
    [SerializeField] private Image fadePanel;
    [SerializeField] private float duration;

    private int imageIdx;
    private const string sceneName = "Tilemap_stage";

    private void Start()
    {
        imageIdx = 0;
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if (imageIdx < cutSceneImages.Length)
            {
                cutSceneViewer.sprite = cutSceneImages[imageIdx];
            }
            else
            {
                ChangeScene();
            }
            imageIdx++;
        }
    }

    public void ChangeScene()
    {
        fadePanel.DOFade(1, duration).OnComplete(() => { SceneManager.LoadScene(sceneName); }).SetEase(Ease.InExpo);
    }
}
