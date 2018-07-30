using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour {

    public string tutoText;
    public GameObject helpPanel;
    private Text helpPanelText;
    public bool displayed = false;

    public void Start() {
        this.helpPanelText = this.helpPanel.GetComponentInChildren<Text>();
    }

    public void OnTriggerStay2D(Collider2D collision) {
        if (!displayed && collision.tag == "Player") {
            this.helpPanelText.text = this.tutoText;
            this.helpPanel.SetActive(true);
            displayed = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision) {
        if (collision.tag == "Player") {
            this.helpPanel.SetActive(false);
            this.helpPanelText.text = string.Empty;
        }
    }
}
