using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waves : MonoBehaviour {

Vector3 basePos;
	// Use this for initialization
	void Start () {
		basePos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = basePos + new Vector3(
			0, 
			Mathf.Cos(Time.time / 3), 
			Mathf.Sin(Time.time / 3) * 5
		);
	}
}
