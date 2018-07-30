using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrampolineBoing : MonoBehaviour {

    private AudioSourceManager audioSourceManager;
    private Animator animator;
    
    // Use this for initialization
	void Start () {
        this.audioSourceManager = this.GetComponent<AudioSourceManager>();
        this.animator = this.GetComponent<Animator>();
    }

    public void OnCollisionEnter2D(Collision2D collision) {
        this.audioSourceManager.PlayAudioClip("boing");
        this.animator.SetBool("Triggered", true);
    }

    public void OnCollisionExit2D(Collision2D collision) {
        this.animator.SetBool("Triggered", false);
    }
}
