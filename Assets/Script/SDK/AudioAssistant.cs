using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;
using UniRx;

using Sirenix.OdinInspector;
using Random = UnityEngine.Random;

[RequireComponent(typeof(AudioListener))]
[RequireComponent(typeof(AudioSource))]
public class AudioAssistant : SerializedMonoBehaviour
{
    public static AudioAssistant Instance { get; private set; }

    [SerializeField]
    AudioSource music;

    [SerializeField]
    AudioSource sfx;

    [SerializeField]
    private AudioSource pitchedAudioSource;

    [SerializeField]
    private Vector2 pitchLevelMinMax;


    private SoundConfig soundCfg;
    static List<TYPE_SOUND> mixBuffer = new List<TYPE_SOUND>(32);
    static float mixBufferClearDelay = 0.05f;
    static float timeUnScaleDentaTime = 0.02f;


    private float normalMusicVolume;

    private object pitchedSoundHandle = new object();


    private void OnEnable()
    {
        InitConstant();
        EventGlobalManager.Instance.OnUpdateSetting.AddListener(UpdateSoundSetting);
        UpdateSoundSetting();
    }

    private void OnDestroy()
    {
        if (EventGlobalManager.Instance == null)
            return;

        EventGlobalManager.Instance.OnUpdateSetting.RemoveListener(UpdateSoundSetting);

        StopPitchedSound(true);
    }

    public void UpdateSoundSetting()
    {
        if (GameManager.Instance == null) return;
        var settingData = GameManager.Instance.Data.Setting;

        sfx.volume = settingData.SoundVolume;
        music.volume = settingData.MusicVolume;
        normalMusicVolume = settingData.MusicVolume;
    }
    public void UpdateSoundSetting2(float sfxVolumn , float musicVolumn)
    {
        if (GameManager.Instance == null) return;

        sfx.volume = sfxVolumn;
        music.volume = musicVolumn;
        normalMusicVolume = musicVolumn;
    }

    public string currentTrack;

    public void InitConstant()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
            Destroy(gameObject);

        soundCfg = ConfigManager.Instance.Audio;

        StartCoroutine(MixBufferRoutine());
    }

    // Coroutine responsible for limiting the frequency of playing sounds
    IEnumerator MixBufferRoutine()
    {
        float time = 0;

        while (true)
        {
            time += timeUnScaleDentaTime;
            yield return null;
            if (time >= mixBufferClearDelay)
            {
                mixBuffer.Clear();
                time = 0;
            }
        }
    }

    // Launching a music track
    public void PlayMusic(string trackName, float delay = .3f, float delayStartFadeOut = 0f)
    {
        if (trackName != string.Empty)
            currentTrack = trackName;

        AudioClip to = null;
        if (trackName != string.Empty)
        {
            List<Bgm> listTrack = new List<Bgm>(1);

            foreach (Bgm track in soundCfg.ListBgm)
                if (track.name == trackName)
                    listTrack.Add(track);

            int random = Random.Range(0, listTrack.Count);
            to = listTrack[random].track;
        }

        if (Instance != null && to != null)
            StartCoroutine(Instance.CrossFade(to, delay, delayStartFadeOut));
    }

    public void PlayRandomMusic()
    {
        PlayMusic(soundCfg.ListBgm.GetRandom().name);
    }

    // A smooth transition from one to another music
    IEnumerator CrossFade(AudioClip to, float delay = .3f, float delayStartFadeOut = 0f)
    {
        float countDownDelay = delay;
        if (music.clip != null)
        {
            while (countDownDelay > 0)
            {
                music.volume = countDownDelay * normalMusicVolume;
                countDownDelay -= timeUnScaleDentaTime;
                yield return null;
            }
        }

        music.clip = to;
        if (to == null)
        {
            music.Stop();
            yield break;
        }

        yield return Yielders.Get(delayStartFadeOut);
        countDownDelay = 0;
        if (!music.isPlaying) music.Play();
        while (countDownDelay < delay)
        {
            music.volume = countDownDelay * normalMusicVolume *3.333f;
            countDownDelay += timeUnScaleDentaTime;
            yield return null;
        }
    }


    public static void Shot(TYPE_SOUND typeSound)
    {
        if (typeSound != TYPE_SOUND.NONE && !mixBuffer.Contains(typeSound))
        {
            mixBuffer.Add(typeSound);
            if (Instance != null && Instance.sfx != null && Instance.soundCfg.GetAudio(typeSound) != null)
                Instance.sfx.PlayOneShot(Instance.soundCfg.GetAudio(typeSound));
        }
    }

    public void FadeOutMusic()
    {
        music.volume = 0.1f;
    }

    public void FadeInMusic()
    {
        music.volume = normalMusicVolume;
    }

    public void Pause()
    {
        music.Pause();
    }

    public void PlayAgain()
    {
        music.UnPause();
    }

    public void SetPitch(float value)
    {
        pitchedAudioSource.pitch = Mathf.Lerp(pitchLevelMinMax.x, pitchLevelMinMax.y, value);
    }

    public IDisposable StartPitchedSound(TYPE_SOUND soundType)
    {
        if (soundType == TYPE_SOUND.NONE)
        {
            return Disposable.Empty;
        }

        var audioClip = soundCfg.GetAudio(soundType);

        SetPitch(0.0f);

        pitchedAudioSource.Stop();
        pitchedAudioSource.loop = true;
        pitchedAudioSource.clip = audioClip;
        pitchedAudioSource.Play();

        DOVirtual.Float(0.0f, 1.0f, 5.0f, SetPitch).SetTarget(pitchedSoundHandle);

        return Disposable.Create(() =>
        {
            StopPitchedSound();
        });
    }

    public void StopPitchedSound(bool immediately = false)
    {
        DOTween.Kill(pitchedSoundHandle);

        pitchedAudioSource.loop = false;

        if (immediately)
        {
            pitchedAudioSource.Stop();
        }
    }
}