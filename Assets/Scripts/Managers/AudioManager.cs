using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(AudioSource),typeof(AudioSource))]
public class AudioManager : ISingleton<AudioManager>
{
    [Header("=== objects ===")]
    public AudioSource soundEffect;
    public AudioSource bgm;
    public AudioSource dub;

    [Header("=== audios ===")]
    public AudioClip slimeMove;
    public AudioClip angelFallDown;

    public override void Awake() {
        soundEffect = gameObject.AddComponent<AudioSource>();
        bgm = gameObject.AddComponent<AudioSource>();
        dub = gameObject.AddComponent<AudioSource>();
    }

    public void init() {
        //todo 根据玩家配置初始化
    }

    public void playSoundEffect(AudioClip clip) {
        soundEffect.PlayOneShot(clip);
    }

    public void playBgm(AudioClip clip) {
        if (!bgm.isPlaying) {
            bgm.PlayOneShot(clip);
        }
    }

    public void playDub(AudioClip clip) {
        if (dub.isPlaying) {
            dub.Stop();
        }
        dub.PlayOneShot(clip);
    }

    public void setVolume(AudioSource _audioSource,float _value) {
        _audioSource.volume = _value;
    }

    public void setSoundEffectVolume(float value) {
        soundEffect.volume = value;
    }

    public void setBgmVolume(float value) {
        bgm.volume = value;
    }

    public void setDubVolume(float value) {
        dub.volume = value;
    }

    public void toggleSoundEffectMute() {
        soundEffect.mute = !soundEffect.mute;
    }
    public void toggleBgmMute() {
        bgm.mute = !bgm.mute;
    }
    public void toggleDubMute() {
        dub.mute = !dub.mute;
    }
}
