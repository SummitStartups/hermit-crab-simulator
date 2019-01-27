using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
    public MeshRenderer shell;
    private bool isEmpty = false;
    private float size;
    private Material mat;
    public Material goodMat; 

    void Start()
    {
        // prefab shell size on shell transform when starting game
        isEmpty = false;
        size = transform.localScale.x;
        mat = shell.material;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isEmpty) 
        {
            // option 1
            // Movement.OnCollisionEnter(Collision col); // how do i talk to Movement class?
            // isEmpty = true;
            if(Movement.instance.transform.localScale.x > transform.localScale.x
            && Movement.instance.transform.localScale.x < transform.localScale.x * 10
            && transform != Movement.instance.shell){
                shell.material = goodMat;
            }else{
                shell.material = mat;
            }

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
