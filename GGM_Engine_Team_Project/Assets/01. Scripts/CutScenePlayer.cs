using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class CutScenePlayer : MonoBehaviour
{
    [SerializeField] Sprite[] cutSceneImages;
    [SerializeField] Image cutSceneViewer;

    private int imageIdx;
    private const string sceneName = "TitleScene";

    private void Start()
    {
        imageIdx = 0;
    }

    public void PlayCutScene()
    {
        if (imageIdx < cutSceneImages.Length)
        {
            cutSceneViewer.sprite = cutSceneImages[imageIdx];
        }
        else
        {
            UIManager.Instance.ChangeScene(sceneName);
        }
        imageIdx++;
    }

    public void Skip()
    {
        UIManager.Instance.ChangeScene(sceneName);
    }
}
