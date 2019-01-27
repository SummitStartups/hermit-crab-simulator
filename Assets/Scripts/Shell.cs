using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
    public Transform home;
    public Transform shell;
    private bool isEmpty;
    private float size; 

    void Start()
    {
        // prefab shell size on shell transform when starting game
        isEmpty = false;
        size = Random.Range(1,5);
    }

    // Update is called once per frame
    void Update()
    {
        if (isEmpty == false) 
        {
            // option 1
            // Movement.OnCollisionEnter(Collision col); // how do i talk to Movement class?
            // isEmpty = true;

            // option 2
            if (true) { // gets inhabited
                // add shell onto player object -> done in Movement class
                isEmpty = true;
            }
        }

        // if (Movement.currSize > size) // player bigger than shell 
        // {
        //     shell.SetParent(null);
		// 	shell = null;
        // }
    }
}
