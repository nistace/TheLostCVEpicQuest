using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechanismLight : Mechanism {

    private SpriteRenderer lightSprite;

    public bool lightOnButtonEnabled = true;
    public bool lightOnButtonDisabled = false;

    // Use this for initialization
    void Start () {
        this.lightSprite = this.GetComponent<SpriteRenderer>();
    }

    public override void SetMechanismEnabled(bool triggerEnabled) {
        this.lightSprite.enabled = triggerEnabled && this.lightOnButtonEnabled || !triggerEnabled && this.lightOnButtonDisabled;
    }


}
