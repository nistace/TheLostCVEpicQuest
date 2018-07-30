using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationChecker : MonoBehaviour {

    private PlayerMovement playerMovement;

    // Use this for initialization
    void Start() {
        this.playerMovement = this.GetComponentInParent<PlayerMovement>();
    }

    public void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ladders"))
            this.playerMovement.SetOnLadder(true);
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
            this.playerMovement.SetOnGround(true);
    }


    public void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ladders"))
            this.playerMovement.SetOnLadder(false);
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
            this.playerMovement.SetOnGround(false);
    }
}
