using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource),typeof(AudioSource))]
public class AudioManager : ISingleton<AudioManager>
{
    [Header("=== objects ===")]
    public AudioSource oneShot;
    public AudioSource bgm;

    [Header("=== audios ===")]
    public AudioClip slimeMove;
    public AudioClip angelFallDown;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playOnce(AudioClip clip) {
        oneShot.PlayOneShot(clip);
    }

    public void playBgm(AudioClip clip) {
        if (!bgm.isPlaying) {
            bgm.PlayOneShot(clip);
        }
    }

    public void setVolume(AudioSource _audioSource,float _value) {
        _audioSource.volume = _value;
    }

    public void setEffectVolume(float value) {
        oneShot.volume = value;
    }

    public void setBgmVolume(float value) {
        oneShot.volume = value;
    }

    
}
