using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatherBowManager : MonoBehaviour {

    private PlayerMovement playerManager;
    private AudioSourceManager audioSourceManager;

    void Start() {
        this.playerManager = this.GetComponent<PlayerMovement>();
        this.audioSourceManager = this.GetComponent<AudioSourceManager>();
    }

    public void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Bow") {
            this.playerManager.hasBow = true;
            this.audioSourceManager.PlayAudioClip("gather");
            Destroy(collision.gameObject);
        }
    }
}
