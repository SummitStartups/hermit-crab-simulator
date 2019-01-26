using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotation : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        transform.eulerAngles = new Vector3(0, Random.value * 360, 0);
    }
}
