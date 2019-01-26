using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
    public Transform home;
    float timeCounter;
    float width = 1;
    float length = 1;
    float direction = 1;
    float currSize;
    float maxSize; // hermit can grow in new shell 2x size of previous shell

    void Start()
    {
        currSize = width * length;
        maxSize = width * length * 2; // hermit can grow in new shell 2x size of previous shell

    }

    // Update is called once per frame
    void Update()
    {
        if (true)//[[[gets inhabited]]])
        {
            // currSize += [[[size of new shell]]] // or currSize += new shell
            maxSize *= 2; 
        }
    }
}
