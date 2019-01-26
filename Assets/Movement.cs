using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

	public Transform cameraObject;
	public float speed = 2;
	public bool hiding = false; // when true, character can't move
	public bool charge = false;
	public bool ExitShell = false; // when true, character is vulnerable from all sides
	// public bool jump = false; // when true, character can't attack

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 primaryTouchpad = OVRInput.Get(OVRInput.Axis2D.PrimaryTouchpad);
		if (hiding == false) {
			if (primaryTouchpad.y > 0.2f || Input.GetKey(KeyCode.W) ) {
				transform.position += cameraObject.forward * speed / 100;
			}
			if (primaryTouchpad.y < -0.2f || Input.GetKey(KeyCode.S) ) {
				transform.position -= cameraObject.forward * speed / 100;
			}
			if (primaryTouchpad.x > 0.2f || Input.GetKey(KeyCode.D) ) {
				transform.position += cameraObject.right * speed / 100;
			}
			if (primaryTouchpad.x < -0.2f || Input.GetKey(KeyCode.A) ) {
				transform.position -= cameraObject.right * speed / 100;
			}
		}

		hiding = (OVRInput.Get(OVRInput.Button.Down) && primaryTouchpad.y < -0.2f) || Input.GetKey(KeyCode.H); 
		charge = (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger)) || Input.GetKey(KeyCode.C );
		ExitShell = (OVRInput.Get(OVRInput.RawButton.Back) || Input.GetKey(KeyCode.E));
		// jump = OVRInput.Get(OVRInput.Button.Up) || Input.GetKey(KeyCode.J); 

		if (hiding) {
			// shell moves down covering player's vision
		}

		if (charge) {
			transform.position += cameraObject.forward * speed / 100;
		}

		if (ExitShell) {
			// shell moves up and becomes detached from player
		}

		// if (jump) {
		// 	// move up y-axis temporarily by height of character
		// }
	}
}
