using UnityEngine;
using System;
using QFramework;

public class AudioManager : MonoSingleton<AudioManager>,IController
{
    [SerializeField]
    AudioSource soundSource;
    [SerializeField]
    AudioSource musicSource;

    PlayerSettingModel model;

    private void Awake()
    {
        Init();
        model = this.GetModel<PlayerSettingModel>();
    }

    public void SetSoundMuteStatus(bool status)
    {
        soundSource.mute = status;
        model.soundMute = status;
    }

    public void SetMusicMuteStatus(bool status)
    {
        musicSource.mute = status;
        model.musicMute = status;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="volume">Range(0-1)</param>
    public void SetSoundVolume(float volume)
    {
        volume = Math.Clamp(volume, 0, 1);
        soundSource.volume = volume;
        model.soundVolume = volume;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="volume">Range(0-1)</param>
    public void SetMusicVolume(float volume)
    {
        volume = Math.Clamp(volume, 0, 1);
        soundSource.volume = volume;
        model.musicVolume = volume;
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void PlayMusic(string musicName, bool loop = true)
    {
        if (musicSource.isPlaying)
            musicSource.Stop();
        AudioClip clip = ResLoader.LoadMusic(musicName);
        musicSource.loop = loop;
        musicSource.clip = clip;
        musicSource.Play();
    }

    public void PlaySound(string soundName)
    {
        AudioClip clip = ResLoader.LoadSound(soundName);
        soundSource.PlayOneShot(clip);
    }

    public void PlaySourceSound(AudioSource source,string soundName)
    {
        AudioClip clip = ResLoader.LoadSound(soundName);
        source.PlayOneShot(clip);
    }
    void Init()
    {
        if (soundSource == null)
        {
            soundSource = gameObject.AddComponent<AudioSource>();
            soundSource.loop = false;
            soundSource.playOnAwake = false;
        }
        if (musicSource == null)
            musicSource = gameObject.AddComponent<AudioSource>();
    }

    public IArchitecture GetArchitecture()
    {
        return Template.Interface;
    }
}
