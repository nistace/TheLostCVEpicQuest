using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour {

    private SpriteRenderer buttonSpriteRenderer;
    private GameObject player;
    private CircleCollider2D areaOfEffet;
    private AudioSourceManager audioSourceManager;
    public Sprite disabledSprite;
    public Sprite enabledSprite;
    public List<Mechanism> mechanisms = new List<Mechanism>();
    public bool buttonEnabled;
    private bool sameActionPress = false;

    // Use this for initialization
    public void Start() {
        this.player = GameObject.FindGameObjectWithTag("Player");
        this.areaOfEffet = this.GetComponent<CircleCollider2D>();
        this.buttonSpriteRenderer = this.GetComponent<SpriteRenderer>();
        this.audioSourceManager = this.GetComponent<AudioSourceManager>();
        this.UpdateMechanisms();
        this.UpdateDisplay();
    }

    public void Update() {
        if (Input.GetAxis("Action") > 0) {
            if (!this.sameActionPress && this.areaOfEffet.IsTouching(player.GetComponent<Collider2D>())) {
                this.ToggleButtonEnabled();
                sameActionPress = true;
            }
        } else {
            sameActionPress = false;
        }
    }

    private void ToggleButtonEnabled() {
        this.buttonEnabled = !this.buttonEnabled;
        this.UpdateMechanisms();
        this.UpdateDisplay();
        this.PlaySound();
    }

    private void PlaySound() {
        if (this.audioSourceManager != null)
            this.audioSourceManager.PlayAudioClip(this.buttonEnabled ? "Enable" : "Disable");
    }

    private void UpdateMechanisms() {
        foreach (Mechanism mechanism in this.mechanisms) {
            mechanism.SetMechanismEnabled(this.buttonEnabled);
        }
    }

    private void UpdateDisplay() {
        this.buttonSpriteRenderer.sprite = this.buttonEnabled ? this.enabledSprite : this.disabledSprite;
    }

}
