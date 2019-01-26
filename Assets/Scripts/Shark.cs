using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shark : MonoBehaviour {
    public Transform target;
    float timeCounter;
    float speed;
    float width;
    float length;

	// Use this for initialization
	void Start () {
        speed = 1;
        width = 2;
        length = 3;

        }
	
	// Update is called once per frame
	void Update () {
        if (Vector3.Distance(target.position, transform.position) <= 5)
        {
            transform.position += (target.position - transform.position).normalized / 50;
        }
        else
        {
            timeCounter += Time.deltaTime * speed;
            float x = Mathf.Cos(timeCounter) * width ;
            float y = 0;
            float z = Mathf.Sin(timeCounter) * length;

            transform.position += new Vector3(x, y, z) * Time.deltaTime*speed;
        }

    }
}
