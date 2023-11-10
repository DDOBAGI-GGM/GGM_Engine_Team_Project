using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAction : SINGLETON<CameraAction>
{
    CinemachineVirtualCamera mainCam;
    CinemachineBasicMultiChannelPerlin cam;

    [Header("Shake")]
    [SerializeField] private float shakeFouce;
    [SerializeField] private float shakeSpeed;
    [SerializeField] private float shakeTimer;

    private void Awake()
    {
        mainCam = GetComponent<CinemachineVirtualCamera>();
        cam = mainCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        cam.m_AmplitudeGain = 0f;
        cam.m_FrequencyGain = 0f;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
            Shake();
    }

    public void Shake()
    {
        StartCoroutine(ShakeTime());
    }

    IEnumerator ShakeTime()
    {
        cam.m_AmplitudeGain = shakeFouce;
        cam.m_FrequencyGain = shakeSpeed;
        yield return new WaitForSeconds(shakeTimer);
        cam.m_AmplitudeGain = 0f;
        cam.m_FrequencyGain = 0f;
    }
}
