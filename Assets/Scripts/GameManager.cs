using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public PlayerMovement playerMovementScript;
    public AudioSource musicAudioSource;
    public ObjectiveManager objectiveManager;
    public GameObject startPanel;
    public GameObject victoryPanel;
    public MuteButton muteManager;
    private AudioSourceManager gameAudioSource;
    private bool alreadyWon = false;

    // Use this for initialization
    void Start() {
        this.gameAudioSource = this.GetComponent<AudioSourceManager>();
        this.startPanel.SetActive(true);
        this.victoryPanel.SetActive(false);
      
    }

    void Update() {
        if (!this.alreadyWon && this.objectiveManager.IsObjectiveAcquired()) {
            this.alreadyWon = true;
            this.playerMovementScript.canMove = false;
            this.musicAudioSource.Stop();
            this.victoryPanel.SetActive(true);
            this.gameAudioSource.PlayAudioClip("victory");
            this.musicAudioSource.PlayDelayed(1.5f);
        }
    }

    public void StartGame() {
        this.gameAudioSource.PlayAudioClip("buttonclick");
        this.startPanel.SetActive(false);
        this.playerMovementScript.canMove = true;
    }

    public void RedirectToCV() {
        this.gameAudioSource.PlayAudioClip("buttonclick");
        Application.OpenURL("http://nathanistace.be/cv/cv.html");
        this.victoryPanel.SetActive(false);
        this.muteManager.Mute();
    }
}
