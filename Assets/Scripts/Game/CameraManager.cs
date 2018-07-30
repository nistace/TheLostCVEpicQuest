using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {


    public Transform target;
    private Vector3 cameraOffset;
    public float smoothness = 5;

	void Start () {
        this.transform.position = new Vector3(target.position.x, target.position.y, this.transform.position.z);
        this.cameraOffset = this.transform.position - this.target.position;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        this.transform.position = Vector3.Lerp(this.transform.position, this.target.position + this.cameraOffset, this.smoothness * Time.deltaTime);
	}
}
