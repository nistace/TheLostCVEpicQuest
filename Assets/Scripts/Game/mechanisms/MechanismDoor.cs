using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechanismDoor : Mechanism {

     SpriteRenderer doorSpriteRenderer;
    private Collider2D doorCollider;
    private bool doorOpen = false;

    public Sprite doorOpenSprite;
    public Sprite doorClosedSprite;

    public void Awake() {
        this.doorSpriteRenderer = this.GetComponent<SpriteRenderer>();
        this.doorCollider = this.GetComponent<Collider2D>();
    }

    // Use this for initialization
    void Start() {
        this.UpdateMechanism();
    }

    private void UpdateMechanism() {
        this.doorSpriteRenderer.sprite = this.doorOpen ? this.doorOpenSprite : this.doorClosedSprite;
        this.doorCollider.enabled = !this.doorOpen;
    }

    public override void SetMechanismEnabled(bool triggerEnabled) {
        this.doorOpen = triggerEnabled;
        this.UpdateMechanism();
    }
}
