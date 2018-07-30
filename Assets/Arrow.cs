using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

    private Rigidbody2D arrowRigidbody;
    private bool ended = false;

    void Start() {
        this.arrowRigidbody = this.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate() {
        if (!this.ended)
            this.transform.Rotate(new Vector3(0, 0, Vector3.Angle(this.transform.right, this.arrowRigidbody.velocity.normalized) - 90));
    }

    public void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "WeakRope")
            Destroy(collision.gameObject);
        this.ended = true;
        this.gameObject.isStatic = true;
        this.arrowRigidbody.bodyType = RigidbodyType2D.Static;
        this.GetComponent<Collider2D>().enabled = false;
    }

}
