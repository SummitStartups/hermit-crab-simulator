using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

	public Transform cameraObject;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 primaryTouchpad = OVRInput.Get(OVRInput.Axis2D.PrimaryTouchpad);
		if (primaryTouchpad.y > 0 || Input.GetKey(KeyCode.W) ) {
			transform.position += cameraObject.forward / 100;
		}
		if (primaryTouchpad.y < 0 || Input.GetKey(KeyCode.S) ) {
			transform.position -= cameraObject.forward / 100;
		}
		if (primaryTouchpad.x > 0 || Input.GetKey(KeyCode.D) ) {
			transform.position += cameraObject.right / 100;
		}
		if (primaryTouchpad.x < 0 || Input.GetKey(KeyCode.A) ) {
			transform.position -= cameraObject.right / 100;
		}
	}
}
