using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Size : MonoBehaviour {

    float size;
    float growTime = 5f;

	// Use this for initialization
	void Start () {
        size = 1;
        InvokeRepeating("Grow", growTime,growTime);
	}
	
	// Update is called once per frame
	void Update () 
    {
    }

    // Recurring growth
    void Grow()
    {
        print("GROW");
        print(size);
        size++;
    }
}
