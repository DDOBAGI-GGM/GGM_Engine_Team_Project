using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
}

public class SoundManager : SINGLETON<SoundManager>
{
    public Sound[] bgmSounds;           // BGM ���� ����
    public Sound[] effectSounds;        // SFX ���� ����

    public AudioSource bgmAudioSource;           // BGM�� ����� ����� �ҽ�
    public List<AudioSource> sfxAudioSource = new List<AudioSource>();     // SFX�� ����� ����� �ҽ�
    [SerializeField] private int sfxAudioSourceConut;

    Dictionary<string, AudioClip> bgmDic = new Dictionary<string, AudioClip>();
    Dictionary<string, AudioClip> sfxDic = new Dictionary<string, AudioClip>();

    [Header("BGMtest")]
    [SerializeField] private string sceneName;

    private void Awake()
    {
        bgmAudioSource = gameObject.AddComponent<AudioSource>();

        for (int i = 0; i < sfxAudioSourceConut; ++i)
            sfxAudioSource.Add(gameObject.AddComponent<AudioSource>());
    }

    private void Start()
    {
        foreach (var bgm in bgmSounds)
        {
            bgmDic.Add(bgm.name, bgm.clip);
        }

        foreach (var sfx in effectSounds)
        {
            sfxDic.Add(sfx.name, sfx.clip);
        }

        if (SceneManager.GetActiveScene().name == sceneName)
            PlayBGM("bgm");
    }

    public void PlaySFX(string name)
    {
        foreach (var sfxSource in sfxAudioSource)
        {
            if (sfxDic[name] != null && sfxSource.isPlaying == false)
            {
                sfxSource.clip = sfxDic[name];
                sfxSource.Play();
            }
        }
    }

    public void PlayBGM(string name) // BGM ����
    {
        if (bgmDic[name] != null)
        {
            bgmAudioSource.clip = bgmDic[name];
            bgmAudioSource.loop = true;
            bgmAudioSource.Play();
        }
    }

    public void StopBGM()
    {
        bgmAudioSource.Stop();
    }

    public void StopAllSFX() // ��� SFX�� ����
    {
        foreach (var sfxSource in sfxAudioSource)
        {
            sfxSource.Stop();
            sfxSource.clip = null;
            sfxSource.loop = false;
        }
    }

    public void StopSFX(string name) // Ư�� SFX�� ����
    {
        foreach (var sfxSource in sfxAudioSource)
        {
            if (sfxDic[name] != null && sfxSource.isPlaying == true)
            {
                if (sfxSource.clip.name == sfxDic[name].name)
                {
                    sfxSource.Stop();
                    sfxSource.clip = null;
                    sfxSource.loop = false;

                    return;
                }
            }
        }
    }
}
