using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GatherCvPartManager : ObjectiveManager {

    public int gatheredCvParts = 0;
    public int cvPartsToGather = 4;
    public Text uiTextDisplay;
    private AudioSourceManager audioSourceManager;
    private readonly List<GameObject> listGathered = new List<GameObject>();

    public void Start() {
        this.UpdateUiTextDisplay();
        this.audioSourceManager = this.GetComponent<AudioSourceManager>();
    }

    public void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "CvPart" && !this.listGathered.Contains(collision.gameObject)) {
            this.listGathered.Add(collision.gameObject);
            Destroy(collision.gameObject);           
            this.gatheredCvParts++;
            this.UpdateUiTextDisplay();
            this.audioSourceManager.PlayAudioClip("gather");
        }
    }

    private void UpdateUiTextDisplay() {
        this.uiTextDisplay.text = this.gatheredCvParts + " / " + this.cvPartsToGather;
    }

    protected internal override bool IsObjectiveAcquired() {
        return this.gatheredCvParts == this.cvPartsToGather;
    }
}
