using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    public AudioClip startBGM_clip;
    public AudioClip loopBGM_clip;
    public AudioClip stopBGM_clip;

    private AudioSource startBGM_source;
    private AudioSource loopBGM_source;
    private AudioSource stopBGM_source;

    private bool play_main_loop = true;

    void Start() {
        startBGM_source = AddAudio(startBGM_clip, false, true, 0.2f);
        loopBGM_source = AddAudio(loopBGM_clip, true, true, 0.6f);
        stopBGM_source = AddAudio(stopBGM_clip, false, true, 0.6f);

        if(!startBGM_source.isPlaying) {
            startBGM_source.Play();
        }
    }

    public AudioSource AddAudio(AudioClip clip, bool loop, bool play_awake, float vol) {
        AudioSource new_audio = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;
        new_audio.clip = clip;
        new_audio.loop = loop;
        new_audio.playOnAwake = play_awake;
        new_audio.volume = vol;

        return new_audio;
    }

    void Update() {

        if((play_main_loop) && 
            !startBGM_source.isPlaying && !loopBGM_source.isPlaying) {
                if(play_main_loop) loopBGM_source.Play();
        }
    }


    public void StopMainBGMLoop() {
        if(play_main_loop) {
            startBGM_source.Stop();
            loopBGM_source.Stop();
            stopBGM_source.Play();
            play_main_loop = false;
        }
    }
}