using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shark : MonoBehaviour {
    public Transform target;
    float timeCounter;
    float speed;
    float width;
    float length;
    float direction;

	// Use this for initialization
	void Start () {
        speed = 1;
        width = 2;
        length = 3;
        direction = 1;
        }
	
	// Update is called once per frame
	void Update () {
        if (Vector3.Distance(target.position, transform.position) <= 5)
        {
            if (Mathf.Approximately(direction,1)) 
            {
                transform.LookAt(target);
            }
            else
            {
                transform.rotation = Quaternion.LookRotation(transform.position - target.position);
            }

            transform.position += direction*(target.position - transform.position).normalized / 50;
        }
        else
        {
            direction = 1;
            transform.forward = Vector3.Normalize(new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"))); ;
            timeCounter += Time.deltaTime * speed;
            float x = Mathf.Cos(timeCounter) * width ;
            float y = 0;
            float z = Mathf.Sin(timeCounter) * length;

            transform.position += new Vector3(x, y, z) * Time.deltaTime*speed;
        }

    }

    // Collide with Player
    void OnTriggerEnter(Collider player)
    {
        if(player.gameObject.tag == "Player") 
        {
            direction = -1;
        }
    }
}
