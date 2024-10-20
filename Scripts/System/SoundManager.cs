using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using static Enums;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField]
    private AudioMixer audioMixer;

    private string poolName;
    private AudioSource bgmSource;
    private Coroutine fadeInCoroutine;

    private readonly float FADE_DURATION = 0.5f;
    private readonly float MIN_DISTANCE_FOR_3D = 1f;
    private readonly float MAX_DISTANCE_FOR_3D = 10f;

    private IEnumerator FadeIn(Action callback)
    {
        bgmSource.volume = 0f;
        bgmSource.Play();

        while (bgmSource.volume < 1f)
        {
            bgmSource.volume += Time.deltaTime / FADE_DURATION;
            yield return null;
        }

        bgmSource.volume = 1f;
        bgmSource.loop = true;
        callback?.Invoke();
    }

    private IEnumerator FadeOut(Action callback)
    {
        while (bgmSource.volume > 0f)
        {
            bgmSource.volume -= Time.fixedUnscaledDeltaTime / FADE_DURATION;
            yield return null;
        }

        bgmSource.volume = 0f;
        bgmSource.Stop();
        bgmSource.clip = null;

        PoolingManager.Instance.Push(poolName, bgmSource.gameObject);
        callback?.Invoke();
    }

    private AudioSource GetAudioSource(Enums.SoundType type, string clipName, bool is3D = false, float minDistance = 0)
    {
        AudioSource audiosSource = PoolingManager.Instance.Pop<AudioSource>(poolName, true);
        audiosSource.playOnAwake = false;
        audiosSource.clip = AddressableManager.Instance.GetAudioClip(clipName.ToString());
        audiosSource.spatialBlend = is3D ? 1 : 0;
        audiosSource.outputAudioMixerGroup = audioMixer.FindMatchingGroups(Constants.Game.STRING_MASTER)[(int)type];
        audiosSource.volume = 1f;
        audiosSource.loop = false;
        
        if (is3D)
        {
            audiosSource.rolloffMode = AudioRolloffMode.Linear;
            audiosSource.minDistance = minDistance;
            audiosSource.maxDistance = MAX_DISTANCE_FOR_3D;
        }

        return audiosSource;
    }

    public void Init()
    {
        audioMixer = ResourceManager.Instance.Load<AudioMixer>(typeof(AudioMixer).Name);
        audioMixer.SetFloat(SoundType.Master.ToString(), PlayerPrefs.GetFloat(SoundType.Master.ToString(), 1f));
        audioMixer.SetFloat(SoundType.Bgm.ToString(), PlayerPrefs.GetFloat(SoundType.Bgm.ToString(), 1f));
        audioMixer.SetFloat(SoundType.Sfx.ToString(), PlayerPrefs.GetFloat(SoundType.Sfx.ToString(), 1f));

        poolName = typeof(AudioSource).Name;
        GameObject audio = new GameObject(poolName).AddComponent<AudioSource>().AddComponent<ReturnAudioSource>().gameObject;
        PoolingManager.Instance.CreatePool(poolName, audio, 1);
        PoolingManager.Instance.Push(poolName, audio);

        
    }

    public void SetSound(Enums.SoundType soundType, float value)
    {
        audioMixer.SetFloat(soundType.ToString(), Mathf.Log10(value) * 20);   
    }

    public float GetSound(Enums.SoundType soundType)
    {
        audioMixer.GetFloat(soundType.ToString(), out float result);
        return result;
    }

    public void PlayBGMWithFadeIn(Enums.BGM name, Action callback = null)
    {
        if (fadeInCoroutine != null)
        {
            StopCoroutine(fadeInCoroutine);
            fadeInCoroutine = null;
        }

        bgmSource = GetAudioSource(Enums.SoundType.Bgm, name.ToString());
        fadeInCoroutine = StartCoroutine(FadeIn(callback));
    }

    public void StopBGMWithFadeOut(Action callback = null)
    {
       StartCoroutine(FadeOut(callback));
    }

    public void PlaySFX(Enums.SFX name)
    {
        AudioSource sfx = GetAudioSource(Enums.SoundType.Sfx ,name.ToString());
        
        sfx.Play();
    }

    public void PlaySFX(Enums.SFX name, float volume)
    {
        AudioSource sfx = GetAudioSource(Enums.SoundType.Sfx, name.ToString());
        sfx.volume = volume;

        sfx.Play();
    }

    public void PlaySFX(Enums.SFX name, bool isLoop)
    {
        AudioSource sfx = GetAudioSource(Enums.SoundType.Sfx, name.ToString());
        sfx.loop = isLoop;

        sfx.Play();
    }

    public void PlaySFX3D(Enums.SFX name, Vector3 position)
    {
        AudioSource sfx = GetAudioSource(Enums.SoundType.Sfx, name.ToString(), true, MIN_DISTANCE_FOR_3D);
        sfx.transform.position = position;

        sfx.Play();
    }

    public void PlaySFX3D(Enums.SFX name, float volume, Vector3 position)
    {
        AudioSource sfx = GetAudioSource(Enums.SoundType.Sfx, name.ToString(), true, MIN_DISTANCE_FOR_3D);
        sfx.transform.position = position;
        sfx.volume = volume;

        sfx.Play();
    }

    public void PlaySFX3D(Enums.SFX name, Vector3 position, bool isLoop)
    {
        AudioSource sfx = GetAudioSource(Enums.SoundType.Sfx, name.ToString(), true, MIN_DISTANCE_FOR_3D);
        sfx.transform.position = position;
        sfx.loop = isLoop;

        sfx.Play();
    }

    public void PlaySFX3D(Enums.SFX name, Vector3 position, bool isLoop, float minDistance, float maxDistance)
    {
        AudioSource sfx = GetAudioSource(Enums.SoundType.Sfx, name.ToString(), true, MIN_DISTANCE_FOR_3D);
        sfx.transform.position = position;
        sfx.loop = isLoop;
        sfx.minDistance = minDistance;
        sfx.maxDistance = maxDistance; 

        sfx.Play();

    }
}
