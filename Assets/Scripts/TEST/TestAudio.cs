using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAudio : MonoBehaviour
{
    public AudioClip clip;
    public AudioClip bgm;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K)) {
            AudioManager.Instance.setVolume(AudioManager.Instance.soundEffect,0.1f);
            AudioManager.Instance.setVolume(AudioManager.Instance.bgm,0.2f);
        }
        if (Input.GetKeyDown(KeyCode.J)) {
            AudioManager.Instance.playSoundEffect(clip);
        }
        if (Input.GetKeyDown(KeyCode.L)) {
            AudioClip clip = ResourceManager.Instance.GetAssetCache<AudioClip>("Media/BGM/Angel fall Down.wav");
            AudioManager.Instance.playBgm(clip);
        }
    }
}
