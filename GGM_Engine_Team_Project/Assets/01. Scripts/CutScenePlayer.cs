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
    [SerializeField] SceneChanger sceneChanger;

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
                Debug.Log(sceneChanger);
                sceneChanger.ChangeScene(sceneName);
            }
            imageIdx++;
        }
    }

}
