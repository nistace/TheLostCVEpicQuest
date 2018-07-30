using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuteButton : MonoBehaviour {

    public List<AudioSource> audioSources = new List<AudioSource>();
    public bool isMute = false;
    public float unmutedVolume = .3f;
    private Image buttonImage;
    public Sprite muteImage;
    public Sprite unmuteImage;

    public void Start() {
        this.buttonImage = this.GetComponent<Image>();
        this.UpdateAudioSourceManagers();
        this.UpdateButtonText();
    }

    public void ToggleMute() {
        print("mute " + !this.isMute);
        this.isMute = !this.isMute;
        this.UpdateAudioSourceManagers();
        this.UpdateButtonText();
    }

    private void UpdateAudioSourceManagers() {
        foreach (AudioSource asm in this.audioSources) {
            asm.volume = this.isMute ? 0 : this.unmutedVolume;
        }
    }

    private void UpdateButtonText() {
        this.buttonImage.sprite = this.isMute ? this.muteImage : this.unmuteImage;
    }

    internal void Mute() {
        if (!this.isMute)
            this.ToggleMute();
    }
}
