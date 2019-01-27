using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
    public MeshRenderer shell;
    private bool isEmpty = true;
    public bool isGood = false;
    private float size;
    private Material mat;
    public Material goodMat;

    void Start()
    {
        // prefab shell size on shell transform when starting game
        isEmpty = true;
        size = transform.lossyScale.x;
        mat = shell.material;
    }

    // Update is called once per frame
    void Update()
    {
        if (Movement.instance.transform.localScale.x > size / 20
        && Movement.instance.transform.localScale.x < size / 10)
        {
            isGood = true;
        }
        else
        {
            isGood = false;
        }
        if (isGood && transform != Movement.instance.shell)
        {
            shell.material = goodMat;
        }
        else
        {
            shell.material = mat;
        }
    }
}
