using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceManager : MonoBehaviour {

    private AudioSource audioSource;
    public List<string> clipNames = new List<string>();
    public List<AudioClip> audioClips = new List<AudioClip>();

    // Use this for initialization
    void Start() {
        this.audioSource = this.GetComponent<AudioSource>();
    }

    public void SetVolume(float volume) {
        this.audioSource.volume = volume;
    }

    public void PlayAudioClip(string key) {
        if (this.clipNames.IndexOf(key) > -1) {
            this.audioSource.PlayOneShot(this.audioClips[this.clipNames.IndexOf(key)]);
        } else {
            print("No sound for " + key);
        }
    }
}
