using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
    public Transform home;
    public Transform shell;
    private bool isEmpty;
    private float maxSize; // hermit can grow in new shell 2x size of previous shell
    private float currSize;

    void Start()
    {
        // prefab shell size on shell transform when starting game
        isEmpty = false;
        currSize = Random.Range(1,11);
        maxSize = currSize * 2; // hermit grows in new shell 2x size of previous shell??
    }

    // Update is called once per frame
    void Update()
    {
        if (isEmpty == false) 
        {
            // Movement.OnCollisionEnter(Collision col); // how do i talk to Movement class?
            isEmpty = true;
            // if (true) { // gets inhabited
            //     // add shell onto player object -> done in Movement class
            //     maxSize *= 2; 
            //     isEmpty = true;
            // }
        }
        if (true) // player bigger than shell 
        {
            shell.SetParent(null);
			shell = null;
        }
    }
}
